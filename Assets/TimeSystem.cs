using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    private float MaximumCastingTime = 300f;

    private float castingTime;

    private bool activatedTime = false;

    public Slider slider;
  
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
    


}
