using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4;
    private float rotationSpeed = 0;


    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("bullet")) Destroy(gameObject); 
        if(other.CompareTag("Player")) Destroy(gameObject);       
    }
}
