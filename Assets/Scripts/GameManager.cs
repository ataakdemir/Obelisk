using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Dictionary<string, bool> itemInteractionStatus = new Dictionary<string, bool>();
    private Dictionary<string, DialogueObject> npcDialogues = new Dictionary<string, DialogueObject>();
    private HashSet<string> talkedToNPCs = new HashSet<string>();
    private string[] allNPCIds = { "e1", "e2", "e3", "e4", "j", "s", "sm1", "sm2", "boss" };

    private Dictionary<string, HashSet<int>> npcSelectedResponses = new Dictionary<string, HashSet<int>>();

    private void Start()
    {
        ResetGame();
    }
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

    public void AddSelectedResponse(string npcId, int responseIndex)
    {
        if (!npcSelectedResponses.ContainsKey(npcId))
        {
            npcSelectedResponses[npcId] = new HashSet<int>();
        }

        npcSelectedResponses[npcId].Add(responseIndex);
    }

    public bool IsResponseSelected(string npcId, int responseIndex)
    {
        return npcSelectedResponses.ContainsKey(npcId) && npcSelectedResponses[npcId].Contains(responseIndex);
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

    public void MarkNPCAsTalked(string npcId)
    {
        if (!talkedToNPCs.Contains(npcId))
        {
            talkedToNPCs.Add(npcId);
            Debug.Log($"NPC {npcId} ile konuþuldu. Þu anda konuþulan NPC'ler: {string.Join(", ", talkedToNPCs)}");
        }
    }

    public bool AllNPCsTalkedTo()
    {
        foreach (string npcId in allNPCIds)
        {
            if (!talkedToNPCs.Contains(npcId))
                return false;
        }
        return true;
    }

    void ResetGame()
    {
            talkedToNPCs.Clear();

            itemInteractionStatus.Clear();

            npcDialogues.Clear();

            npcSelectedResponses.Clear();

        }

}
