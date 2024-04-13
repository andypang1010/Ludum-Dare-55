using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set;}

    public GameObject dialoguePanel;
    public TextMeshPro nameText;
    public TextMeshPro dialogueText;
    public Image continueIcon;

    Story currentStory;
    bool dialogueIsPlaying;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one DialogueManager in scene");
        }

        Instance = this;
    }
}
