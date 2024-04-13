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
        npcView.sprite = npcObject.bookView;
        
        for(int i = 0; i < sentenceParent.transform.childCount; i++)
        {
            Destroy(sentenceParent.transform.GetChild(i).gameObject);
        }

        foreach (string sentence in NPCManager.Instance.GetSentences())
        {
            GameObject sentenceObj = Instantiate(sentencePrefab);
            TMP_Text textMeshPro = sentenceObj.GetComponent<TMP_Text>();
            textMeshPro.text = sentence;
            sentenceObj.transform.SetParent(sentenceParent.transform, true);
        }
    }
}
