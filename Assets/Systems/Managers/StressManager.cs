using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressManager : MonoBehaviour
{
    private static Image stressImage;
    private PlayerMovement playerMovement;
    private TimeSystem timeSystem;
    [SerializeField] float initStress = 0.1f;
    [SerializeField] float stressDistance = 2.5f;
    [SerializeField] float reliefAmount = 0.001f;
    [SerializeField] float reliefDelay = 2f;
    [SerializeField] float stressAmount = 0.01f;
    [SerializeField] float stressDelay = 1f;
    [SerializeField] int stressMinut = 1;
    [SerializeField] Transform bossTransform;

    private void OnEnable()
    {
        AI_Client.OnClientGone += IncreaseStress;
    }

    private void OnDisable()
    {
        AI_Client.OnClientGone -= IncreaseStress;
    }

    private void Awake()
    {
        stressImage = GameObject.FindGameObjectWithTag("StressAmount").GetComponent<Image>();
        playerMovement = GetComponent<PlayerMovement>();
        timeSystem = (TimeSystem)FindAnyObjectByType(typeof(TimeSystem));
    }

    private void Start()
    {
        stressImage.fillAmount = initStress;
    }

    private void Update()
    {
        if(!bossTransform)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, bossTransform.position);
        UpdateStressByBossDist(distance);
        ReliefStressByVelocity(distance);
        UpdateStressByTimeRemaining();
    }

    void UpdateStressByBossDist(float distance)
    {
        if (distance <= stressDistance)
        {
            float increment = (initStress/2) * Time.deltaTime;
            IncreaseStress(increment);
        }
    }

    void ReliefStressByVelocity(float distance)
    {
        if(!playerMovement)
        { 
            return; 
        }

        float initValue = reliefDelay;

        if (playerMovement.GetPlayerVelocity() == 0f && distance > stressDistance)
        {
            reliefDelay -= Time.deltaTime;
            if (reliefDelay <= 0f)
            {
                DecreaseStress((reliefAmount * Time.deltaTime));
            }
        }
        else
        {
            reliefDelay = initValue;
        }
    }

    void UpdateStressByTimeRemaining()
    {
        if(!timeSystem)
        {
            return;
        }

        float initValue = stressDelay;
        if(timeSystem.GetMinuts() == stressMinut)
        {
            stressDelay -= Time.deltaTime;
            if (reliefDelay <= 0f)
            {
                IncreaseStress((stressAmount * Time.deltaTime));
            }
        }
        else
        {
            stressDelay = initValue;
        }
    }

    public static void IncreaseStress(float amount)
    {
        if(stressImage.fillAmount < 1)
        {
            stressImage.fillAmount += amount;
            ClampStressProgress();
        }
    }

    public static void DecreaseStress(float amount) 
    {  
        if(stressImage.fillAmount > 0)
        {
            stressImage.fillAmount -= amount;
            ClampStressProgress();
        }
    }

    private static void ClampStressProgress()
    {
        if (stressImage.fillAmount > 1f)
        {
            stressImage.fillAmount = 1f;
        }

        if (stressImage.fillAmount < 0f)
        {
            stressImage.fillAmount = 0f;
        }     
    }

    public static float GetStressAmount()
    {
        return stressImage.fillAmount;
    }

}
