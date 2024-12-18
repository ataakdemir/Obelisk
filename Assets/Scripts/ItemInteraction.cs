using UnityEngine;

public class ItemInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private string itemId;
    [SerializeField] private string npcId; 
    [SerializeField] private DialogueObject newDialogueObjectForNPC;
    [SerializeField] private DialogueObject playerThoughtsDialogueObject;


    private bool hasUpdatedNPCDialogue = false;

    public void Interact(Movement player)
    {
        player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, () =>
        {
            if (!hasUpdatedNPCDialogue)
            {
                GameManager.Instance.SetItemInteracted(itemId, true); // Mark item as interacted
                GameManager.Instance.UpdateNPCDialogue(npcId, newDialogueObjectForNPC); // Update NPC dialogue
                hasUpdatedNPCDialogue = true;
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Movement>().Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Movement>().Interactable = null;
        }
    }
}
