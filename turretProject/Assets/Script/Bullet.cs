using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
