using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 30f;

    private float timer;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer >= 7f) Destroy(gameObject);
    }
}
