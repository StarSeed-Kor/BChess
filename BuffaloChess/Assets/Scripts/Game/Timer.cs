using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float Game_Timer;
    float Max_Time = 3f;

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

        if (Game_Timer < 0)
        {
            Debug.Log("Next Turn!");

            controller = GameObject.FindGameObjectWithTag("GameController");

            /*controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<ChessManager>().GetXBoard(),
            reference.GetComponent<ChessManager>().GetYBoard());*/

            /*reference.GetComponent<ChessManager>().SetXBoard(matrixX);
            reference.GetComponent<ChessManager>().SetYBoard(matrixY);
            reference.GetComponent<ChessManager>().SetCoord();*/

            //controller.GetComponent<Game>().SetPosition(reference);

            //controller.GetComponent<Game>().NextTurn();

            //reference.GetComponent<ChessManager>().DestroyMovePlates();

            Game_Timer = Max_Time;
        }
    }
}
