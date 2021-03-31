using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    // 위치
    private int BoardX = -1;
    private int BoardY = -1;

    private string player;

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    [Space]
    [Space]

    public Sprite hunter, dog, buffalo;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoord();

        switch(this.name)
        {
            //버팔로체스
            case "hunter": this.GetComponent<SpriteRenderer>().sprite = hunter; player = "white"; break;
            case "dog": this.GetComponent<SpriteRenderer>().sprite = dog; player = "white"; break;

            case "buffalo": this.GetComponent<SpriteRenderer>().sprite = buffalo; player = "black"; break;

            //일반 체스
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    public void SetCoord()
    {
        float x = BoardX;
        float y = BoardY;

        x *= 1.1f;
        y *= 1.1f;

        //x += -5.35f; y += -2.7f;
        x += -3.85f; y += -4.15f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return BoardX;
    }

    public int GetYBoard()
    {
        return BoardY;
    }

    public void SetXBoard(int x)
    {
        BoardX = x;
    }

    public void SetYBoard(int y)
    {
        BoardY = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() &&
            controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        } 
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            //버팔로 체스
            case "hunter":
                SurroundMovePlate();
                break;
            case "dog":
                DogMovePlate(1, 0);
                DogMovePlate(0, 1);
                DogMovePlate(1, 1);
                DogMovePlate(-1, 0);
                DogMovePlate(0, -1);
                DogMovePlate(-1, -1);
                DogMovePlate(-1, 1);
                DogMovePlate(1, -1);
                break;
            case "buffalo":
                BuffaloMovePlate(BoardX, BoardY - 1);
                break;

            //일반 체스
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(BoardX, BoardY - 1);
                break;
            case "white_pawn":
                PawnMovePlate(BoardX, BoardY + 1);
                break;
        }
    }

    //버팔로 체스
    public void DogMovePlate(int xIncreament, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = BoardX + xIncreament;
        int y = BoardY + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncreament;
            y += yIncrement;
        }
    }

    public void BuffaloMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }
        }
    }

    //일반 체스
    public void LineMovePlate(int xIncreament, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = BoardX + xIncreament;
        int y = BoardY + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncreament;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessManager>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(BoardX + 1, BoardY + 2);
        PointMovePlate(BoardX - 1, BoardY + 2);
        PointMovePlate(BoardX + 2, BoardY + 1);
        PointMovePlate(BoardX + 2, BoardY - 1);
        PointMovePlate(BoardX + 1, BoardY - 2);
        PointMovePlate(BoardX - 1, BoardY - 2);
        PointMovePlate(BoardX - 2, BoardY + 1);
        PointMovePlate(BoardX - 2, BoardY - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(BoardX, BoardY + 1);
        PointMovePlate(BoardX, BoardY - 1);
        PointMovePlate(BoardX - 1, BoardY - 1);
        PointMovePlate(BoardX - 1, BoardY - 0);
        PointMovePlate(BoardX - 1, BoardY + 1);
        PointMovePlate(BoardX + 1, BoardY - 1);
        PointMovePlate(BoardX + 1, BoardY - 0);
        PointMovePlate(BoardX + 1, BoardY + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if(sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<ChessManager>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if(sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x+1, y) != null &&
                sc.GetPosition(x+1, y).GetComponent<ChessManager>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<ChessManager>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.1f;
        y *= 1.1f;

        //x += -5.35f; y += -2.7f;
        x += -3.85f; y += -4.15f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.1f;
        y *= 1.1f;

        //x += -5.35f; y += -2.7f;
        x += -3.85f; y += -4.15f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
