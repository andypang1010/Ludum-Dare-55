using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour
{
    public string sentence;
    public string currentGuess;
    // public List<string> sentences;
    // public List<string> guesses;
    public Sprite bookView;
    // public bool isRelevant;
    public NPCObject nextUnlockedNPC;
    public bool isConfirmed;

    public bool GuessCorrect() {
        return sentence == currentGuess;
    }
}
