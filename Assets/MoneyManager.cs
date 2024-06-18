using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TMP_Text playerMoneyText;
    public TMP_Text shopMoneyText;
    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        UpdateMoneyText();
    }

    public void AddMoney(int itemPrice)
    {
        GameManager.playerMoney += Mathf.RoundToInt(itemPrice * 0.20f);
        GameManager.shopMoney += itemPrice - Mathf.RoundToInt(itemPrice * 0.20f);
        audioManager.PlaySFX(audioManager.money);
        UpdateMoneyText();
    }

    public void SubstractPlayerMoney(int itemPrice)
    {
        GameManager.playerMoney = GameManager.playerMoney - itemPrice;
        audioManager.PlaySFX(audioManager.money);
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
