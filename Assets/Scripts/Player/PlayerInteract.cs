using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Camera")]
    public Transform cam;

    [Header("Settings")]
    public float maxInteractDistance;
    public float minInteractTime;
    float holdTime;
    GameObject targetObject;
    RaycastHit hit;

    void Update()
    {
        if (InputController.GetInteractDown()
            && Physics.Raycast(cam.transform.position, cam.forward, out hit, maxInteractDistance) 
            && hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
                holdTime = 0;

                targetObject = hit.collider.gameObject;
            }

        if (InputController.GetInteractUp()) {
            holdTime = 0;
        }
   
        if (InputController.GetInteractHold()) {
            if (Physics.Raycast(cam.transform.position, cam.forward, out hit, maxInteractDistance)
            && hit.collider.gameObject == targetObject) {
                holdTime += Time.deltaTime;

                if (holdTime >= minInteractTime) {
                    print("Interact successful");
                }
            }
        }

    }
}
