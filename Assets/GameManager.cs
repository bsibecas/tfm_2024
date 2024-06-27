using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public static int playerMoney = 0;
    public static int shopMoney = 0;
    public static int clients = 0;
    public static int satisfiedClients = 0;
    public static int days = 0;
    public static int minClients = days + 2;
    public static bool furnaceUpgraded = false;
    public static bool firedByStress = false;
    public static int minPlayerMoney = days * 10 + 20;

    public static float musicVolume = 0.70f;
    public static float SFXVolume = 0.70f;
    [SerializeField] private AudioMixer myMixer;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolume();
    }

    public void SaveVolume(float SFXValue, float musicValue)
    {
        PlayerPrefs.SetFloat("musicVolume", musicValue);
        musicVolume = musicValue;
        PlayerPrefs.SetFloat("SFXVolume", SFXValue);
        SFXVolume = SFXValue;
        LoadVolume();
    }

    public void LoadVolume()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        myMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        myMixer.SetFloat("SFX", Mathf.Log10(SFXVolume) * 20);
    }
}
