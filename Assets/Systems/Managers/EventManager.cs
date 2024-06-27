using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public ObstacleSpawner puddleSpawner;
    public ObstacleSpawner trashSpawner;
    public Dialogue trashDialogue;
    public Dialogue puddleDialogue;


    private void Start()
    {
        TriggerEvent();
    }

    public void TriggerEvent()
    {

        if (GameManager.days == 1)
        {
            trashSpawner.TriggerObstacleEvent();
            StartCoroutine(TrashDialogue());
        }
        else if (GameManager.days == 3)
        {
            puddleSpawner.TriggerObstacleEvent();
            StartCoroutine(PuddleDialogue());
        }
    }

    private IEnumerator TrashDialogue()
    {
        yield return null;
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueManager>().StartDialogue(trashDialogue);
    }

    private IEnumerator PuddleDialogue()
    {
        yield return null;
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueManager>().StartDialogue(puddleDialogue);
    }
}
