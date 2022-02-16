using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fps_counter : MonoBehaviour
{

    public TextMeshProUGUI fpsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "" + (int)fps;
    }
}
