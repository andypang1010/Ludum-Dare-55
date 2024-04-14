using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject visualCue;
    public TextAsset inkJSON;
    public Transform playerCam;
    public float maxInteractDistance;
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
            && Physics.Raycast(new Ray(playerCam.position, playerCam.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Interactable"))
            && hit.transform.gameObject == transform.parent.gameObject.GetComponentInChildren<Collider>().gameObject) {

            visualCue.SetActive(true);
            
            if (InputController.GetInteract() && !DialogueManager.Instance.dialogueIsPlaying ) {
                NPCObject npcObject = GetComponentInParent<NPCObject>();

                if (npcObject.guessed)
                {
                    print("Guessed");
                    BookUIManager.Instance.HideBook();
                    DialogueManager.Instance.EnterDialogueMode(inkJSON);
                }

                else
                {
                    BookUIManager.Instance.ShowBook();
                    BookUIManager.Instance.ShowSentenceGuesser(npcObject);
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