using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart()
    {
       
        SceneManager.LoadScene("1-Level");
    }

    public void Start()
    {
        GameManager.clients = 0;
        GameManager.playerMoney = 0;
        GameManager.shopMoney = 0;
        GameManager.satisfiedClients = 0;
    }
}