using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour
{
    public string sentence;
    public string currentGuess;
    public Sprite bookView;
    public NPCObject nextUnlockedNPC;
    public bool isConfirmed;

    public bool GuessCorrect() {
        return sentence == currentGuess;
    }
}
