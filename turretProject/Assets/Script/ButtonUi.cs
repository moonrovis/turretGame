using UnityEngine;

public class ButtonUi : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip buySound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Click()
    {
        audioSource.PlayOneShot(buySound);
    }
}
