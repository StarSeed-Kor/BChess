﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //보드판 내의 움직임
    int matrixX;
    int matrixY;

    // false면 움직이고, true면 공격
    public bool attack = false;

    // Start is called before the first frame update
    public void Start()
    {
        if (attack)
        {
            //붉은 색으로 바꿈
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
        }
    }
    //테스트용 만약 클릭했을 시 승패와 보상 저장
    public void BtnClick()
    {
        GameObject.Find("GameManager").GetComponent<Rating>().LoadFile();
        if (GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star1 &&
            !GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star2 &&
            !GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star3)
        {
            GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star2 = true;
            GameObject.Find("GameManager").GetComponent<Rating>().SaveFile();
        }

        else if (GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star1 &&
             GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star2 &&
             !GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star3)
        {
            GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star3 = true;
            GameObject.Find("GameManager").GetComponent<Rating>().SaveFile();
        }

        else if (GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star1 &&
             GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star2 &&
              GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star3)
        {
            GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Rank += 1;
            GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star1 = true;
            GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star2 = false;
            GameObject.Find("GameManager").GetComponent<Rating>().wlList[0].Star3 = false;
            GameObject.Find("GameManager").GetComponent<Rating>().SaveFile();
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king")
            {
                controller.GetComponent<Game>().Winner("black");
                //블랙 승 저장
                //화이트 패 저장
            }
            if (cp.name == "black_king")
            {
                controller.GetComponent<Game>().Winner("white");
                //화이트 승 저장
                //GameObject.Find("GameManager").GetComponent<Rating>().LoadFile();
                //GameObject.Find("GameManager").GetComponent<Rating>().wlList.Add(new WLRecord("랭크", "보상")); ;
                //GameObject.Find("GameManager").GetComponent<Rating>().SaveFile();
                //블랙 패 저장
            }

            if (cp.name == "buffalo")
            {
                Game.BuffaloCnt -= 1;
                Debug.Log(Game.BuffaloCnt);
                if(Game.BuffaloCnt == 0)
                {
                    controller.GetComponent<Game>().Winner("Hunter");
                }
            }
                
            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<ChessManager>().GetXBoard(),
            reference.GetComponent<ChessManager>().GetYBoard());

        reference.GetComponent<ChessManager>().SetXBoard(matrixX);
        reference.GetComponent<ChessManager>().SetYBoard(matrixY);
        reference.GetComponent<ChessManager>().SetCoord();

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<ChessManager>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
