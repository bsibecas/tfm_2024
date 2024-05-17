using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AI_Client : MonoBehaviour
{
    public enum ClientState
    {
        WalkingState = 1,
        WaitingState = 2,
        RageState = 3,
        ExitShopState = 4,
    }

    [SerializeField] float movSpeed = 15f;
    [SerializeField] float minWait = 15f;
    [SerializeField] float maxWait = 25f;
    [SerializeField] float destroyDelay = 4f;
    [SerializeField] GameObject rageDisplayer;

    private ClientState currentState;
    private Material rageMaterial;
    private Transform[] goToPositions;
    private bool isWalking = false;
    private int currentTargetID = 0;

    private void Awake()
    {
        rageMaterial = rageDisplayer.GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        InitGoToPositions();

        currentState = ClientState.WalkingState;
        ManageNewState((int)currentState);
    }

    private void InitGoToPositions()
    {
        GameObject positionsContainer = GameObject.Find("ClientTargetPoints");
        goToPositions = new Transform[positionsContainer.transform.childCount];

        for (int i = 0; i < goToPositions.Length; i++)
        {
            goToPositions[i] = positionsContainer.transform.GetChild(i);
            //Debug.Log(goToPositions[i].name);
        }
    }

    private void ManageNewState(int state)
    { 
        switch (state)
        {
            case (int)ClientState.WalkingState:
                isWalking = true;
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

    private void Update()
    {
        if(isWalking && currentTargetID < goToPositions.Length)
        {
            EnterMovement();

            if (transform.position == goToPositions[goToPositions.Length - 1].position)
            {
                isWalking = false;
                currentState = ClientState.WaitingState;
                ManageNewState((int)currentState);
            }
        }

    }

    private void EnterMovement()
    {
        GoToTargetPoint(currentTargetID);
        if (Vector2.Distance(transform.position, goToPositions[currentTargetID].position) <= 0.5f)
        {
            if (currentTargetID != goToPositions.Length - 1) { currentTargetID++; }
            GoToTargetPoint(currentTargetID);
        }
    }

    private void GoToTargetPoint(int ID)
    {
        transform.position = Vector2.MoveTowards(transform.position, goToPositions[ID].position, movSpeed * Time.deltaTime*0.1f);
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
