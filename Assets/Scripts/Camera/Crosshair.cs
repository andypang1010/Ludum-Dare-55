using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance;
    public float maxDetectionRange = 4f;
    public Sprite defaultCrosshair, interacteCrosshair;
    public PlayerCamera playerCamera;
    
    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one Crosshair in scene");
        }

        Instance = this;
    }

    void Update()
    {
        GetComponent<Image>().enabled = (GameManager.Instance.currentGameState == GameManager.GameStates.GAME) && !playerCamera.playingAnimation;
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
