using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode altCrouchKey = KeyCode.LeftCommand;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode inspectKey = KeyCode.Q;

    public Vector2 GetWalkDirection() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public Vector2 GetLookDirection() {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    public bool GetSprint() {
        return Input.GetKey(sprintKey);
    }

    public bool GetCrouchDown() {
        return Input.GetKeyDown(crouchKey) ^ Input.GetKeyDown(altCrouchKey);
    }

    public bool GetCrouchHold() {
        return Input.GetKey(crouchKey) ^ Input.GetKey(altCrouchKey);
    }

    public bool GetCrouchUp() {
        return Input.GetKeyUp(crouchKey) ^ Input.GetKeyUp(altCrouchKey);
    }

    public bool GetJumpDown() {
        return Input.GetKeyDown(jumpKey);
    }

    public bool GetInteractDown() {
        return Input.GetKeyDown(interactKey);
    }

    public bool GetInteractUp() {
        return Input.GetKeyUp(interactKey);
    }

    public bool GetInteractHold() {
        return Input.GetKey(interactKey);
    }

    public bool GetInspect() {
        return Input.GetKeyDown(inspectKey);
    }
}
