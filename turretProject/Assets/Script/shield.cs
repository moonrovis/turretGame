using System.ComponentModel;
using UnityEngine;

public class shield : MonoBehaviour
{
    private domeManager domeManagerScript;

    private AudioSource audioSource;
    public AudioClip pickUp;

    private void Start()
    {
        domeManagerScript = FindAnyObjectByType<domeManager>();
        audioSource = GetComponent<AudioSource>();
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            domeManagerScript.isDomeActive = true;
            audioSource.PlayOneShot(pickUp, 1f);
        }
    }   
}
