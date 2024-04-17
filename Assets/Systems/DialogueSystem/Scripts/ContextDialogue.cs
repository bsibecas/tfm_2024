using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    private IEnumerator Start()
    {
        yield return null;
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
