using UnityEngine;

public class ItemInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueActivator npcDialogueActivator;
    [SerializeField] private DialogueObject newDialogueObjectForNPC;
    [SerializeField] private DialogueObject playerThoughtsDialogueObject; 

    private bool hasInteracted = false;

    public void Interact(Movement player)
    {
        if (!hasInteracted)
        {
            hasInteracted = true;

            player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, () =>
            {
                npcDialogueActivator.UpdateDialogue(newDialogueObjectForNPC);

            });
        }
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
