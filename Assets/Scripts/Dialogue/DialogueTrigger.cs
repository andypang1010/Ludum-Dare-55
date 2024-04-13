using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject visualCue;
    public TextAsset inkJSON;
    bool isInRange;
    // Start is called before the first frame update
    void Start()
    {
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !DialogueManager.Instance.dialogueIsPlaying) {
            visualCue.SetActive(true);
            
            if (InputController.GetInteract()) {
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
