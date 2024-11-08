using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject itemAcquiredDialogueObject;

    private DialogueObject activeDialogueObject;
    private bool itemAcquired = false; // �tem al�nd���nda true yap�lacak

    private void Start()
    {
        activeDialogueObject = dialogueObject; // Ba�lang��ta varsay�lan diyalog
    }

    public void SetItemAcquired(bool acquired)
    {
        itemAcquired = acquired;
        activeDialogueObject = itemAcquired ? itemAcquiredDialogueObject : dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Movement player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Movement player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(Movement player)
    {
        player.DialogueUI.ShowDialogue(activeDialogueObject);
    }
}
