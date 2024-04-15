using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCTrigger : MonoBehaviour
{
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.TryGetComponent(out NPCObject parentNPC))
        {
            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out hit,
                    Crosshair.Instance.maxDetectionRange,
                    LayerMask.GetMask("Interactable"))
                && hit.transform.gameObject.GetComponent<Collider>() == GetComponent<Collider>())
            {
                print(gameObject.name + " NO PARENT, RAYCAST FOUND");
                CheckBook(GetComponent<NPCObject>());
            }

            else {
                print(gameObject.name + " NO PARENT, RAYCAST NOT FOUND");
            }
        }

        else
        {
            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out hit,
                    Crosshair.Instance.maxDetectionRange,
                    LayerMask.GetMask("Interactable"))
                && transform.parent.gameObject.GetComponentsInChildren<Collider>().Contains(hit.transform.gameObject.GetComponent<Collider>()))
            {
                print(transform.parent.name + " HAS PARENT, RAYCAST FOUND");
                CheckBook(GetComponentInParent<NPCObject>());
            }

            else {
                print(transform.parent.name + " HAS PARENT, RAYCAST NOT FOUND");
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
