using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject[] endings; // Ending GameObject'lerini Inspector'dan s�r�kleyip b�rak�n.

    void Start()
    {
        // PlayerPrefs'ten oyuncunun se�ti�i ending index'ini al
        int selectedEnding = PlayerPrefs.GetInt("SelectedEnding", 0); // Varsay�lan olarak Ending 0

        // Se�ilen ending'i aktif hale getir
        ShowEnding(selectedEnding);
    }

    public void ShowEnding(int endingIndex)
    {
        // T�m ending nesnelerini devre d��� b�rak
        foreach (GameObject ending in endings)
        {
            ending.SetActive(false);
        }

        // E�er ge�erli bir index girdisi varsa se�ilen ending'i aktif hale getir
        if (endingIndex >= 0 && endingIndex < endings.Length)
        {
            endings[endingIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Ge�ersiz ending index'i: " + endingIndex);
        }
    }
}
