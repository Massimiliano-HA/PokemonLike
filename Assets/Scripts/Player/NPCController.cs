using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{

    [SerializeField] Dialogue dialogue;
    public void Interact()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        
        if(player != null) {
            player.LookForward(transform.position);
            Debug.Log("NPC is looking forward.");
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        }
    }
}
