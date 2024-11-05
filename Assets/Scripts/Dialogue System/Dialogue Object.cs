using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;

    public bool hasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
