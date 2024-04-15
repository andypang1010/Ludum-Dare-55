using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance;
    public float maxDetectionRange = 10f;
    public Sprite defaultCrosshair, interacteCrosshair;
    
    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one Crosshair in scene");
        }

        Instance = this;
    }

    void Update()
    {
        GetComponent<Image>().enabled = GameManager.Instance.currentGameState == GameManager.GameStates.GAME;
        if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out _,
                    maxDetectionRange,
                    LayerMask.GetMask("Interactable")))
            {
                GetComponent<Image>().sprite = interacteCrosshair;
            }

            else {
                GetComponent<Image>().sprite = defaultCrosshair;
            }
    }
}
