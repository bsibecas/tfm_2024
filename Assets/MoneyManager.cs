using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TMP_Text playerMoneyText;
    public TMP_Text shopMoneyText;

    void Start()
    {
        UpdateMoneyText();
    }

    public void AddMoney(int itemPrice)
    {
        GameManager.playerMoney += Mathf.RoundToInt(itemPrice * 0.20f);
        GameManager.shopMoney += itemPrice - Mathf.RoundToInt(itemPrice * 0.20f);
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        if (playerMoneyText != null)
        {
            playerMoneyText.text = "Player Money: $" + GameManager.playerMoney.ToString();
        }

        if (shopMoneyText != null)
        {
            shopMoneyText.text = "Shop Money: $" + GameManager.shopMoney.ToString();
        }
    }
}
