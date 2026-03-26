using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Color highlightColor = new Color(r: 1f, g:95, b:.6f)

    [Range(0f, 1f)] public float highlightStrength = .4f;

    private RenderBuffer objectRenderer;
    private Color originalColor;
    private bool isHighlighted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)

       originalColor = object.render.shadedMaterial.color;
    }
    else
    {
        Debug.Log
    }
    
    public void Highlight()
    {
        Debug.Log("no obj render & ishighlighted is true");
        return;
    }
    // Update is called once per frame
    objectRenderer.material.color = Color.Lerp(originalColor, highlightColor, highlightStrength);
    isHighlighted = true;
}


public void Unhighlight()
{
    if (!isHighlighted || objectRender == null) return;
    objectRender.material.color = originalColor;
    isHighlited = false;
}
