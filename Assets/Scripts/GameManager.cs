using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public int frameRate = 60;
    public GameStates currentGameState;
    public GameObject canvas;
    public GameObject menuPanel, rulesPanel, pausePanel, gamePanel, winPanel;
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
        bgm.SetActive(currentGameState == GameStates.PAUSE
                    || currentGameState == GameStates.GAME
                    || currentGameState == GameStates.WIN);

        menuPanel.SetActive(currentGameState == GameStates.MENU);
        rulesPanel.SetActive(currentGameState == GameStates.RULES);
        pausePanel.SetActive(currentGameState == GameStates.PAUSE);
        gamePanel.SetActive(currentGameState == GameStates.GAME);
        winPanel.SetActive(currentGameState == GameStates.WIN);

        // switch (currentGameState) {
        //     case GameStates.MENU:
        //         menuPanel.SetActive(true);
        //         rulesPanel.SetActive(false);
        //         pausePanel.SetActive(false);
        //         gamePanel.SetActive(false);
        //         winPanel.SetActive(false);
        //         break;
        //     case GameStates.RULES:
        //         menuPanel.SetActive(false);
        //         rulesPanel.SetActive(true);
        //         pausePanel.SetActive(false);
        //         gamePanel.SetActive(false);
        //         winPanel.SetActive(false);
        //         break;
        //     case GameStates.PAUSE:
        //         menuPanel.SetActive(false);
        //         rulesPanel.SetActive(false);
        //         pausePanel.SetActive(true);
        //         gamePanel.SetActive(false);
        //         winPanel.SetActive(false);
        //         break;
        //     case GameStates.GAME:
        //         menuPanel.SetActive(false);
        //         rulesPanel.SetActive(false);
        //         pausePanel.SetActive(false);
        //         gamePanel.SetActive(true);
        //         winPanel.SetActive(false);
        //         break;
        //     case GameStates.WIN:
        //         menuPanel.SetActive(false);
        //         rulesPanel.SetActive(false);
        //         pausePanel.SetActive(false);
        //         gamePanel.SetActive(false);
        //         winPanel.SetActive(true);
        //         break;
        // }
    }

    public void StartMenu() {
        currentGameState = GameStates.MENU;
    }

    public void StartRules() {
        currentGameState = GameStates.RULES;
    }

    public void StartPause() {
        currentGameState = GameStates.PAUSE;
    }

    public void StartGame() {
        currentGameState = GameStates.GAME;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool InputIsAvailable() {
        return Instance.currentGameState == GameStates.GAME
            || Instance.currentGameState == GameStates.WIN;
    }

    public enum GameStates {
        MENU,
        RULES,
        PAUSE,
        GAME,
        WIN
    }
}
