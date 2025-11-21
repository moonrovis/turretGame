using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4;
    private float rotationSpeed = 360;

    private CapsuleCollider cap;

    public ParticleSystem explosionVFX;
    public ParticleSystem fireEngine;

    public GameObject rocket;

    private void Start()
    {
        cap = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet") || other.CompareTag("Player"))
        {
            Explode();
        }
      
    }

    private void Explode()
    {
        fireEngine.Stop();
        explosionVFX.Play();
        rocket.SetActive(false);
        Destroy(gameObject, 1f);
        cap.enabled = false;
    }
} 
