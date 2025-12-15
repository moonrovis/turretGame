using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0,15f,0) * Time.deltaTime);
    }
}