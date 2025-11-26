using UnityEngine;

public class health : MonoBehaviour
{
    public static event System.Action OnHealthIncreased; // Статическое событие

    private Animator anim;
    public GameObject obj;
    private float rotationSpeed = 180f;
    public ParticleSystem explosionVFX;
    private BoxCollider bc;
    private bar barScript;
    public bool healthInc = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        barScript = FindAnyObjectByType<bar>();
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
            if (barScript.healthBar < 1f)
            {
                barScript.healthBar = Mathf.Min(1f, barScript.healthBar + 0.25f);
                barScript.healthImg.fillAmount = barScript.healthBar;

                healthInc = true;
                OnHealthIncreased?.Invoke(); // Уведомляем, что здоровье увеличилось
                Invoke(nameof(resetBool), 1f);
            }
            else
            {
                barScript.healthBar = 1f;
            }

            obj.SetActive(false);
            explosionVFX.Play();
            bc.enabled = false;
            Destroy(gameObject, 1f);
        }
    }

    private void resetBool()
    {
        healthInc = false;
    }
}
