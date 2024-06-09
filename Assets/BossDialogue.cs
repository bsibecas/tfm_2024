using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public float interactionRange = 2.5f;
    public KeyCode interactionKey = KeyCode.E;
    public TMP_Text interactionBossText;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CheckForBossInteraction();
    }

    private void CheckForBossInteraction()
    {
        if (playerTransform == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        bool isBossNearby = distanceToPlayer <= interactionRange;

        if (isBossNearby)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                StartBossDialogue();
            }

            if (interactionBossText != null)
            {
                interactionBossText.text = "Press 'E' to talk with the boss";
            }
        }
        else
        {
            if (interactionBossText != null)
            {
                interactionBossText.text = "";
            }
        }
    }

    private void StartBossDialogue()
    {
        int nbSentences = dialogue.sentences.Length;
        int randomSentenceIndex = Random.Range(0, nbSentences);

        Dialogue tempDialogue = new Dialogue
        {
            name = dialogue.name,
            sentences = new string[] { dialogue.sentences[randomSentenceIndex] }
        };

        FindObjectOfType<DialogueManager>().StartDialogue(tempDialogue);
    }
}
