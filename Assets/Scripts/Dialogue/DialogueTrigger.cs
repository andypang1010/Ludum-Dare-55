using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
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
            && Physics.Raycast(new Ray(playerCam.position, playerCam.forward), out hit, 2f, LayerMask.GetMask("Interactable"))
            && hit.collider.gameObject == transform.parent.gameObject.GetComponentInChildren<Collider>().gameObject) {

            visualCue.SetActive(true);
            
            if (InputController.GetInteract() && !DialogueManager.Instance.dialogueIsPlaying ) {
                DialogueManager.Instance.EnterDialogueMode(inkJSON);
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
