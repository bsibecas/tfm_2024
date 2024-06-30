using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public void SetUpNewGame()
    {
        GameManager.clients = 0;
        GameManager.playerMoney = 0;
        GameManager.shopMoney = 0;
        GameManager.satisfiedClients = 0;
        GameManager.days = 0;
    }
}
