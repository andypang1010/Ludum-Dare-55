using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

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
        if(npc.sentence == sentence)
        {
            guessedNPCs.Add(npc);
            npc.guessed = true;
            if(guessedNPCs.Count >= 3)
            {
                UnlockNewNPCs();
                BookUIManager.Instance.HideBook();
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
        List<string> sentences = new List<string>();
        foreach(NPCObject npc in unlockedNPCs)
        {
            sentences.Add(npc.sentence);
        }
        return sentences;
    }
}
