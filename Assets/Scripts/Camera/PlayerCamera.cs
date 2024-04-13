using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera")]
    public Transform cam;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;
    [HideInInspector] public float rotationX, rotationY;
    float currentSensX, currentSensY;
    Vector2 lookDirection;

    void Start()
    {
        // Centers and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Time.timeScale != 1) {
            currentSensX = sensX / Time.timeScale;
            currentSensY = sensY / Time.timeScale;
        }

        else {
            currentSensX = sensX;
            currentSensY = sensY;
        }

        if (!DialogueManager.Instance.dialogueIsPlaying) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            lookDirection = InputController.GetLookDirection();
        }

        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Get mouse input with sensitivity
        float mouseX = lookDirection.x * Time.fixedDeltaTime * currentSensX;
        float mouseY = lookDirection.y * Time.fixedDeltaTime * currentSensY;
    
        // Weird but works (DON'T TOUCH)
        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp y-axis rotation
        rotationX = Math.Clamp(rotationX, -80f, 80f);

        // Update camera rotation
        cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        
        // Update player rotation
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
