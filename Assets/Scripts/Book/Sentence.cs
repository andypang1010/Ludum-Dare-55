using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    public NPCObject npcObject { get; private set; }
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

    public void SetEnabled(bool enabled)
    {
        buttonComponent.enabled = enabled;
    }

    public void UpdateSentence()
    {
        // If sentence is already confirmed, strike through the sentence in bookView
        if(npcObject.isConfirmed)
        {
            tmpTextComponent.text = "<s>\"" + npcObject.sentence + "\"</s>";
            tmpTextComponent.color = Color.green;
            buttonComponent.enabled = false;
        }

        else {
            tmpTextComponent.text = "\"" + npcObject.sentence + "\"";
        }
    }
}
