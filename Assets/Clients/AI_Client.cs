using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Client : MonoBehaviour
{
    public enum ClientState
    {
        EnterShopState = 1,
        WaitingState = 2,
        RageState = 3,
        ExitShopState = 4,
    }

    [SerializeField] float movSpeed = 50f;
    [SerializeField] float minWait = 45f;
    [SerializeField] float maxWait = 96f;
    [SerializeField] Material rageMaterial;

    private ClientState currentState;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rageMaterial = gameObject.GetComponentInChildren<Renderer>().material;
    }

    private void Start()
    {
        currentState = ClientState.EnterShopState;
        ManageNewState((int)currentState);
    }

    private void ManageNewState(int state)
    { 
        switch (state)
        {
            case (int)ClientState.EnterShopState:
                rb.AddForce(Vector2.left * movSpeed);
                break;

            case (int)ClientState.WaitingState:
                StartCoroutine(Wait());
                break;

            case (int)ClientState.RageState:
                Rage();
                break;

            case (int)ClientState.ExitShopState:
                ExitShop();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        rb.velocity = Vector2.zero;
        currentState = ClientState.WaitingState;
        ManageNewState((int)currentState);
    }

    IEnumerator Wait() 
    {
        float delay = Random.Range(minWait, maxWait);
        yield return new WaitForSeconds(delay);
        currentState = ClientState.RageState;
        ManageNewState((int)currentState);
    }

    private void Rage()
    {
        Debug.Log("RageState");
        //Sprite fills with red anim
        //Get mad standing still anim
        //Yell
        currentState = ClientState.ExitShopState;
        ManageNewState((int)currentState);
    }

    private void ExitShop() 
    {
        Debug.Log("ExitState");
        //Move right
        //Destroy once it is offscreen
    }


    public int GetClientState()
    {
        return (int)currentState;
    }


    
}
