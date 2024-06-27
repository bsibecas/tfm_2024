using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{
    public TMP_Text playerMoneyText;
    public TMP_Text shopMoneyText;
    public TMP_Text tasksCompletedText;
    public TMP_Text dayCompletedText;

    void Start()
    {
        UpdateStatistics();
        GameManager.days++;
    }

    public void Update()
    {
        UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        if (dayCompletedText != null)
        {
            dayCompletedText.text = "Day:" + GameManager.days.ToString();
        }

        if (playerMoneyText != null)
        {
            playerMoneyText.text = "Salary earned: " + GameManager.playerMoney.ToString() + "$";
        }

        if (shopMoneyText != null)
        {
            shopMoneyText.text = "Shop benefits: " + GameManager.shopMoney.ToString() + "$";
        }

        if (GameManager.satisfiedClients > (GameManager.clients - 1))
        {
            if (tasksCompletedText != null)
            {
                tasksCompletedText.text = "Satisfied clients: " + GameManager.satisfiedClients.ToString() + "/" + GameManager.clients.ToString();
            }
        } else
        {
            if (tasksCompletedText != null)
            {
                tasksCompletedText.text = "Satisfied clients: " + GameManager.satisfiedClients.ToString() + "/" + (GameManager.clients - 1).ToString();
            }
        }

        
    }
}
