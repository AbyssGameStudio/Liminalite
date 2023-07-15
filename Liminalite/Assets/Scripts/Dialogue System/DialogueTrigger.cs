using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public Dialogue[] dialogue;
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerDialogue();
            gameObject.SetActive(false);
        }
    }
    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue);
    }
}
