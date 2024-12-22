using UnityEngine;

public class NotesInteraction : MonoBehaviour
{
    public GameObject notlar; 
    private bool isPlayerInRange = false;


    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            notlar.SetActive(true);
        }

        if (notlar.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            notlar.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false; 
        }
    }
}
