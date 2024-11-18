using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject initialDialogueObject;
    private DialogueObject currentDialogueObject;

    private void Start()
    {
        currentDialogueObject = initialDialogueObject;
    }

    public void Interact(Movement player)
    {
        player.DialogueUI.ShowDialogue(currentDialogueObject);
    }

    public void UpdateDialogue(DialogueObject newDialogue)
    {
        currentDialogueObject = newDialogue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement>().Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement>().Interactable = null;
        }
    }
}