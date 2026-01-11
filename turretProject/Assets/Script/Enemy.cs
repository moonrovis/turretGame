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

    private AudioSource audioSource;
    public AudioClip[] explosionSound;
    
    private void Start()
    {
        cap = GetComponent<CapsuleCollider>();
        playerScript = FindAnyObjectByType<Player>();
        gameManagerScript = FindAnyObjectByType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerScript.isAlive && !gameManagerScript.isPause)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if(!playerScript.isAlive && gameManagerScript.isPause)
        {
            speed = 0f;
            rotationSpeed = 0f;
        }

        if(gameManagerScript.isRewarded) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet") || other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(explosionSound[Random.Range(0, explosionSound.Length)],1f);
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
