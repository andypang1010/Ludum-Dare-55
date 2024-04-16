using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera")]
    public Transform cam;
    public float initialRotationX, initialRotationY;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;
    [HideInInspector] public float rotationX, rotationY;

    public float lookDuration;
    public HeadBob headBob;
    public AudioSource audioSource;
    public AudioClip puff;

    public bool playingAnimation;

    void Start() {
        rotationX = initialRotationX;
        rotationY = initialRotationY;
    }

    void Update()
    {
        if (!GameManager.Instance.InputIsAvailable()
        || BookUIManager.Instance.showingBook) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Update camera rotation
            cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);

            // Update player rotation
            transform.rotation = Quaternion.Euler(0, rotationY, 0);

            return;
        }

        else {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (!playingAnimation) {
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
    }

    public void PlayFollowAnimation(List<NPCObject> npcs)
    {
        StartCoroutine(FollowAnimation(npcs));
    }

    private IEnumerator FollowAnimation(List<NPCObject> npcs)
    {
        playingAnimation = true;
        headBob.enabled = false;
        foreach (NPCObject npc in npcs)
        {
            StartCoroutine(LookAtPos(npc.transform.position));
            yield return new WaitForSeconds(lookDuration);
            audioSource.PlayOneShot(puff);
            npc.PlayAnimated();
            yield return new WaitForSeconds(1f);
        }
        
        playingAnimation = false;
        headBob.enabled = true;

        if (NPCManager.Instance.allNPCs.All(x => x.GetComponent<NPCObject>().isConfirmed == true)) {
            GameManager.Instance.currentGameState = GameManager.GameStates.WIN;
        }
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
