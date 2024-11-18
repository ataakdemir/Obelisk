using UnityEngine;

public class ItemInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueActivator npcDialogueActivator;
    [SerializeField] private DialogueObject newDialogueObjectForNPC;
    [SerializeField] private DialogueObject playerThoughtsDialogueObject;

    private bool hasUpdatedNPCDialogue = false;

    public void Interact(Movement player)
    {
        player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, () =>
        {
            if (!hasUpdatedNPCDialogue)
            {
                npcDialogueActivator.UpdateDialogue(newDialogueObjectForNPC);
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
