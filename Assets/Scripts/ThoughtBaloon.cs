using UnityEngine;

public class ThoughtBaloon : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject playerThoughtsDialogueObject;

    public void Interact(Movement player)
    {
        if (playerThoughtsDialogueObject != null)
        {
            player.DialogueUI.ShowDialogue(playerThoughtsDialogueObject, null); 
        }
        else
        {
            Debug.LogWarning("Player thoughts dialogue object is not assigned!", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.Interactable = this;
            }
            else
            {
                Debug.LogError("Player object does not have a Movement component!", collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.Interactable = null;
            }
        }
    }
}
