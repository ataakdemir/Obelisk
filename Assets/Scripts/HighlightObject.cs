using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Sprite Renderer bile�eni
    private Material defaultMaterial;      // Varsay�lan materyal
    public Material highlightMaterial;     // Outline shader'l� materyal
    public float outlineScaleFactor = 0.01f; // Sprite boyutuna g�re scale

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            defaultMaterial = spriteRenderer.material;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteRenderer != null && highlightMaterial != null)
        {
            spriteRenderer.material = highlightMaterial;

            // Outline geni�li�ini sprite boyutuna g�re ayarla
            float scale = outlineScaleFactor / transform.lossyScale.x;
            spriteRenderer.material.SetFloat("_OutlineWidth", scale);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteRenderer != null)
        {
            spriteRenderer.material = defaultMaterial;
        }
    }
}
