using UnityEngine;

public class ItemInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private string itemId; 
    [SerializeField] private string npcId; 
    [SerializeField] private DialogueObject newDialogueObjectForNPC;
    [SerializeField] private DialogueObject playerThoughtsDialogueObject;

    private bool hasUpdatedNPCDialogue = false;


    private void Start()
    {
        if (PlayerPrefs.GetInt(itemId, 0) == 1) 
        {
            Destroy(gameObject);
        }
    }

    public void Interact(Movement player)
    {
        player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, npcId, () =>
        {
            if (!hasUpdatedNPCDialogue)
            {
                GameManager.Instance.SetItemInteracted(itemId, true);

                GameManager.Instance.UpdateNPCDialogue(npcId, newDialogueObjectForNPC);

                hasUpdatedNPCDialogue = true;

                if (itemId == "cigarette")
                {
                    PlayerPrefs.SetInt(itemId, 1); 
                    PlayerPrefs.Save();

                    Destroy(gameObject);
                }
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
