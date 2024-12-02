using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Dictionary<string, bool> itemInteractionStatus = new Dictionary<string, bool>();
    private Dictionary<string, DialogueObject> npcDialogues = new Dictionary<string, DialogueObject>();
  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void SetItemInteracted(string itemId, bool interacted)
    {
        itemInteractionStatus[itemId] = interacted;
        Debug.Log($"Item {itemId} interaction status updated to {interacted}");
    }

    public bool GetItemInteracted(string itemId)
    {
        return itemInteractionStatus.ContainsKey(itemId) && itemInteractionStatus[itemId];
    }

    public void UpdateNPCDialogue(string npcId, DialogueObject newDialogue)
    {
        if (npcDialogues.ContainsKey(npcId))
        {
            npcDialogues[npcId] = newDialogue;
        }
        else
        {
            npcDialogues.Add(npcId, newDialogue);
        }
    }

    public DialogueObject GetNPCDialogue(string npcId)
    {
        return npcDialogues.ContainsKey(npcId) ? npcDialogues[npcId] : null;
    }
}
