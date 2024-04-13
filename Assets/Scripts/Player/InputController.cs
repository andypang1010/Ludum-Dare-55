using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    static KeyCode sprintKey = KeyCode.LeftShift;
    static KeyCode crouchKey = KeyCode.LeftControl;
    static KeyCode altCrouchKey = KeyCode.LeftCommand;
    static KeyCode jumpKey = KeyCode.Space;
    static KeyCode interactKey = KeyCode.E;
    static KeyCode inspectKey = KeyCode.Q;

    public static Vector2 GetWalkDirection() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public static Vector2 GetLookDirection() {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    public static bool GetSprint() {
        return Input.GetKey(sprintKey);
    }

    public static bool GetCrouchDown() {
        return Input.GetKeyDown(crouchKey) ^ Input.GetKeyDown(altCrouchKey);
    }

    public static bool GetCrouchHold() {
        return Input.GetKey(crouchKey) ^ Input.GetKey(altCrouchKey);
    }

    public static bool GetCrouchUp() {
        return Input.GetKeyUp(crouchKey) ^ Input.GetKeyUp(altCrouchKey);
    }

    public static bool GetJumpDown() {
        return Input.GetKeyDown(jumpKey);
    }

    public static bool GetInteractDown() {
        return Input.GetKeyDown(interactKey);
    }

    public static bool GetInteractUp() {
        return Input.GetKeyUp(interactKey);
    }

    public static bool GetInteractHold() {
        return Input.GetKey(interactKey);
    }

    public static bool GetInspect() {
        return Input.GetKeyDown(inspectKey);
    }
}
