using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject visualCue;
    public TextAsset inkJSON;
    public Transform playerCam;
    bool isInRange;
    RaycastHit hit;

    void Start()
    {
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.SphereCast(new Ray(playerCam.position, playerCam.forward), 0.01f, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        //if (hit.collider != null)
        //{
        //    Debug.Log(hit.collider.gameObject.name);
        //}

        if (isInRange 
            && Physics.SphereCast(new Ray(playerCam.position, playerCam.forward), 0.01f, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable"))
            && transform.parent.gameObject.GetComponentsInChildren<Collider>().Contains(hit.transform.gameObject.GetComponent<Collider>())) {

            //hit.transform.gameObject == transform.parent.gameObject.GetComponentInChildren<Collider>().gameObject

            visualCue.SetActive(true);
            
            if (InputController.GetInteract()) {
                if (!BookUIManager.Instance.showingBook) {
                    NPCObject npcObject = GetComponentInParent<NPCObject>();

                    BookUIManager.Instance.ShowSentenceGuesser(npcObject);
                }

                else if (BookUIManager.Instance.showingBook) {
                    BookUIManager.Instance.HideBook();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isInRange = false;
        }
    }
}
