using UnityEngine;

public class healthImage : MonoBehaviour
{
    private health healthScript;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthScript = FindAnyObjectByType<health>();
    }

    private void Update()
    {
        if (healthScript.healthInc)
        {
            animator.SetTrigger("on");
        }
    }
}
