using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Game_Timer;
    public float Max_Time = 3f;

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

            GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
            for (int i = 0; i < movePlates.Length; i++)
            {
                Destroy(movePlates[i]);
            }

            controller.GetComponent<Game>().NextTurn();

            Game_Timer = Max_Time;
        }
    }
}
