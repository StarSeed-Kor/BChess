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

        if (this.name == "TimerImage_Hunter")
        {
            //출발위치 (680,354), 도착위치 (-590 , 354) 1300 이동
            pos = GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(-620 + 1300 * (GM.GetComponent<Timer>().Game_Timer / GM.GetComponent<Timer>().Alert_Time), -620);
        }
        else if(this.name == "TimerImage_Buffalo")
        {
            //출발위치 (-646,-620), 도착위치 (656 , -620) 1300 이동
            pos = GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(654 - 1300 * (GM.GetComponent<Timer>().Game_Timer / GM.GetComponent<Timer>().Alert_Time), 354);
        }

    }
}
