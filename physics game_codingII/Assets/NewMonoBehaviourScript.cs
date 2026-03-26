using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabber : MonoBehaviour
{
    [header("Grab Settings")]

    [Tooltip("How fast the held object moves to the hold point. higher = snappier")]
    public float grabRange = 4;

    public float holdSmoothing = 15f;

    public Trabsfirn holdPoint;

    publc float throwForce = 15f;

    private Rigitdbody heldObject;
    private bool isHolding = false; 

    private InteractableObject
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        if (isHolding && heldObject != null) MoveHeldObject();

    }

    // Update is called once per frame
    void Update()
    {
        //run the detention raycast every frame to updaet update the highlight
        //this is diff from grab graycast is just checks what the player is looking at and highlights/unhighlight accordingly
        UpdateHighlight();
    }

    //called every fixed updae while holding an object 
    //smoothly moves the rigidbody toward the hold point 
    
    void MoveHeldObject()
    {
        Vector3 targetPos = cameraTransform.position + cameraTransform.forward * holdDistance;
        Vector3 currentPos = heldObject.position;
        Vector3 newPos = Vector3.Lerp(currentPos, targetPos, holdSmoothing * Time.fixedDeltaTime);
        heldObject.MovePosition(newPos);
    }
    void TryGrab()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * grabRange, Color.red, 0.5f);

        if(Physics.Raycast(ray, out hit, grabRange))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if(interactable != null)
            {
                heldObject.useGravity = false;
                heldObject.freezeRotation = true;
                heldObject.linearVelocity = Vector3.zero;
                heldObject.angularVelocity = Vector3.zero;

                interactable.Unhighlight();
                currentHighlight = null;

                isHolding = true;
                Debug.Log($"Grabbed {heldObject.name}");

                heldObject.MovePosition(newPos);
            }
    }    

    void ThrowObject()
    {
        if (heldObject == null) return;
        
        heldObejct.useGravity = true;
        heldObejct.useGravity = true;

    }

    public void OnThrowPlatformed(InputAction.CallbackContext context)
    {
        if(isHolding) ThrowObject();
    }
    
    void UpdateHighlight()
    {
        if (isHolding) return;
        Ray ray = new Ray(origin:transform.position, direction:transform,froward);
        RatcastHit hit;
        Debug.DrawRay(start transform.position, dir:transform.forward * grabRange, Color, red);

        if (Physics.Raycast(ray, out hit, grabRange))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null && currentHight != interactable)
            {
                currentHighLight.Unhighlight
            }
        }
    }
}
