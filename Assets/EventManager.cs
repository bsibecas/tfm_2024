using UnityEngine;

public class EventManager : MonoBehaviour
{
    public ObstacleSpawner puddleSpawner;
    public ObstacleSpawner trashSpawner;

    private void Start()
    {
        TriggerEvent();
    }

    public void TriggerEvent()
    {
        float randomValue = Random.value;

        if (randomValue <= 0.2f)
        {
            puddleSpawner.TriggerObstacleEvent();
            Debug.Log("Puddle event triggered!");
        }
        else if (randomValue <= 0.5f)
        {
            trashSpawner.TriggerObstacleEvent();
            Debug.Log("Trash event triggered!");
        }
        else
        {
            Debug.Log("No event triggered.");
        }
    }
}
