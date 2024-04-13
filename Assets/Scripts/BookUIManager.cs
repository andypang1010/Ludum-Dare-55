using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUIManager : MonoBehaviour
{
    public static BookUIManager Instance;

    public GameObject sentencePrefab;

    public GameObject canvas;
    public Image npcView;
    public GameObject sentenceParent;

    private bool showingBook;
    private NPCObject currNpc;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputController.GetInteract() && showingBook)
        {
            //HideBook();
        }
    }

    private void ShowBook()
    {
        showingBook = true;
        canvas.SetActive(true);
    }

    private void HideBook()
    {
        showingBook = false;
        canvas.SetActive(false);
    }

    public void ShowSentenceGuesser(NPCObject npcObject)
    {
        ShowBook();
        currNpc = npcObject;
        npcView.sprite = npcObject.bookView;
        
        for(int i = 0; i < sentenceParent.transform.childCount; i++)
        {
            Destroy(sentenceParent.transform.GetChild(i).gameObject);
        }

        foreach (NPCObject npcObj in NPCManager.Instance.unlockedNPCs)
        {
            GameObject sentenceObj = Instantiate(sentencePrefab);
            Sentence sentenceComponent = sentenceObj.GetComponent<Sentence>();
            sentenceComponent.Setup(npcObj, npcObj.sentence);
            sentenceObj.transform.SetParent(sentenceParent.transform, false);
        }
    }
}
