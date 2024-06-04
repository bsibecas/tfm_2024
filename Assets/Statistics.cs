using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{
    public TMP_Text playerMoneyText;
    public TMP_Text shopMoneyText;
    public TMP_Text tasksCompletedText;

    void Start()
    {
        UpdateStatistics();
    }

    public void Update()
    {
        UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        if (playerMoneyText != null)
        {
            playerMoneyText.text = "Player Money: $" + GameManager.playerMoney.ToString();
        }

        if (shopMoneyText != null)
        {
            shopMoneyText.text = "Shop Money: $" + GameManager.shopMoney.ToString();
        }

        if (tasksCompletedText != null)
        {
            tasksCompletedText.text = GameManager.tasksCompleted.ToString() + "/0 tasks completed";
        }
    }
}
