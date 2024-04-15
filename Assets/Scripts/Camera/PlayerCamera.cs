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

    public float lookDuration;
    public HeadBob headBob;

    private bool playingAnimation;

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

        else if (!playingAnimation)
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
            StartCoroutine(LookAtPos(obj.transform.position));
            yield return new WaitForSeconds(3);
        }
        playingAnimation = false;
        headBob.enabled = true;
    }

    private IEnumerator LookAtPos(Vector3 pos)
    {
        float timeElapsed = 0;
        Quaternion initRot = cam.rotation;
        Vector3 deltaPostition = pos - cam.position;
        Quaternion targetRotation = Quaternion.LookRotation(deltaPostition, Vector3.up);

        while (timeElapsed < lookDuration)
        {
            cam.rotation = Quaternion.Slerp(initRot, targetRotation, Mathf.SmoothStep(0, 1, timeElapsed / lookDuration));
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        cam.rotation = targetRotation;
    }
}
