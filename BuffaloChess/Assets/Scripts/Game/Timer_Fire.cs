using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Fire : MonoBehaviour
{
    GameObject GM;
    RectTransform pos;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        //출발위치 (265,-200), 도착위치 (-270 , -200)
        if (this.name == "TimerImage_Hunter")
        {
            pos = GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(-270 + 535 * (GM.GetComponent<Timer>().Game_Timer / GM.GetComponent<Timer>().Alert_Time), -200);
        }
        else if(this.name == "TimerImage_Buffalo")
        {
            pos = GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(260 - 535 * (GM.GetComponent<Timer>().Game_Timer / GM.GetComponent<Timer>().Alert_Time), 205);
        }

    }
}
