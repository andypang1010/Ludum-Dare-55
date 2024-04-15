using System.Collections;
using System.Collections.Generic;
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

    public void GuessSentence(string sentence)
    {
        NPCObject openedNpc = BookUIManager.Instance.currNpc;

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
        if(correctGuessedNPCs.Count >= minCorrectCount) {
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
                newUnlocked.Add(npc);
            }
        }
        BookUIManager.Instance.UnlockNewNPCs(new List<NPCObject>(correctGuessedNPCs), newUnlocked);
    }
}
