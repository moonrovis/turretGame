using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float nextFireTime = 0f; // Изначально 0 — значит, можно стрелять сразу
    public float fireRate = 0.5f; // Выстрел каждые 0.5 секунды

    public Transform turretTransform; // Башня танка
    public float rotationSpeed = 5f;  // Скорость поворота (настраивается в Inspector)

    private Rigidbody rb;
    private Animator anim;

    public Transform spawnBulletPos;
    public GameObject bulletPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!turretTransform)
            return;
        
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = targetPoint - turretTransform.position;
            direction.y = 0; // Только горизонтальное направление

            if (direction.sqrMagnitude > 0.01f)
            {
                // Желаемый поворот вокруг оси Y
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                float targetAngle = targetRotation.eulerAngles.y;

                // Плавно интерполируем угол по оси Y
                float currentAngle = turretTransform.eulerAngles.y;
                float smoothAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime * 360f);

                // Применяем только поворот по Y, сохраняя другие оси
                turretTransform.rotation = Quaternion.Euler(-90f, smoothAngle, 0f);
            }
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if(Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        anim.SetTrigger("shoot");

        if (bulletPrefab != null && spawnBulletPos != null)
        {
            // Создаём пулю в точке спавна
            GameObject bullet = Instantiate(bulletPrefab, spawnBulletPos.position, spawnBulletPos.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
