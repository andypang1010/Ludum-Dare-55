using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    public int minCorrectCount = 3;
    public List<NPCObject> unlockedNPCs;
    // public HashSet<NPCObject> confirmedNPCs;
    public HashSet<NPCObject> correctGuessedNPCs;
    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one NPCManager in scene");
        }

        Instance = this;
    }

    void Start() {
        // confirmedNPCs = new HashSet<NPCObject>();
        correctGuessedNPCs = new HashSet<NPCObject>();
    }

    public void GuessSentence(NPCObject npc, string sentence)
    {
        // Select/Deselect
        if (sentence == npc.currentGuess) {
            npc.currentGuess = "";
        }
        else {
            npc.currentGuess = sentence;
        }

        BookUIManager.Instance.UpdateGuessText(npc.currentGuess);

        // Check
        if (npc.GuessCorrect()) {
            correctGuessedNPCs.Add(npc);
        }
        else {
            correctGuessedNPCs.Remove(npc);
        }
        
        CheckNewUnlock();

    }

    private void CheckNewUnlock()
    {
        if(correctGuessedNPCs.Count >= minCorrectCount) {
            foreach (NPCObject npc in correctGuessedNPCs) {
                // confirmedNPCs.Add(npc);
                npc.isConfirmed = true;
            }

            UnlockNewNPCs();
            correctGuessedNPCs.Clear();
        }
    }

    public void UnlockNewNPCs()
    {
        List<NPCObject> newUnlocked = new List<NPCObject>();
        foreach(NPCObject npc in correctGuessedNPCs)
        {
            unlockedNPCs.Add(npc.nextUnlockedNPC);
            newUnlocked.Add(npc);
        }
        BookUIManager.Instance.UnlockNewNPCs(newUnlocked);
    }
}
