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
    public GameObject menuPanel, rulesPanel, winPanel;
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
        bgm.SetActive(currentGameState == GameStates.GAME
                    || currentGameState == GameStates.WIN);

        switch (currentGameState) {
            case GameStates.MENU:
                InputController.Instance.enabled = false;
                menuPanel.SetActive(true);
                rulesPanel.SetActive(false);
                winPanel.SetActive(false);
                break;
            case GameStates.RULES:
                InputController.Instance.enabled = false;
                menuPanel.SetActive(false);
                rulesPanel.SetActive(true);
                winPanel.SetActive(false);
                break;
            case GameStates.GAME:
                InputController.Instance.enabled = true;
                menuPanel.SetActive(false);
                rulesPanel.SetActive(false);
                winPanel.SetActive(false);
                break;
            case GameStates.WIN:
                InputController.Instance.enabled = true;
                menuPanel.SetActive(false);
                rulesPanel.SetActive(false);
                winPanel.SetActive(true);
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

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public enum GameStates {
        MENU,
        RULES,
        GAME,
        WIN
    }
}
