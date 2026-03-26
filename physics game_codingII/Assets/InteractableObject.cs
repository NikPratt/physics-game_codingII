using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Color highlightedColor = new Color(r: 1f, g: 95, b: .6f);
    [Range(0f, 1f)] public float highlightStrength = .4f;

    private Renderer objectRenderer;

    private Color originalColor;

    private bool isHighlighted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.sharedMaterial.color;
        }

        else
        {
            Debug.Log($"Interactable object on {gameObject.name} has no renderer. highlight worn work");
        }
    }
    public void Highlight()
    {
        if (isHighlighted || objectRenderer == null)
        {
            Debug.Log("no rednerer");

        }
        objectRenderer.material.color = Color.Lerp(originalColor, highlightedColor, highlightStrength);

        isHighlighted = true;
    }


    public void Unhighlight()
    {
        if (!isHighlighted && objectRenderer == null) return;
        objectRenderer.material.color = originalColor;
        isHighlighted = false;
    }
}   