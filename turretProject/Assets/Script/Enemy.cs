using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private CapsuleCollider cap;
    private Player playerScript;

    public ParticleSystem explosionVFX;
    public ParticleSystem fireEngine;

    public GameObject rocket;

    private GameManager gameManagerScript;
    
    private void Start()
    {
        cap = GetComponent<CapsuleCollider>();
        playerScript = FindAnyObjectByType<Player>();
        gameManagerScript = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (playerScript.isAlive && !gameManagerScript.isPause)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            speed = 0f;
            rotationSpeed = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet") || other.CompareTag("Player"))
        {
            Exploide();
        }
    }

    private void Exploide()
    {
        cap.enabled = false;
        fireEngine.Stop();
        explosionVFX.Play();
        rocket.SetActive(false);
        Destroy(gameObject, 1f);
    }
} 
