using UnityEngine;

public class health : MonoBehaviour
{
    private Animator anim;
    public GameObject obj;

    private float rotationSpeed = 180f;

    public ParticleSystem explosionVFX;

    private Player playerScript;
    private bar barScript;

    public bool healthInc = false;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerScript = FindAnyObjectByType<Player>();
        barScript = FindAnyObjectByType<bar>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            healthInc = true;
            Invoke(nameof(resetBool), 0.5f);
            obj.SetActive(false);
            explosionVFX.Play();
            Destroy(gameObject, 1f);
            barScript.healthBar += 0.25f;
            if(barScript.healthBar >= 1) barScript.healthBar = 1;
            barScript.healthImg.fillAmount = barScript.healthBar;
        }
    }

    private void resetBool()
    {
        healthInc = false;
    }
}
