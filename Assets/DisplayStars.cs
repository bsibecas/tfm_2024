using UnityEngine;
using UnityEngine.UI;

public class DisplayStars : MonoBehaviour
{
    public RawImage[] childImages;

    void Start()
    {
        RawImage[] rawImages = GetComponentsInChildren<RawImage>();

        childImages = new RawImage[rawImages.Length];

        for (int i = 0; i < rawImages.Length; i++)
        {
            childImages[i] = rawImages[i];
        }

        Debug.Log("Found " + childImages.Length + " child raw images.");
    }
    private void Update()
    {
        VerifyStars();
    }

   

    private void VerifyStars()
    {
        bool starTwo = false;
        if (GameManager.satisfiedClients >= GameManager.minClients - 1)
        {
            Color colorStarOne = childImages[0].color;
            colorStarOne.a = 1f;
            childImages[0].color = colorStarOne;

            if ((GameManager.clients - 1) == GameManager.satisfiedClients)
            {
                Color colorStarTwo = childImages[1].color;
                colorStarTwo.a = 1f;
                childImages[1].color = colorStarTwo;
                starTwo = true;

            }
            if (GameManager.playerMoney >= GameManager.minPlayerMoney)
            {
                if (starTwo == false)
                {
                    Color colorStarTwo = childImages[1].color;
                    colorStarTwo.a = 1f;
                    childImages[1].color = colorStarTwo;
                }
                else
                {
                    Color colorStarThree = childImages[2].color;
                    colorStarThree.a = 1f;
                    childImages[2].color = colorStarThree;
                }

            }

        }
    }


}