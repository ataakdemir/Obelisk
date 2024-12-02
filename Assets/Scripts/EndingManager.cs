using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject[] endings; // Ending GameObject'lerini Inspector'dan sürükleyip býrakýn.

    void Start()
    {
        // PlayerPrefs'ten oyuncunun seçtiði ending index'ini al
        int selectedEnding = PlayerPrefs.GetInt("SelectedEnding", 0); // Varsayýlan olarak Ending 0

        // Seçilen ending'i aktif hale getir
        ShowEnding(selectedEnding);
    }

    public void ShowEnding(int endingIndex)
    {
        // Tüm ending nesnelerini devre dýþý býrak
        foreach (GameObject ending in endings)
        {
            ending.SetActive(false);
        }

        // Eðer geçerli bir index girdisi varsa seçilen ending'i aktif hale getir
        if (endingIndex >= 0 && endingIndex < endings.Length)
        {
            endings[endingIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Geçersiz ending index'i: " + endingIndex);
        }
    }
}
