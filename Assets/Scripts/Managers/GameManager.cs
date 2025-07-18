using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value; // Set the target frame rate to the screen's refresh rate
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
