using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    private NPCObject npcObject;
    private string sentence;
    private Button buttonComponent;
    private TMP_Text tmpTextComponent;

    public void Setup(NPCObject npcObj, string sentence, bool enabled = true)
    {
        buttonComponent = GetComponent<Button>();
        tmpTextComponent = GetComponentInChildren<TMP_Text>();

        // if the current opened npc is confirmed, disable the button
        buttonComponent.enabled = enabled;

        this.sentence = sentence;
        npcObject = npcObj;

        // if the npc that the sentence belongs to is confirmed, strikethrough the text and disable the button
        UpdateSentence();
    }

    public void Guess()
    {
        NPCManager.Instance.GuessSentence(sentence);
    }

    public void UpdateSentence()
    {
        // If sentence is already confirmed, strike through the sentence in bookView
        if(npcObject.isConfirmed)
        {
            Debug.Log("sentence update is confirmed");
            tmpTextComponent.text = "<s>\"" + npcObject.sentence + "\"</s>";
            buttonComponent.enabled = false;
        }

        else {
            Debug.Log("sentence update is not confirmed");
            tmpTextComponent.text = "\"" + npcObject.sentence + "\"";
        }
    }
}
