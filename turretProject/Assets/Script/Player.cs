using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform turretTransform;
    public Transform spawnBulletPos;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float rotationSpeed = 5f;
    public float turretAngleOffset = 0f;

    private float nextFireTime = 0f;
    private Animator anim;

    public ParticleSystem shootVFX;
    public ParticleSystem explosionVFX;

    private Camera mainCam;
    public Animator cameraAnim;

    public bool isAlive = true;
    public bool isDamaged = false;
    public bool isDamagedBomb = false;

    private bar barScript;

    private void Start()
    {
        Camera mainCam = Camera.main;
        cameraAnim = mainCam.GetComponent<Animator>();

        anim = GetComponent<Animator>();
        barScript = FindAnyObjectByType<bar>();
    }

    private void Update()
    {
        if (isAlive)
        {   
            if (turretTransform != null)
            {
                RotateTurret();
            }

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
    }

    private void RotateTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        
        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 direction = ray.GetPoint(distance) - turretTransform.position;
            direction.y = 0;
            
            if (direction.sqrMagnitude > 0.01f)
            {
                float targetAngle = Quaternion.LookRotation(direction).eulerAngles.y + turretAngleOffset;
                float smoothAngle = Mathf.MoveTowardsAngle(turretTransform.eulerAngles.y, targetAngle, rotationSpeed * 360f * Time.deltaTime);
                turretTransform.rotation = Quaternion.Euler(-90f, smoothAngle, 0f);
            }
        }
    }

    private void Shoot()
    {
        shootVFX.Play();
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        cameraAnim.SetTrigger("shoot");

        anim.SetTrigger("shoot");
        
        if (bulletPrefab != null && spawnBulletPos != null)
        {
            Instantiate(bulletPrefab, spawnBulletPos.position, spawnBulletPos.rotation);
        }
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
        isDamaged = true;
        barScript.healthBar -= 0.25f;
        barScript.healthImg.fillAmount = barScript.healthBar;   
        Invoke(nameof(ResetDamageFlag), 1f);
        if(barScript.healthBar <= 0) Death();   
        cameraAnim.SetTrigger("death"); 
    }

    private void TakeDamageBomb()
    {
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
    }

    private void ResetDamageFlag()
    {
        isDamaged = false;
    }
}