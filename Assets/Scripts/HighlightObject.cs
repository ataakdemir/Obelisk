using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Sprite Renderer bileþeni
    private Material defaultMaterial;      // Varsayýlan materyal
    public Material highlightMaterial;     // Outline shader'lý materyal
    public float outlineScaleFactor = 0.01f; // Sprite boyutuna göre scale

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

            // Outline geniþliðini sprite boyutuna göre ayarla
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
