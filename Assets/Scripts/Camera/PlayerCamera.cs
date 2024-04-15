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

    public float turningRate = 200f;
    public HeadBob headBob;

    private bool playingAnimation;
    private Quaternion targetRotation;

    void Update()
    {
        if (GameManager.Instance.currentGameState != GameManager.GameStates.GAME) {
            // Update camera rotation
            cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);

            // Update player rotation
            transform.rotation = Quaternion.Euler(0, rotationY, 0);

            return;
        }

        if (BookUIManager.Instance.showingBook)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Update camera rotation
            cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);

            // Update player rotation
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }

        else if (playingAnimation)
        {
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, targetRotation, turningRate * Time.deltaTime);
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 lookDirection = InputController.Instance.GetLookDirection();

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

    public void PlayFollowAnimation(List<GameObject> gameObjects)
    {
        StartCoroutine(FollowAnimation(gameObjects));
    }

    private IEnumerator FollowAnimation(List<GameObject> gameObjects)
    {
        playingAnimation = true;
        headBob.enabled = false;
        foreach (GameObject obj in gameObjects)
        {
            Vector3 deltaPostition = obj.transform.position - cam.position;
            targetRotation = Quaternion.LookRotation(deltaPostition, Vector3.up);
            yield return new WaitForSeconds(3);
        }
        playingAnimation = false;
        headBob.enabled = true;
    }
}
