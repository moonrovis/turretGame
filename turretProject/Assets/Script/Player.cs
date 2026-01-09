using NUnit.Framework;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform turretTransform;
    public Transform spawnBulletPos;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float rotationSpeed = 5f;
    public float turretAngleOffset = 0f;
    public float turretRotatorOffset;

    private float nextFireTime = 0f;
    private Animator anim;

    public ParticleSystem shootVFX;
    public ParticleSystem explosionVFX;

    private Camera mainCam;
    public Animator cameraAnim;

    public bool isAlive = true;
    public bool isDamaged = false;
    public bool isDamagedBomb = false;
    public bool isGunSpeedActive = false;

    private bar barScript;
    private gunSpeedAbility gunSpeedScript;
    private GameManager gameManagerScript;
    private AmmoManager ammoScript;
    private AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip emptyAmmoSound;

    private float timer = 11f;
    public GameObject abCanvas;
    public TextMeshProUGUI abTimer;

    [Header("Mobile Controls")]
    public Joystick joystick; // Пример: FloatingJoystick
    private bool useMobileControl = false;
    public GameObject mobileUICanvas; // 🔧 Ссылка на весь Canvas с джойстиком и кнопкой

    private bool isMobileShootPressed = false;

    private bool isMobile;

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
        useMobileControl = isMobile;

        if(mobileUICanvas != null) mobileUICanvas.SetActive(isMobile);

        Camera mainCam = Camera.main;
        cameraAnim = mainCam.GetComponent<Animator>();

        anim = GetComponent<Animator>();
        barScript = FindAnyObjectByType<bar>();
        gunSpeedScript = FindAnyObjectByType<gunSpeedAbility>();
        gameManagerScript = FindAnyObjectByType<GameManager>();
        ammoScript = FindAnyObjectByType<AmmoManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isAlive && !gameManagerScript.isPause)
        {   
            if (turretTransform != null)
            {
                RotateTurret();
            }

            bool shouldShoot = false;

            if (useMobileControl)
            {
                shouldShoot = isMobileShootPressed;
            }
            else
            {
                shouldShoot = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
            }
            
            if(shouldShoot) Shoot();
        }

        if (isGunSpeedActive)
        {
            abCanvas.SetActive(true);
            timer -= Time.deltaTime;
            int secondsLeft = Mathf.Max(Mathf.FloorToInt(timer), 0);
            abTimer.text = secondsLeft.ToString();
        }
    }

    private void RotateTurret()
    {
        Vector3 direction = Vector3.zero;

        if (useMobileControl && joystick != null)
        {
            // Мобильный ввод: джойстик
            Vector2 joyInput = joystick.Direction;

            if (joyInput.sqrMagnitude > 0.1f)
            {
                // Джойстик даёт 2D направление (x, y)
                // Мы используем x как движение вправо/влево, y как вперёд/назад
                direction = new Vector3(joyInput.x, 0f, joyInput.y);
            }
        }
        else
        {
            // ПК: мышь
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                direction = new Vector3(hitPoint.x, 0f, hitPoint.z) - turretTransform.position;
            }
        }

        // Общий поворот туррели
        if (direction.sqrMagnitude > 0.1f)
        {
            float targetAngle = Quaternion.LookRotation(direction).eulerAngles.y + turretAngleOffset;
            float currentAngle = turretTransform.eulerAngles.y;
            float smoothAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * 360f * Time.deltaTime);
            turretTransform.rotation = Quaternion.Euler(turretRotatorOffset, smoothAngle, 0f);
        }
    }

    private void Shoot()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        cameraAnim.SetTrigger("shoot");

        
        if (bulletPrefab != null && spawnBulletPos != null)
        {
            if(ammoScript.ammoCount > 0)
            {               
                anim.SetTrigger("shoot");
                shootVFX.Play();
                Instantiate(bulletPrefab, spawnBulletPos.position, spawnBulletPos.rotation);
                ammoScript.ReduceAmmo();
                float[] pitches = {0.9f, 1f, 1.1f};
                audioSource.pitch = pitches[Random.Range(0, pitches.Length)];
                audioSource.PlayOneShot(shootSound, 1f);
            }
            else audioSource.PlayOneShot(emptyAmmoSound, 1f);
        }
    }

    public void GunSpeed()
    {
        fireRate = 0.25f;
        isGunSpeedActive = true;

        CancelInvoke(nameof(DeactivateGunSpeed));
        Invoke(nameof(DeactivateGunSpeed), 10f);

    }
    private void DeactivateGunSpeed()
    {
        fireRate = 0.5f;
        isGunSpeedActive = false;
        abCanvas.SetActive(false);
        timer = 11f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            TakeDamage();
        }
        if (other.CompareTag("bomb"))
        {
            TakeDamageBomb();
        }
    }

    private void TakeDamage()
    {
        explosionVFX.Play();
        isDamaged = true;
        barScript.healthBar -= 0.25f;
        barScript.healthImg.fillAmount = barScript.healthBar;   
        Invoke(nameof(ResetDamageFlag), 1f);
        if(barScript.healthBar <= 0) Death();   
        cameraAnim.SetTrigger("death"); 
    }

    private void TakeDamageBomb()
    {
        explosionVFX.Play();
        isDamagedBomb = true;
        barScript.healthBar -= 0.5f;
        barScript.healthImg.fillAmount = barScript.healthBar;   
        Invoke(nameof(ResetDamageFlag), 1f);
        if(barScript.healthBar <= 0) Death();   
        cameraAnim.SetTrigger("death"); 
    }

    private void Death()
    {
        isAlive = false;
        anim.SetTrigger("death");
        explosionVFX.Play();
        cameraAnim.SetTrigger("death");
        gameManagerScript.OnPlayerDeath();
    }

    private void ResetDamageFlag()
    {
        isDamaged = false;
    }

    public void SetMobileShootPressed(bool pressed)
    {
        isMobileShootPressed = pressed;
    }
}