using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int frameRate = 60;
    
    void Update()
    {
        Application.targetFrameRate = frameRate;
    }
}
