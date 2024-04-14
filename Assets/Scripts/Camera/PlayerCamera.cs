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

    void Start()
    {
        // Centers and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (BookUIManager.Instance.showingBook) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Update camera rotation
            cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);
            
            // Update player rotation
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }

        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 lookDirection = InputController.GetLookDirection();

            // Get mouse input with sensitivity
            float mouseX = lookDirection.x * Time.fixedDeltaTime * sensX;
            float mouseY = lookDirection.y * Time.fixedDeltaTime * sensY;

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
}
