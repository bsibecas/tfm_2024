
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------------- Audio Source ----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------------- Audio Clip ----------------")]
    public AudioClip backgroundMusic;
    public AudioClip deliverOrder;
    public AudioClip finishedCooking;
    public AudioClip money;
    public AudioClip pickUpObject;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
