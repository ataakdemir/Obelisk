using UnityEngine;

public class ItemInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private string itemId; // Bu etkileþimde kullanýlan item ID'si
    [SerializeField] private string npcId; // Bu etkileþimin baðlý olduðu NPC'nin ID'si
    [SerializeField] private DialogueObject newDialogueObjectForNPC; // NPC için güncellenmiþ diyalog
    [SerializeField] private DialogueObject playerThoughtsDialogueObject; // Oyuncunun düþünce diyalogu

    private bool hasUpdatedNPCDialogue = false; // NPC diyaloglarý daha önce güncellenmiþ mi?

    public void Interact(Movement player)
    {
        // Oyuncunun düþünce diyalog kutusunu göster
        player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, npcId, () =>
        {
            if (!hasUpdatedNPCDialogue)
            {
                // Etkileþim durumunu güncelle
                GameManager.Instance.SetItemInteracted(itemId, true);

                // Ýlgili NPC'nin diyaloglarýný güncelle
                GameManager.Instance.UpdateNPCDialogue(npcId, newDialogueObjectForNPC);

                // Güncellemenin yapýldýðýný iþaretle
                hasUpdatedNPCDialogue = true;
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer oyuncu bu alana girerse, onun "Interactable" alanýna bu objeyi ata
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
        // Eðer oyuncu bu alandan çýkarsa, "Interactable" alanýný null yap
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
