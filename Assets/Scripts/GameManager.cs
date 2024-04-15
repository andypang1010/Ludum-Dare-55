using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public int frameRate = 60;
    public GameStates currentGameState;
    public GameObject menuPanel;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one GameManager in scene");
        }

        Instance = this;
    }

    void Start() {
        currentGameState = GameStates.MENU;
    }
    
    void Update()
    {
        Application.targetFrameRate = frameRate;

        switch (currentGameState) {
            case GameStates.MENU:
                InputController.Instance.enabled = false;
                menuPanel.SetActive(true);
                break;
            case GameStates.RULES:
                break;
            case GameStates.GAME:
                InputController.Instance.enabled = true;
                menuPanel.SetActive(false);
                break;
        }
    }

    public void StartGame() {
        currentGameState = GameStates.GAME;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ShowRules() {
        currentGameState = GameStates.RULES;
    }

    public enum GameStates {
        MENU,
        RULES,
        GAME
    }
}
