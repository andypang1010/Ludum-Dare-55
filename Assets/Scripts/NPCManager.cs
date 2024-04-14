using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    public int minCorrectCount = 3;
    public List<NPCObject> unlockedNPCs = new List<NPCObject>();
    private List<NPCObject> guessedNPCs = new List<NPCObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuessSentence(NPCObject npc, string sentence)
    {
        // npc.guesses.Add(sentence);
        if(npc.sentences.Contains(sentence))
        {
            guessedNPCs.Add(npc);
            npc.guessed = true;
            if(guessedNPCs.Count >= minCorrectCount)
            {
                UnlockNewNPCs();
                // BookUIManager.Instance.HideBook();
            }
        }
    }

    public void UnlockNewNPCs()
    {
        foreach(NPCObject npc in guessedNPCs)
        {
            unlockedNPCs.Add(npc.nextUnlockedNPC);
        }
    }

    public List<string> GetSentences()
    {
        List<string> sentencesList = new List<string>();
        foreach(NPCObject npc in unlockedNPCs)
        {
            foreach(string sentence in npc.sentences) {
                sentencesList.Add(sentence);
            }
        }
        return sentencesList;
    }
}
