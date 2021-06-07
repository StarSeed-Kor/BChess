using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [HideInInspector]
    public float Game_Timer;
    public float Max_Time;
    public float Total_Time;
    public Text TurnText;
    public Text PlayerText;
    public Text Total_TimeText;

    public GameObject controller;
    /*GameObject reference = null;*/

    public GameObject Timer_Buffalo;
    public GameObject Timer_Hunter;

    // Start is called before the first frame update
    void Start()
    {
        //Max_Time은 Unity Hierarchy 내의 GameManager 건드리기
        //Max_Time = 6f;
        Game_Timer = Max_Time;
        Total_Time = 0;
}

    // Update is called once per frame
    void Update()
    {
        if (this.name == "GameManager")
        {
            Game_Timer -= Time.deltaTime;
            Total_Time += Time.deltaTime;

            Timer_Update();
            Buffalo_Check();
            Debug.Log(Max_Time);
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

            Game_Timer = Max_Time;
        }
    }

    public void Timer_Update()
    {
        if (controller.GetComponent<Game>().GetCurrentPlayer() == "black")
        {
            Timer_Buffalo.SetActive(true);
            GameObject.Find("Rope_Buffalo").GetComponent<Image>().fillAmount = Game_Timer / Max_Time;
            Timer_Hunter.SetActive(false);

            if (Game_Timer < 1f)
            {
                GameObject.Find("TimeEdge_Buffalo").GetComponent<Animator>().SetTrigger("Event_On");
            }
        }
        else if (controller.GetComponent<Game>().GetCurrentPlayer() == "white")
        {
            Timer_Hunter.SetActive(true);
            GameObject.Find("Rope_Hunter").GetComponent<Image>().fillAmount = Game_Timer / Max_Time;
            Timer_Buffalo.SetActive(false);

            if (Game_Timer < 1f)
            {
                GameObject.Find("TimeEdge_Hunter").GetComponent<Animator>().SetTrigger("Event_On");
            }
        }

        TurnText.text = "Turn : " + controller.GetComponent<Game>().GetTurnCnt().ToString();
        //PlayerText.text = controller.GetComponent<Game>().GetCurrentPlayer();
        Total_TimeText.text = "Total : " + Total_Time.ToString("N1");

        if (controller.GetComponent<Game>().GetCurrentPlayer() == "black")
        {
            PlayerText.text = "Buffalo";
        }
        else if (controller.GetComponent<Game>().GetCurrentPlayer() == "white")
        {
            PlayerText.text = "Hunter";
        }
    }

    public void Reset_Timer()
    {
        GetComponent<Animator>().SetTrigger("Event_Off");
    }
}
