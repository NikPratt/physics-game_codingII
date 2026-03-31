// using UnityEngine;

// public class NewMonoBehaviourScript : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     //called every fixed updae while holding an object 
//     //smoothly moves the rigidbody toward the hold point 
    
//     void MoveHeldObject()
//     {
//         Vector3 targetPos = cameraTransform.position + cameraTransform.forward * holdDistance;
//         Vector3 currentPos = HeldObject.position;
//         Vector3 newPos = Vector3.Lerp(currentPos, targetPos, holdSmoothing * Time.fixedDeltaTime);
//         HeldObject.MovePosition(newPos);
//     }
//     void TryGrab()
//     {
//         Ray ray = new Ray(transform.position, transform.forward);
//         RaycastHit hit;

//         Debug.DrawRay(transform.position, transform.forward * 5, Color.red, .5f);

//         if(Physics.Raycast(ray, out hit, grabRAnge))
//         {
//             InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
//             if(interactable != null);
//         }
//             HeldObject.useGravity = false;
//             HeldObject.freezeRotation = true;

//             Vector3 newPos = Vector3.Lerp(currentPos, targetPos, holdSmoothing * Time.fixedDeltaTime);

//             HeldObject.MovePosition(newPos);
//     }    

//     void ThrowObject()
//     {
//         if (HeldObject == null); 
//         MoveHeldObject; 
//     }
// }
