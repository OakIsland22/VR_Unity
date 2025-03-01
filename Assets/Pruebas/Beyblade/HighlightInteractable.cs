using UnityEngine;

public class HighlightInteractable : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    public Color highlightColor = Color.white; 

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void OnHoverEnter()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.SetColor("_EmissionColor", highlightColor);
        }
    }

    public void OnHoverExit()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.SetColor("_EmissionColor", originalColor);
        }
    }
}