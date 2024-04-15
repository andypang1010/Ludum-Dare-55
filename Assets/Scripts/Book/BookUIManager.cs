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
    public AudioSource audioSource;
    public AudioClip openBook, closeBook, confirmedNPC;

    public bool showingBook;
    public NPCObject currNpc;
    private List<Sentence> currSentences = new List<Sentence>();
    private bool playCamAnimation;
    private List<NPCObject> justGuessed;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one BookUIManager in scene");
        }

        Instance = this;
    }

    void Start() {
        showingBook = false;
        book.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBook()
    {
        audioSource.PlayOneShot(openBook, 0.5f);
        showingBook = true;
        book.SetActive(true);
    }

    public void HideBook()
    {
        audioSource.PlayOneShot(closeBook, 0.5f);
        showingBook = false;
        book.SetActive(false);
        if(playCamAnimation)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach(NPCObject npc in justGuessed)
            {
                gameObjects.Add(npc.gameObject);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCamera>().PlayFollowAnimation(gameObjects);
            playCamAnimation = false;
        }
    }

    public void ShowSentenceGuesser(NPCObject npc)
    {
        currNpc = npc;

        ShowBook();
        npcView.sprite = npc.bookView;
        UpdateGuessText(npc.currentGuess);
        
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

        sentenceComponent.Setup(npc, npc.sentence, !currNpc.isConfirmed);

        sentenceObj.transform.SetParent(sentenceParent.transform, false);
    }

    public void UpdateGuessText(string guess)
    {
        currentGuessText.text = guess.Length > 0 ? "\"" + guess + "\"" : "";
        currentGuessText.color = currNpc.isConfirmed ? Color.green : Color.white;
    }

    public void UnlockNewNPCs(List<NPCObject> justGuessed, List<NPCObject> newUnlocked)
    {
        this.justGuessed = justGuessed;
        foreach (Sentence sentence in currSentences)
        {
            sentence.SetEnabled(false);
        }
        StartCoroutine(UnlockAnimation(newUnlocked));
    }

    private IEnumerator UnlockAnimation(List<NPCObject> newUnlocked)
    {
        playCamAnimation = true;

        foreach(Sentence sentence in currSentences)
        {
            if(!sentence.npcObject.isConfirmed)
            {
                continue;
            }
            audioSource.PlayOneShot(confirmedNPC, 0.3f);
            sentence.UpdateSentence();
            yield return new WaitForSeconds(0.5f);
        }

        // TODO: test unlock new
        foreach(NPCObject npc in newUnlocked)
        {

            // TODO: play confirmed sound (ARE YOU SURE???)

            // audioSource.PlayOneShot(confirmedNPC);
            CreateNewSentence(npc);
            yield return new WaitForSeconds(0.5f);
        }
        foreach (Sentence sentence in currSentences)
        {
            if (!sentence.npcObject.isConfirmed)
            {
                sentence.SetEnabled(true);
            }
        }
        UpdateGuessText(currNpc.sentence);
    }
}
