using UnityEngine;

public class ItemInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private string itemId; // Bu etkile�imde kullan�lan item ID'si
    [SerializeField] private string npcId; // Bu etkile�imin ba�l� oldu�u NPC'nin ID'si
    [SerializeField] private DialogueObject newDialogueObjectForNPC; // NPC i�in g�ncellenmi� diyalog
    [SerializeField] private DialogueObject playerThoughtsDialogueObject; // Oyuncunun d���nce diyalogu

    private bool hasUpdatedNPCDialogue = false; // NPC diyaloglar� daha �nce g�ncellenmi� mi?

    public void Interact(Movement player)
    {
        // Oyuncunun d���nce diyalog kutusunu g�ster
        player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, npcId, () =>
        {
            if (!hasUpdatedNPCDialogue)
            {
                // Etkile�im durumunu g�ncelle
                GameManager.Instance.SetItemInteracted(itemId, true);

                // �lgili NPC'nin diyaloglar�n� g�ncelle
                GameManager.Instance.UpdateNPCDialogue(npcId, newDialogueObjectForNPC);

                // G�ncellemenin yap�ld���n� i�aretle
                hasUpdatedNPCDialogue = true;
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er oyuncu bu alana girerse, onun "Interactable" alan�na bu objeyi ata
        if (collision.CompareTag("Player"))
        {
            var playerMovement = collision.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.Interactable = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // E�er oyuncu bu alandan ��karsa, "Interactable" alan�n� null yap
        if (collision.CompareTag("Player"))
        {
            var playerMovement = collision.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.Interactable = null;
            }
        }
    }
}
