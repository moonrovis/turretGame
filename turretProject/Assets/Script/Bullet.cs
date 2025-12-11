using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 50f;

    private float timer;

    private void Update()
    {
        transform.Translate(Vector3.back * -speed * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer >= 7f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("enemy") || other.CompareTag("bomb") || other.CompareTag("coin")
         || other.CompareTag("gunSpeed")|| other.CompareTag("health")) Destroy(gameObject);
    }
}
