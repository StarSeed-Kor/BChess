using System.Collections;
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

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king")
            {
                controller.GetComponent<Game>().Winner("black");
            }
            if (cp.name == "black_king")
            {
                controller.GetComponent<Game>().Winner("white");
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
