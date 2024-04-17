using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    private float castingTime;
    private bool activatedTime = false;

    public float MaximumCastingTime = 300f;
    public Slider slider;
    public GameObject Canvas;

    private void Start()
  {
        ActiveTime();
  }


    void Update()
    {
        if(activatedTime)
        {
            CheckTime();
        }
    }

    private void CheckTime()
    {
        castingTime -= Time.deltaTime;

        if(castingTime >=0)
        {
            slider.value = castingTime;
        }
        if(castingTime <= 0)
        {
            Debug.Log("Final");
            StatusTimeChange(false);
        }
    }

    private void StatusTimeChange(bool estado)
    {
        activatedTime = estado;
    }

    public void ActiveTime()
    {
        castingTime = MaximumCastingTime;
        slider.maxValue = MaximumCastingTime;
        StatusTimeChange(true);
    }

    public void DeactiveTime()
    {
        StatusTimeChange(false);
    }

    public float GetCastingTime()
    {
        return castingTime;
    }

    public void ShowTasksCanvas()
    {
        if (Canvas != null)
        {
            Canvas.SetActive(true);
        }
    }

}
