using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [HideInInspector]
    public float Game_Timer;
    public float Max_Time;
    public Text TurnText;
    public Text PlayerText;

    public GameObject controller;
    /*GameObject reference = null;*/

    public GameObject[] Enemy_Timer_Object = new GameObject[3];
    public GameObject[] Player_Timer_Object = new GameObject[3];

    //Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Max_Time은 Unity Hierarchy 내의 GameManager 건드리기
        //Max_Time = 6f;
        Game_Timer = Max_Time;
    }

    // Update is called once per frame
    void Update()
    {
        Game_Timer -= Time.deltaTime;

        Timer_Update();
        Buffalo_Check();
        Alert_Time();

        Debug.Log(Max_Time);
    }

    void Alert_Time()
    {
        if(controller.GetComponent<Game>().GetCurrentPlayer() == "white")
        {
            if (Game_Timer <= 3f)
            {
                Player_Timer_Object[0].SetActive(true);
            }
            if (Game_Timer <= 2f)
            {
                Player_Timer_Object[1].SetActive(true);
            }
            if (Game_Timer <= 1f)
            {
                Player_Timer_Object[2].SetActive(true);
            }
        }
        else if(controller.GetComponent<Game>().GetCurrentPlayer() == "black")
        {
            if ( Game_Timer <= 3f)
            {
                Enemy_Timer_Object[0].SetActive(true);
            }
            if (Game_Timer <= 2f)
            {
                Enemy_Timer_Object[1].SetActive(true);
            }
            if (Game_Timer <= 1f)
            {
                Enemy_Timer_Object[2].SetActive(true);
            }
        }
    }

    void Buffalo_Check()
    {
        if (Game_Timer < 0)
        {
            Debug.Log("Next Turn!");

            controller = GameObject.FindGameObjectWithTag("GameController");

            GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
            for (int i = 0; i < movePlates.Length; i++)
            {
                Destroy(movePlates[i]);
            }

            controller.GetComponent<Game>().NextTurn();

            for(int i = 0; i < 3; i++)
            {
                Enemy_Timer_Object[i].SetActive(false);
                Player_Timer_Object[i].SetActive(false);
            }

            Game_Timer = Max_Time;
        }
    }

    public void Timer_Update()
    {
        GameObject.Find("Time_Slider").GetComponent<Slider>().maxValue = Max_Time;
        GameObject.Find("Time_Slider").GetComponent<Slider>().value = Game_Timer;

        TurnText.text = "Turn : " + controller.GetComponent<Game>().GetTurnCnt().ToString();
        PlayerText.text = controller.GetComponent<Game>().GetCurrentPlayer();
    }
}
