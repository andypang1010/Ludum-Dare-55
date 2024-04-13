using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set;}

    public float typeSpeed = 0.04f;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public bool dialogueIsPlaying { get; private set; }

    Story currentStory;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one DialogueManager in scene");
        }

        Instance = this;
    }

    void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    void Update() {
        if (!dialogueIsPlaying) {
            return;
        }

        if (InputController.GetContinueDialogue()) {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string sentence = currentStory.Continue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        else
        {
            ExitDialogueMode();
        }
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
