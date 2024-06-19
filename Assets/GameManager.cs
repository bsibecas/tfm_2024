using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int playerMoney = 0;
    public static int shopMoney = 0;
    public static int clients = 0;
    public static int satisfiedClients = 0;
    public static int days = 0;
    public static int minClients = days + 2;
    public static bool furnaceUpgraded = false;
    public static int minPlayerMoney = days * 10 + 20;
}
