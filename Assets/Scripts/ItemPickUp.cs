using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private DialogueActivator npcDialogueActivator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            npcDialogueActivator.SetItemAcquired(true); // �tem al�nd���nda diyalog de�i�tir
        }
    }
}