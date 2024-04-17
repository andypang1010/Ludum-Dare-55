using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            switch (currentGameState) {
                case GameStates.GAME:
                    StartPause();
                    break;
                case GameStates.PAUSE:
                    StartGame();
                    break;
            }
        }
    }

    public void StartMenu() {
        if (currentGameState == GameStates.RULES) {
            currentGameState = GameStates.MENU;
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
        currentGameState = GameStates.GAME;
    }

    public bool InputIsAvailable() {
        return Instance.currentGameState == GameStates.GAME;
    }

    public enum GameStates {
        MENU,
        RULES,
        PAUSE,
        GAME,
        WIN
    }
}
