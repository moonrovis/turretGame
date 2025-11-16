using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform turretTransform; // Башня танка
    public float rotationSpeed = 5f;  // Скорость поворота (настраивается в Inspector)

    private Rigidbody rb;
    private Animator anim;

    public Transform spawnBulletPos;
    public GameObject bullet;

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
        anim.SetTrigger("shoot");

        Instantiate(bullet, spawnBulletPos.position, turretTransform.rotation);
    }
}
