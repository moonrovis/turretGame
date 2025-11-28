using UnityEngine;

public class gunSpeedAbility : MonoBehaviour
{
private Animator anim;
    public GameObject obj;
    private float rotationSpeed = 180f;
    public ParticleSystem explosionVFX;
    private BoxCollider bc;
    public bool gunSpeedUp;

    private float timer = 0f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        bc = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            obj.SetActive(false);
            explosionVFX.Play();
            bc.enabled = false;
            Destroy(gameObject, 1f);
            gunSpeedUp = true;

            Player player = FindAnyObjectByType<Player>();
            if(player != null)
            {
                player.GunSpeed();
            }
        }
    }
}