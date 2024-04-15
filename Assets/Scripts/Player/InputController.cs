using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set;}
    public KeyCode interactKey = KeyCode.E;
    public KeyCode dialogueKey = KeyCode.Space;
    public KeyCode inspectKey = KeyCode.Q;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one InputController in scene");
        }
        Instance = this;
    }

    public Vector2 GetWalkDirection() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public Vector2 GetLookDirection() {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    public bool GetInteract() {
        return Input.GetKeyDown(interactKey);
    }

    public bool GetContinueDialogue() {
        return Input.GetKeyDown(dialogueKey);
    }

    public bool GetInspect() {
        return Input.GetKeyDown(inspectKey);
    }
}
