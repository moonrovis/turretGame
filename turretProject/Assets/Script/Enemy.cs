using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public float speed = 4;
    private float rotationSpeed = 360f;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("bullet")) Destroy(gameObject);        
    }
}
