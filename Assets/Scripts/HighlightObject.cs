using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private Color originalColor;         
    private SpriteRenderer objectRenderer; 

    [SerializeField] private Color highlightColor = Color.yellow;

    void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.color;
        }
        else
        {
            Debug.LogWarning("Sprite Renderer bulunamadý! Nesnenin bir Sprite Renderer olduðundan emin ol.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && objectRenderer != null)
        {
            objectRenderer.color = highlightColor; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && objectRenderer != null)
        {
            objectRenderer.color = originalColor; 
        }
    }
}
