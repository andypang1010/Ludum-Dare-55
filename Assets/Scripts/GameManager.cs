using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public int frameRate = 60;
    public GameStates currentGameState;
    public GameObject canvas;
    public GameObject menuPanel, rulesPanel;
    public GameObject bgm;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one GameManager in scene");
        }

        Instance = this;
    }

    void Start() {
        currentGameState = GameStates.MENU;
        canvas.SetActive(true);
    }
    
    void Update()
    {
        Application.targetFrameRate = frameRate;
        bgm.SetActive(currentGameState == GameStates.GAME);

        switch (currentGameState) {
            case GameStates.MENU:
                InputController.Instance.enabled = false;
                menuPanel.SetActive(true);
                rulesPanel.SetActive(false);
                break;
            case GameStates.RULES:
                InputController.Instance.enabled = false;
                menuPanel.SetActive(false);
                rulesPanel.SetActive(true);
                break;
            case GameStates.GAME:
                InputController.Instance.enabled = true;
                menuPanel.SetActive(false);
                rulesPanel.SetActive(false);
                break;
        }
    }

    public void StartGame() {
        currentGameState = GameStates.GAME;
    }

    public void StartMenu() {
        currentGameState = GameStates.MENU;
    }

    public void StartRules() {
        currentGameState = GameStates.RULES;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public enum GameStates {
        MENU,
        RULES,
        GAME
    }
}
