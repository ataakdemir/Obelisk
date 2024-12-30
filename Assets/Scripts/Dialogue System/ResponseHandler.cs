using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void ShowResponses(Response[] responses, string npcId)
    {
        float responseBoxHeight = 0;

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);

            TMP_Text buttonText = responseButton.GetComponent<TMP_Text>();
            buttonText.text = $"{i + 1}. {response.ResponseText}";

            if (GameManager.Instance.IsResponseSelected(npcId, i))
            {
                buttonText.color = Color.gray;
            }

            int index = i;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(responses[index], index, npcId));

            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);

        StartCoroutine(HandleKeyboardInput(responses, npcId));
    }

    private IEnumerator HandleKeyboardInput(Response[] responses, string npcId)
    {
        while (responseBox.gameObject.activeSelf)
        {
            for (int i = 0; i < responses.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    OnPickedResponse(responses[i], i, npcId);
                    yield break;
                }
            }
            yield return null;
        }
    }

    private void OnPickedResponse(Response response, int index, string npcId)
    {
        GameManager.Instance.AddSelectedResponse(npcId, index);

        foreach (GameObject button in tempResponseButtons)
        {
            TMP_Text buttonText = button.GetComponent<TMP_Text>();
            if (buttonText.text.StartsWith($"{index + 1}."))
            {
                buttonText.color = Color.gray;
            }
        }

        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        tempResponseButtons.Clear();

        dialogueUI.ShowDialogue(response.DialogueObject, npcId);
    }

    public void ResetResponseBox()
    {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        tempResponseButtons.Clear();
    }
}
