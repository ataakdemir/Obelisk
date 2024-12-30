using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private string npcId;
    [SerializeField] private DialogueObject initialDialogueObject;

    public void Interact(Movement player)
    {
        DialogueObject currentDialogueObject = GameManager.Instance.GetNPCDialogue(npcId) ?? initialDialogueObject;
        player.DialogueUI.ShowDialogue(currentDialogueObject, npcId);

        GameManager.Instance.MarkNPCAsTalked(npcId);
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
