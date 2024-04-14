using System.Collections;
using System.Collections.Generic;
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
        if (isInRange 
            && Physics.SphereCast(new Ray(playerCam.position, playerCam.forward), 1f, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable"))
            && hit.transform.gameObject == transform.parent.gameObject.GetComponentInChildren<Collider>().gameObject) {

            visualCue.SetActive(true);
            
            if (InputController.GetInteract()) {
                if (!BookUIManager.Instance.showingBook) {
                    NPCObject npcObject = GetComponentInParent<NPCObject>();

                    BookUIManager.Instance.ShowBook();
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
