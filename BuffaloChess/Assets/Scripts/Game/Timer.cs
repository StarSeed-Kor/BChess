using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float Game_Timer;
    public float Max_Time = 3f;
    public Text TurnText;
    public Text PlayerText;

    public GameObject controller;
    GameObject reference = null;

    // Start is called before the first frame update
    void Start()
    {
        Game_Timer = Max_Time;
    }

    // Update is called once per frame
    void Update()
    {
        Game_Timer -= Time.deltaTime;

        Timer_Update();
        Buffalo_Check();
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
        GameObject.Find("Time_Slider").GetComponent<Slider>().maxValue = Max_Time;
        GameObject.Find("Time_Slider").GetComponent<Slider>().value = Game_Timer;

        TurnText.text = "Turn : " + controller.GetComponent<Game>().GetTurnCnt().ToString();
        PlayerText.text = controller.GetComponent<Game>().GetCurrentPlayer();
    }
}
