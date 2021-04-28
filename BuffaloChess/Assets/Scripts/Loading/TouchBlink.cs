using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchBlink : MonoBehaviour
{
    public Text TouchText;
    float time;

    // Update is called once per frame
    void Update()
    {
        if (time < 0.5)
        {
           TouchText.color = new Color(0.5f, 0.8f, 1, 1 - time);
        }
        else
        {
            TouchText.color = new Color(0.5f, 0.8f, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }

}