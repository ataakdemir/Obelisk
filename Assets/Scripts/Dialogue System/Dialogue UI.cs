using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    public TMP_Text textLabel;
    public GameObject dialogueBox;

    public bool isOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    private Action onDialogueComplete;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject, string npcId, Action onDialogueComplete = null)
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        this.onDialogueComplete = onDialogueComplete;
        StartCoroutine(StepThroughDialogue(dialogueObject, npcId));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject, string npcId)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.hasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.hasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses, npcId);
        }
        else
        {
            CloseDialogueBox();

            onDialogueComplete?.Invoke();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void ResetDialogue()
    {
        StopAllCoroutines();
        CloseDialogueBox(); 

        if (responseHandler != null)
        {
            responseHandler.ResetResponseBox(); 
        }

        onDialogueComplete = null; 
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
