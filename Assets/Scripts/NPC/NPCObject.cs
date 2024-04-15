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
    public GameObject original;
    public GameObject animated;

    private void Start()
    {
        if (animated != null)
        {
            animated.SetActive(false);
        }
    }

    public bool GuessCorrect()
    {
        return sentence == currentGuess;
    }

    public void PlayAnimated()
    {
        if (original != null)
        {
            original.SetActive(false);
        }
        if (animated != null)
        {
            animated.SetActive(true);
        }
    }
}
