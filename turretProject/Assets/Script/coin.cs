using UnityEngine;

public class coin : MonoBehaviour
{
    private CoinManager coinScript;

    private Animator anim;
    public GameObject obj;
    private float rotationSpeed = 180f;
    public ParticleSystem explosionVFX;
    private SphereCollider bc;
    public bool gunSpeedUp;

    private float timer = 0f;

    private void Start()
    {
        coinScript = FindAnyObjectByType<CoinManager>();
        anim = GetComponentInChildren<Animator>();
        bc = GetComponent<SphereCollider>();
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
            coinScript.UpdateCoinText();
        }
    }
}
