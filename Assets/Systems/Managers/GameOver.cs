using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject [] firedTxts;

    void Start()
    {
        if(!GameManager.firedByStress)
        {
            firedTxts[0].SetActive(true);
        }
        else
        {
            firedTxts[1].SetActive(true);
        }
    }
}
