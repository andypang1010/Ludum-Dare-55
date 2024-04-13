using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputController
{
    static KeyCode interactKey = KeyCode.E;
    static KeyCode dialogueKey = KeyCode.Space;
    static KeyCode inspectKey = KeyCode.Q;

    public static Vector2 GetWalkDirection() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public static Vector2 GetLookDirection() {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    public static bool GetInteract() {
        return Input.GetKeyDown(interactKey);
    }

    public static bool GetContinueDialogue() {
        return Input.GetKeyDown(dialogueKey);
    }

    public static bool GetInspect() {
        return Input.GetKeyDown(inspectKey);
    }
}
