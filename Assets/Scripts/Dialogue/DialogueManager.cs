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

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.04f;
    public bool dialogueIsPlaying { get; private set; }

    [Header("Choices")]
    public GameObject[] choices;
    TextMeshProUGUI[] choiceTexts;

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

        choiceTexts = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices) {
            choiceTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update() {
        if (!dialogueIsPlaying) {
            return;
        }

        if (InputController.GetContinueDialogue() && currentStory.currentChoices.Count == 0) {
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

            ShowChoices();
        }

        else
        {
            ExitDialogueMode();
        }
    }

    private void ShowChoices() {
        List<Choice> choicesList = currentStory.currentChoices;

        int index = 0;
        foreach (Choice choice in choicesList) {
            choices[index].gameObject.SetActive(true);
            choiceTexts[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++) {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
