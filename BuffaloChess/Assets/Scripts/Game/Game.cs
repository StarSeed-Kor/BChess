using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    GameObject controller;

    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[11, 7];
    //private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "black";

    private bool gameOver = false;

    //남은 물소 마릿수
    static public int BuffaloCnt;

    //현재 턴수
    static public int TurnCnt;

    //자칼 이벤트의 턴수와 동작여부
    int Jackal_Turn;
    bool Jackal_Bool = false;
    int Jackal_Line;
    public GameObject Jackal_Zone;

    // Start is called before the first frame update
    void Start()
    {
        //시작하면 1턴
        TurnCnt = 1;

        //기본 11마리
        BuffaloCnt = 11;

        //자칼 이벤트의 턴수
        Jackal_Turn = Random.Range(4, 10);//4~9턴 사이
        Jackal_Bool = false;
        Jackal_Line = Random.Range(1, 5);//어느 라인에 생길지
        Jackal_Zone.SetActive(false);

        //Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        playerWhite = new GameObject[]
        {
            //버팔로체스
            Create("hunter", 5, 1), Create("dog", 3, 1), Create("dog", 4, 1), Create("dog", 6, 1), Create("dog", 7, 1)
            //, Create("dog", 0, 0), Create("dog", 10, 0)

            //일반 체스
            /*Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0),
            Create("white_queen", 3, 0), Create("white_king", 4, 0), Create("white_bishop", 5, 0),
            Create("white_knight", 6, 0), Create("white_rook", 7, 0), Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1), Create("white_pawn", 5, 1), Create("white_pawn", 6, 1),
            Create("white_pawn", 7, 1)*/
        };
        playerBlack = new GameObject[]
        {
            //버팔로체스
            Create("buffalo", 0, 6),Create("buffalo", 1, 6),Create("buffalo", 2, 6),Create("buffalo", 3, 6),
            Create("buffalo", 4, 6),Create("buffalo", 5, 6),Create("buffalo", 6, 6),Create("buffalo", 7, 6),
            Create("buffalo", 8, 6),Create("buffalo", 9, 6),Create("buffalo", 10, 6)

            //일반 체스
            /*Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7),
            Create("black_queen", 3, 7), Create("black_king", 4, 7), Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7), Create("black_rook", 7, 7), Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6), Create("black_pawn", 5, 6), Create("black_pawn", 6, 6),
            Create("black_pawn", 7, 6)*/
        };

        for(int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            //SetPosition(playerBlack[i]);
        }

        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            //SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessManager CM = obj.GetComponent<ChessManager>();

        CM.name = name;
        CM.SetXBoard(x);
        CM.SetYBoard(y);
        CM.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        ChessManager CM = obj.GetComponent<ChessManager>();

        positions[CM.GetXBoard(), CM.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            return false;
        }

        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public int GetTurnCnt()
    {
        return TurnCnt;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            TurnCnt += 1;
            Jackal_Event();
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }

        GameObject GM = GameObject.Find("GameManager");
        GM.GetComponent<Timer>().Game_Timer = GM.GetComponent<Timer>().Max_Time;
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the Winner";
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }

        if (currentPlayer == "black")
        {
            Buffalo_Check();
        }
    }

    void Buffalo_Check()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        Game sc = controller.GetComponent<Game>();
        int Cnt = 0;

        GameObject[] Mals = GameObject.FindGameObjectsWithTag("Buffalo");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Buffalo").Length; i++)
        {
            if (sc.GetPosition(Mals[i].GetComponent<ChessManager>().GetXBoard()
                    , Mals[i].GetComponent<ChessManager>().GetYBoard() - 1) != null)
            {
                Cnt += 1;
            }
        }

        //현재 이동 가능한 버팔로의 수가 0이면 사냥꾼 승리
        if ((GameObject.FindGameObjectsWithTag("Buffalo").Length - Cnt) == 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().Winner("Hunter");
        }
    }

    void Jackal_Event()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        Game sc = controller.GetComponent<Game>();

        //자칼 이벤트가 발생하기 2턴 전에
        if ((Jackal_Turn - 2) == TurnCnt && Jackal_Bool == false)
        {
            //자칼 이벤트가 발생할 라인에 표시
            Debug.Log("Before Event");
            Jackal_Bool = true;

            Jackal_Zone.SetActive(true);
            Jackal_Zone.transform.GetChild(1).position = new Vector3(0.15f, -2.24f + (1.165f * Jackal_Line), 0);
        }

        //자칼 이벤트가 발생할 턴이 되면
        if (Jackal_Turn == TurnCnt)
        {
            //자칼 지나가는 애니메이션 실행
            GameObject.Find("Jackal_Anim").GetComponent<Animator>().Play("Jackal_Event");

            //표시됬던 라인에 이벤트 발생
            Debug.Log("Event Worked");
            Jackal_Zone.SetActive(false);

            GameObject[] Mals = GameObject.FindGameObjectsWithTag("Buffalo");
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Buffalo").Length; i++)
            {
                if(Mals[i].GetComponent<ChessManager>().GetYBoard() == (Jackal_Line + 1))
                {
                    controller.GetComponent<Game>().SetPositionEmpty(Mals[i].GetComponent<ChessManager>().GetXBoard()
                        , Mals[i].GetComponent<ChessManager>().GetYBoard());

                    Mals[i].GetComponent<ChessManager>().SetYBoard(6);
                    Mals[i].GetComponent<ChessManager>().SetCoord();

                    controller.GetComponent<Game>().SetPosition(Mals[i]);
                }
            }

            Mals = GameObject.FindGameObjectsWithTag("Dog");
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Dog").Length; i++)
            {
                if (Mals[i].GetComponent<ChessManager>().GetYBoard() == (Jackal_Line + 1))
                {
                    controller.GetComponent<Game>().SetPositionEmpty(Mals[i].GetComponent<ChessManager>().GetXBoard()
                        , Mals[i].GetComponent<ChessManager>().GetYBoard());

                    Mals[i].GetComponent<ChessManager>().SetYBoard(1);

                    GameObject cp = sc.GetPosition(Mals[i].GetComponent<ChessManager>().GetXBoard(), Mals[i].GetComponent<ChessManager>().GetYBoard());
                    while (cp != null && Mals[i].GetComponent<ChessManager>().GetXBoard() < 10)
                    {
                        //만약 자리에 뭔가가 있으면 우측으로
                        Mals[i].GetComponent<ChessManager>().SetXBoard(Mals[i].GetComponent<ChessManager>().GetXBoard() + 1);
                        cp = sc.GetPosition(Mals[i].GetComponent<ChessManager>().GetXBoard(), Mals[i].GetComponent<ChessManager>().GetYBoard());
                    }
                    if (Mals[i].GetComponent<ChessManager>().GetXBoard() == 10)//우측 끝이면 좌로
                    {
                        while (cp != null && Mals[i].GetComponent<ChessManager>().GetXBoard() > 0)
                        {
                            //만약 자리에 뭔가가 있으면 우측으로
                            Mals[i].GetComponent<ChessManager>().SetXBoard(Mals[i].GetComponent<ChessManager>().GetXBoard() - 1);
                            cp = sc.GetPosition(Mals[i].GetComponent<ChessManager>().GetXBoard(), Mals[i].GetComponent<ChessManager>().GetYBoard());
                        }
                    }
                    Mals[i].GetComponent<ChessManager>().SetCoord();
                    controller.GetComponent<Game>().SetPosition(Mals[i]);
                }
            }

            GameObject Mal = GameObject.FindWithTag("Hunter");
            if (Mal.GetComponent<ChessManager>().GetYBoard() == (Jackal_Line + 1))
            {
                controller.GetComponent<Game>().SetPositionEmpty(Mal.GetComponent<ChessManager>().GetXBoard()
                        , Mal.GetComponent<ChessManager>().GetYBoard());

                Mal.GetComponent<ChessManager>().SetYBoard(1);

                GameObject cp = sc.GetPosition(Mal.GetComponent<ChessManager>().GetXBoard(), Mal.GetComponent<ChessManager>().GetYBoard());
                while (cp != null && Mal.GetComponent<ChessManager>().GetXBoard() < 10)
                {
                    //만약 자리에 뭔가가 있으면 우측으로
                    Mal.GetComponent<ChessManager>().SetXBoard(Mal.GetComponent<ChessManager>().GetXBoard() + 1);
                    cp = sc.GetPosition(Mal.GetComponent<ChessManager>().GetXBoard(), Mal.GetComponent<ChessManager>().GetYBoard());
                }
                if (Mal.GetComponent<ChessManager>().GetXBoard() == 10)//우측 끝이면 좌로
                {
                    while (cp != null && Mal.GetComponent<ChessManager>().GetXBoard() > 0)
                    {
                        //만약 자리에 뭔가가 있으면 우측으로
                        Mal.GetComponent<ChessManager>().SetXBoard(Mal.GetComponent<ChessManager>().GetXBoard() - 1);
                        cp = sc.GetPosition(Mal.GetComponent<ChessManager>().GetXBoard(), Mal.GetComponent<ChessManager>().GetYBoard());
                    }
                }
                Mal.GetComponent<ChessManager>().SetCoord();
                controller.GetComponent<Game>().SetPosition(Mal);
            }

            Jackal_Bool = true;
        }
    }
}
