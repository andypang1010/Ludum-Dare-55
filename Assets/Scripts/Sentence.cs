using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sentence : MonoBehaviour
{
    private NPCObject npcObject;
    private string sentence;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(NPCObject npcObj, string sentence)
    {
        TMP_Text textMeshPro = GetComponentInChildren<TMP_Text>();
        textMeshPro.text = sentence;
        this.sentence = sentence;
        this.npcObject = npcObj;
    }

    public void Guess()
    {
        NPCManager.Instance.GuessSentence(npcObject, sentence);
    }
}