using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;

    public static DialogueManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void ShowDialogue(Dialogue dialogue) {
        OnShowDialogue?.Invoke();

        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public void HandleUpdate() {

    }

    public IEnumerator TypeDialogue(string line) {

        dialogueText.text = "";
        foreach(var letter in line.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
}
