using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour
{
    public List<string> sentences;
    // public List<string> guesses;
    public Sprite bookView;
    public bool isRelevant;
    public bool guessed;
    public NPCObject nextUnlockedNPC;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // public void OnCollisionStay(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Player") && InputController.GetInteract())
    //     {
    //         Debug.Log("2");
    //         BookUIManager.Instance.ShowSentenceGuesser(this);
    //     }
    // }
}
