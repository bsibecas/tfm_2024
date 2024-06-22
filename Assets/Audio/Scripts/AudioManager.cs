
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------------- Audio Source ----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------------- Audio Clip ----------------")]
    public AudioClip backgroundMusic;
    public AudioClip specialAreaMusic;
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

    public void PlayBackgroundMusic()
    {
        if (musicSource.clip != backgroundMusic)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlaySpecialAreaMusic()
    {
        if (specialAreaMusic != null)
        {
            if (musicSource.clip != specialAreaMusic)
            {
                musicSource.clip = specialAreaMusic;
                musicSource.Play();
            }
        }
    }
}
