using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUIManager : MonoBehaviour
{
    public static BookUIManager Instance;

    public GameObject sentencePrefab;
    public GameObject book;
    public Image npcView;
    public GameObject sentenceParent;

    public bool showingBook;
    private NPCObject currNpc;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one BookUIManager in scene");
        }

        Instance = this;
    }

    void Start() {
        HideBook();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBook()
    {
        showingBook = true;
        book.SetActive(true);
    }

    public void HideBook()
    {
        showingBook = false;
        book.SetActive(false);
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
