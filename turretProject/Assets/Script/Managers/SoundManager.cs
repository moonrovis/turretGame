using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    
    // Ссылка на текущий экземпляр
    private static SoundManager instance;

    private void Awake()
    {
        // Проверяем, есть ли уже такой объект
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при смене сцены

            // Настройка AudioSource
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }
}
