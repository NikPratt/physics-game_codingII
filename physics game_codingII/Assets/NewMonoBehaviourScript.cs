using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabber : MonoBehaviour
{
    [Header("Grab Settings")]
    [Tooltip("Maximum distance to grab an object")]
    public float grabRange = 4f;
    [Tooltip("How fast the held object moves to the hold point. higher = snappier")]
    public float holdSmoothing = 15f;
    public float throwForce = 15f;

    [Header("References")]
    public Transform holdPoint;
    public Transform cameraTransform; // Added this since it was used but not defined

    private Rigidbody heldRigidbody;
    private InteractableObject currentHighlight;
    private bool isHolding = false;

    void FixedUpdate()
    {
        if (isHolding && heldRigidbody != null) 
        {
            MoveHeldObject();
        }
    }

    void Update()
    {
        // Only update highlights if we aren't currently carrying something
        if (!isHolding)
        {
            UpdateHighlight();
        }
    }

    void MoveHeldObject()
    {
        // Smoothly interpolate the position toward the holdPoint
        Vector3 targetPos = holdPoint.position;
        Vector3 newPos = Vector3.Lerp(heldRigidbody.position, targetPos, holdSmoothing * Time.fixedDeltaTime);
        heldRigidbody.MovePosition(newPos);
    }

    // Call this from an Input Action (e.g., OnGrab)
    public void OnGrabPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isHolding) ThrowObject();
            else TryGrab();
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * grabRange, Color.red, 0.5f);

        if (Physics.Raycast(ray, out hit, grabRange))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (interactable != null && rb != null)
            {
                heldRigidbody = rb;
                heldRigidbody.useGravity = false;
                // Optional: stop it from spinning wildly while held
                heldRigidbody.angularVelocity = Vector3.zero; 
                
                if(currentHighlight != null) currentHighlight.Unhighlight();
                currentHighlight = null;

                isHolding = true;
                Debug.Log($"Grabbed {heldRigidbody.name}");
            }
        }
    }

    void ThrowObject()
    {
        if (heldRigidbody == null) return;

        heldRigidbody.useGravity = true;
        // Apply force in the direction the camera is looking
        heldRigidbody.AddForce(cameraTransform.forward * throwForce, ForceMode.Impulse);

        heldRigidbody = null;
        isHolding = false;
    }

    void UpdateHighlight()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabRange))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            // If we hit a new interactable
            if (interactable != null)
            {
                if (currentHighlight != interactable)
                {
                    if (currentHighlight != null) currentHighlight.Unhighlight();
                    currentHighlight = interactable;
                    currentHighlight.Highlight();
                }
            }
            else if (currentHighlight != null)
            {
                currentHighlight.Unhighlight();
                currentHighlight = null;
            }
        }
        else if (currentHighlight != null)
        {
            currentHighlight.Unhighlight();
            currentHighlight = null;
        }
    }
}
