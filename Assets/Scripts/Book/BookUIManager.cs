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
    public GameObject sentenceParent;
    public TMP_Text currentGuessText;
    public Image npcView;

    public bool showingBook;
    public NPCObject currNpc;
    private List<Sentence> currSentences = new List<Sentence>();

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
        Debug.Log("show book");
        showingBook = true;
        book.SetActive(true);
    }

    public void HideBook()
    {
        Debug.Log("hide book");
        showingBook = false;
        book.SetActive(false);
    }

    public void ShowSentenceGuesser(NPCObject npc)
    {
        ShowBook();

        npcView.sprite = npc.bookView;
        UpdateGuessText(npc.currentGuess);
        currNpc = npc;
        
        for(int i = 0; i < sentenceParent.transform.childCount; i++)
        {
            Destroy(sentenceParent.transform.GetChild(i).gameObject);
        }
        currSentences.Clear();

        foreach (NPCObject npcObj in NPCManager.Instance.unlockedNPCs)
        {
            // foreach (string sentence in npcObj.sentences) {
            CreateNewSentence(npcObj);
            // }
        }
    }

    private void CreateNewSentence(NPCObject npc)
    {
        GameObject sentenceObj = Instantiate(sentencePrefab);
        Sentence sentenceComponent = sentenceObj.GetComponent<Sentence>();
        currSentences.Add(sentenceComponent);

        sentenceComponent.Setup(npc, npc.sentence, !npc.isConfirmed);

        sentenceObj.transform.SetParent(sentenceParent.transform, false);
    }

    public void UpdateGuessText(string guess)
    {
        currentGuessText.text = guess.Length > 0 ? "\"" + guess + "\"" : "";
    }

    public void UnlockNewNPCs(List<NPCObject> newUnlocked)
    {
        StartCoroutine(UnlockAnimation(newUnlocked));
    }

    private IEnumerator UnlockAnimation(List<NPCObject> newUnlocked)
    {
        foreach(Sentence sentence in currSentences)
        {
            sentence.UpdateSentence();
            yield return new WaitForSeconds(0.5f);
        }
        foreach(NPCObject npc in  newUnlocked)
        {
            CreateNewSentence(npc);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
