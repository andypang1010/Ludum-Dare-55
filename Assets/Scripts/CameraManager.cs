using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Camera camera;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one NPCManager in scene");
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // camera.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
