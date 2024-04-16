using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;
    public int minCorrectCount = 3;
    public List<NPCObject> unlockedNPCs;
    public HashSet<NPCObject> correctGuessedNPCs;
    public bool isPlayingAnimation;
    public GameObject[] allNPCs;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one NPCManager in scene");
        }

        Instance = this;
    }

    void Start() {
        correctGuessedNPCs = new HashSet<NPCObject>();
        allNPCs = GameObject.FindGameObjectsWithTag("NPC");
        
        print("All NPCs length: " + allNPCs.Length);
    }

    public void GuessSentence(string sentence)
    {
        NPCObject openedNpc = BookUIManager.Instance.currNpc;
        if (openedNpc.isConfirmed)
        {
            return;
        }

        // Select/Deselect
        if (sentence == openedNpc.currentGuess) {
            openedNpc.currentGuess = "";
        }
        else {
            openedNpc.currentGuess = sentence;
        }

        BookUIManager.Instance.UpdateGuessText(openedNpc.currentGuess);

        // Check
        if (openedNpc.GuessCorrect()) {
            correctGuessedNPCs.Add(openedNpc);
        }
        else {
            correctGuessedNPCs.Remove(openedNpc);
        }
        
        CheckNewUnlock();
    }

    private void CheckNewUnlock()
    {
        int notConfirmed = 0;
        foreach(NPCObject npc in unlockedNPCs)
        {
            if(!npc.isConfirmed)
            {
                notConfirmed++;
            }
        }

        if(correctGuessedNPCs.Count >= minCorrectCount || (unlockedNPCs.Count == 13 && correctGuessedNPCs.Count == 1 && notConfirmed == 1)) {
            foreach (NPCObject npc in correctGuessedNPCs) {
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
            if (npc.nextUnlockedNPC != null)
            {
                unlockedNPCs.Add(npc.nextUnlockedNPC);
                newUnlocked.Add(npc.nextUnlockedNPC);
            }
        }
        BookUIManager.Instance.UnlockNewNPCs(new List<NPCObject>(correctGuessedNPCs), newUnlocked);
    }
}
