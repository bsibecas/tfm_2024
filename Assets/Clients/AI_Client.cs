using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
    [SerializeField] float minWait = 15f;
    [SerializeField] float maxWait = 25f;
    [SerializeField] float destroyDelay = 4f;
    [SerializeField] GameObject rageDisplayer;

    private ClientState currentState;
    private Rigidbody2D rb;
    private Material rageMaterial;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rageMaterial = rageDisplayer.GetComponent<SpriteRenderer>().material;
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
                StartCoroutine(Rage());
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

    IEnumerator Rage()
    {
        float progress = 0.0f;

        while (progress < 1)
        { 
            progress += Time.deltaTime * 0.1f;
            rageMaterial.SetFloat("_RageProgress", progress);
            yield return new WaitForEndOfFrame();
            //Debug.Log("Rage Progress: " + progress);
        }
        
        currentState = ClientState.ExitShopState;
        ManageNewState((int)currentState);

        //TO DO
        //Get mad standing still anim
        //Yell
    }

    private void ExitShop() 
    {
        rb.AddForce(Vector2.right * movSpeed);
        Destroy(gameObject, destroyDelay);
    }


    public int GetClientState()
    {
        return (int)currentState;
    }

    //DEBUG
    //private void Update()
    //{
    //    Debug.Log(currentState);
    //    Debug.Log(rageMaterial.ToString());
    //}



}
