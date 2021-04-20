using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
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
    public int TurnCnt;

    //자칼 이벤트의 턴수와 동작여부
    int JackalTurn;
    bool Jackal_Bool = false;

    // Start is called before the first frame update
    void Start()
    {
        //시작하면 1턴
        TurnCnt = 1;

        //기본 11마리
        BuffaloCnt = 11;

        //자칼 이벤트의 턴수
        JackalTurn = Random.Range(4, 10);

        //Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        playerWhite = new GameObject[]
        {
            //버팔로체스
            Create("hunter", 5, 1), Create("dog", 3, 1), Create("dog", 4, 1), Create("dog", 6, 1), Create("dog", 7, 1)

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

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            TurnCnt += 1;
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
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
    }

    void Jackal_Event()
    {
        //자칼 이벤트가 발생하기 2턴 전에
        if ((JackalTurn - 2) == TurnCnt)
        {
            //자칼 이벤트가 발생할 라인에 표시
            Debug.Log("Before Event");
        }

        //자칼 이벤트가 발생할 턴이 되면
        if (JackalTurn == TurnCnt)
        {
            //표시됬던 라인에 이벤트 발생
            Debug.Log("Event Worked");
        }
    }
}
