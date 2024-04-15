using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    float maxDetectionRange = 10f;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        //Physics.SphereCast(new Ray(playerCam.position, playerCam.forward), 0.001f, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        //Physics.Raycast(new Ray(playerCam.position, playerCam.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        //Physics.Raycast(
        //            Camera.main.transform.position,
        //            Camera.main.transform.forward,
        //            out hit,
        //            maxDetectionRange,
        //            LayerMask.GetMask("Interactable"));
        //if (hit.collider != null)
        //{
        //    Debug.Log(hit.collider.gameObject.transform.parent.name);
        //}
        if (!transform.parent.TryGetComponent(out NPCObject parentNPC))
        {
            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out hit,
                    maxDetectionRange,
                    LayerMask.GetMask("Interactable"))
                && hit.transform.gameObject.GetComponent<Collider>() == GetComponent<Collider>())
            {
                if (!BookUIManager.Instance.showingBook && InputController.GetInteract())
                {
                    CheckBook(GetComponent<NPCObject>());
                }
            }
        }
        else
        {
            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out hit,
                    maxDetectionRange,
                    LayerMask.GetMask("Interactable"))
                && transform.parent.gameObject.GetComponentsInChildren<Collider>().Contains(hit.transform.gameObject.GetComponent<Collider>()))
            {
                CheckBook(GetComponentInParent<NPCObject>());
            }
        }
    }

    private void CheckBook(NPCObject npc)
    {
        if (InputController.GetInteract())
        {
            if (!BookUIManager.Instance.showingBook)
            {
                Debug.Log(name);
                Debug.Log(transform.parent.name);
                BookUIManager.Instance.ShowSentenceGuesser(npc);
            }
            else
            {
                Debug.Log(name);
                Debug.Log(transform.parent.name);
                BookUIManager.Instance.HideBook();
            }
        }
    }
}
