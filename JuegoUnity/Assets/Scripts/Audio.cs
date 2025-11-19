using UnityEngine;
using UnityEngine.InputSystem;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayMusic();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StopMusic();    
        }

    }

    public void PlayMusic()
    {
        audioSource.Play();
        Debug.Log("Reproduciendo música");
    }

    public void StopMusic()
    {
        audioSource.Stop();
        Debug.Log("Parando música");
    }
}
