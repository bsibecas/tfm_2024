using UnityEngine;

public class EventManager : MonoBehaviour
{
    public ObstacleSpawner puddleSpawner;
    public ObstacleSpawner trashSpawner;
    float randomValue = 0;

    private void Start()
    {
        TriggerEvent();
    }

    public void TriggerEvent()
    {

        if (GameManager.days > 2)
        {
            randomValue = Random.value;

            if (randomValue <= 0.2f)
            {
                trashSpawner.TriggerObstacleEvent();
            }
        } else if (GameManager.days > 4)
        {
            randomValue = Random.value;

            if (randomValue <= 0.3f)
            {
                trashSpawner.TriggerObstacleEvent();
            } else if (randomValue >= 0.9f)
            {
                puddleSpawner.TriggerObstacleEvent();

            }
        }
       
    }
}
