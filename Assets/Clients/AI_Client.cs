using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Client : MonoBehaviour
{
    public enum ClientState
    {
        WalkingState = 1,
        WaitingState = 2,
        RageState = 3,
        ExitShopState = 4,
    }

    public delegate void ClientExitHandler(AI_Client client);
    public event ClientExitHandler OnClientExit;

    public delegate void PositionReachedHandler(Transform position);
    public event PositionReachedHandler OnPositionReached;

    [SerializeField] private float movSpeed = 4f;
    [SerializeField] private float minWait = 15f;
    [SerializeField] private float maxWait = 25f;
    [SerializeField] private float destroyDelay = 4f;
    [SerializeField] private GameObject rageDisplayer;
    [SerializeField] private GenerateRandomTasks clientTasks;
    [SerializeField] private  Image[] CheckImages;


    private ClientState currentState;
    private Material rageMaterial;
    private Transform[] goToPositions;
    private bool isWalking = false;
    private int currentTargetID = 0;
    private Transform targetPosition;
    private ClientManager clientManager;
    public Animator animator;

    private void Awake()
    {
        clientTasks = GetComponent<GenerateRandomTasks>();

        rageMaterial = rageDisplayer.GetComponent<SpriteRenderer>().material;

        GameObject animatorObject = GameObject.FindWithTag("TaskAnimator");
        if (animatorObject != null)
        {
            animator = animatorObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator component not found on the GameObject with tag 'TaskAnimator'.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject with tag 'TaskAnimator' not found.");
        }
        AssignCheckImages();
    }

    void AssignCheckImages()
    {
        GameObject[] emptyPlaceObjects = GameObject.FindGameObjectsWithTag("EmptyCheck");
        CheckImages = new Image[emptyPlaceObjects.Length];
        for (int i = 0; i < emptyPlaceObjects.Length; i++)
        {
            CheckImages[i] = emptyPlaceObjects[emptyPlaceObjects.Length - 1 - i].GetComponent<Image>();
        }
    }

    private void Start()
    {
        InitGoToPositions();
        MoveToNextAvailableSpawnPoint();
    }

    public void SetGoToPositions(Transform[] positions)
    {
        goToPositions = positions;
    }

    public void SetClientManager(ClientManager manager)
    {
        clientManager = manager;
    }

    private void InitGoToPositions()
    {
        GameObject positionsContainer = GameObject.Find("ClientTargetPoints");
        List<Transform> availablePositions = new List<Transform>();

        for (int i = 0; i < positionsContainer.transform.childCount; i++)
        {
            Transform child = positionsContainer.transform.GetChild(i);
            EmptyPlace targetPoint = child.GetComponent<EmptyPlace>();
            if (targetPoint != null && targetPoint.emptyPlace)
            {
                availablePositions.Add(child);
            }
        }

        goToPositions = availablePositions.ToArray();
    }

    private void MoveToNextAvailableSpawnPoint()
    {
        if (goToPositions.Length == 0) return;

        int nextIndex = -1;
        for (int i = 0; i < goToPositions.Length; i++)
        {
            if (goToPositions[i].GetComponent<EmptyPlace>().emptyPlace)
            {
                nextIndex = i;
                break;
            }
        }

        if (nextIndex != -1)
        {
            targetPosition = goToPositions[nextIndex];
            currentTargetID = nextIndex;
            currentState = ClientState.WalkingState;
            ManageNewState((int)currentState);
        }
        else
        {
            currentState = ClientState.ExitShopState;
            ManageNewState((int)currentState);
        }
    }

    private void ManageNewState(int state)
    {
        switch ((ClientState)state)
        {
            case ClientState.WalkingState:
                isWalking = true;
                StartCoroutine(MoveToTarget(targetPosition, movSpeed));
                break;
            case ClientState.WaitingState:
                StartCoroutine(Wait());
                break;
            case ClientState.RageState:
                StartCoroutine(Rage());
                break;
            case ClientState.ExitShopState:
                StartCoroutine(MoveAndExit());
                break;
        }
    }

    private void Update()
    {
        VerifyOrderList();
    }

    private void VerifyOrderList()
    {
        GameObject[] orderedItems = clientTasks.GetOrderList();

        int tasksCompleted = 0;
        for (int x = 0; x < orderedItems.Length; x++)
        {
            if (orderedItems[x] == null)
            {
                tasksCompleted++;
            }
        }
        if (tasksCompleted == orderedItems.Length)
        {
            currentState = ClientState.ExitShopState;
            ManageNewState((int)currentState);
        }
    }

    private IEnumerator MoveToTarget(Transform target, float speed)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = target.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (isWalking)
        {
            float coveredDistance = (Time.time - startTime) * speed;
            float fractionOfJourney = coveredDistance / distance;
            transform.position = Vector3.Lerp(startPosition, targetPosition, EaseInOutQuad(fractionOfJourney));

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isWalking = false;
                OnPositionReached?.Invoke(target);
                EmptyPlace targetPoint = target.GetComponent<EmptyPlace>();
                if (targetPoint != null)
                {
                    targetPoint.emptyPlace = false;
                }
                currentState = ClientState.WaitingState;
                ManageNewState((int)currentState);
            }

            yield return null;
        }
    }

    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
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
            progress += Time.deltaTime * 0.01f;
            rageMaterial.SetFloat("_RageProgress", progress);
            yield return new WaitForEndOfFrame();
        }

        currentState = ClientState.ExitShopState;
        ManageNewState((int)currentState);
    }

    private IEnumerator MoveAndExit()
    {
        float moveDuration = 2f;
        float moveDistance = 8f;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Vector3 exitTargetPosition = startingPosition + Vector3.right * moveDistance;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startingPosition, exitTargetPosition, EaseInOutQuad(elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = exitTargetPosition;

        // Ensure targetPosition is not null before accessing its component
        if (targetPosition != null)
        {
            EmptyPlace targetPoint = targetPosition.GetComponent<EmptyPlace>();
            if (targetPoint != null)
            {
                targetPoint.emptyPlace = true;
            }
        }

        OnClientExit?.Invoke(this);

        animator.SetBool("isOpen", false);
        for (int i = 0; i < CheckImages.Length; i++)
        {
            Color color = CheckImages[i].color;
            color.a = 0f;
            CheckImages[i].color = color;
        }

        DestroyTaskAnimations();

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    private void DestroyTaskAnimations()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("TaskAnimator") && child.GetComponent<Item>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }


    public int GetClientState()
    {
        return (int)currentState;
    }
}
