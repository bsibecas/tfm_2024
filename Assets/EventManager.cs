using UnityEngine;

public class EventManager : MonoBehaviour
{
    public PuddleSpawner puddleSpawner;

    private void Start()
    {
        TriggerEvent();
    }

    public void TriggerEvent()
    {
        float randomValue = Random.value;

        if (randomValue <= 0.2f)
        {
            puddleSpawner.TriggerPuddleEvent();
            Debug.Log("Puddle event triggered!");
        }
        else
        {
            Debug.Log("No event triggered.");
        }
    }
}
