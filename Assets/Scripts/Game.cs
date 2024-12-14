using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.Linq;
public class Game : MonoBehaviour
{
    public GameObject ShogiPeice;
    private List<string> boardStates = new List<string>();

    public GameObject[,] positions = new GameObject[9, 9];
    private GameObject[] playerGote = new GameObject[20];
    private GameObject[] playerSente = new GameObject[20];

    public GameObject controller;

    private string currentPlayer = "Sente";

    private bool gameover = false;

    private bool capturePiece = false;

    void Start()
    {
        playerSente = new GameObject[]
        {
Create("Th_sente",0,0 ), Create("M_sente",1,0),Create("B_sente",2,0), Create("Vg_sente",3,0),
Create("V_sente",4,0), Create("Vg_sente",5,0), Create("B_sente",6,0), Create("M_sente",7,0),
Create("Th_sente",8,0), Create("Tg_sente",1,1), Create("X_sente",7,1), Create("T_sente",0,2),
Create("T_sente",1,2), Create("T_sente",2,2), Create("T_sente",3,2), Create("T_sente",4,2),
Create("T_sente",5,2), Create("T_sente",6,2), Create("T_sente",7,2), Create("T_sente",8,2)
        };
        playerGote = new GameObject[]
        {
Create("Th_gote",0,8 ), Create("M_gote",1,8),Create("B_gote",2,8), Create("Vg_gote",3,8),
Create("V_gote",4,8), Create("Vg_gote",5,8), Create("B_gote",6,8), Create("M_gote",7,8),
Create("Th_gote",8,8), Create("Tg_gote",7,7), Create("X_gote",1,7),Create("T_gote",0,6),
Create("T_gote",1,6), Create("T_gote",2,6), Create("T_gote",3,6), Create("T_gote",4,6),
Create("T_gote",5,6), Create("T_gote",6,6), Create("T_gote",7,6), Create("T_gote",8,6)
        };
        //Drop = new GameObject[]
        //{
        //    Create("Th_sente",0,-1 ), Create("M_sente",1,-1),Create("B_sente",2,-1), Create("Vg_sente",3,-1)
        //};
        for (int i = 0; i < playerSente.Length; i++)
        {
            SetPosition(playerSente[i]);
        }
        for (int i = 0; i < playerGote.Length; i++)
        {
            SetPosition(playerGote[i]);
        }
    }
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(ShogiPeice, new Vector3(0, 0, -1), Quaternion.identity);
        ShogiMan sm = obj.GetComponent<ShogiMan>();
        sm.name = name;
        sm.setXBoard(x);
        sm.setYBoard(y);
        sm.Activate();
        return obj;
    }
    public void SetPosition(GameObject obj)
    {
        ShogiMan sm = obj.GetComponent<ShogiMan>();
        positions[sm.getXBoard(), sm.getYBoard()] = obj;
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
        return x >= 0 && x < 9 && y >= 0 && y < 9;
    }

    public void GetName(string pieceName)
    {
        Debug.Log("Captured piece: " + pieceName);
        if (pieceName == "Vg_sente") pieceName = "Vg_gote";
        if (pieceName == "Vg_gote") pieceName = "Vg_sente";
        if (pieceName == "X_sente") pieceName = "X_gote";
        if (pieceName == "plusX_sente") pieceName = "X_gote";
        if (pieceName == "Tg_sente") pieceName = "Tg_gote";
        if (pieceName == "plusTg_sente") pieceName = "Tg_gote";
        if (pieceName == "Th_sente") pieceName = "Th_gote";
        if (pieceName == "plusTh_sente") pieceName = "Th_gote";
        if (pieceName == "M_sente") pieceName = "M_gote";
        if (pieceName == "plusM_sente") pieceName = "M_gote";
        if (pieceName == "B_sente") pieceName = "B_gote";
        if (pieceName == "plusS_sente") pieceName = "B_gote";
        if (pieceName == "T_sente") pieceName = "T_gote";
        if (pieceName == "plusT_sente") pieceName = "T_gote";

        if (pieceName == "Vg_gote") pieceName = "Vg_sente";
        if (pieceName == "Vg_sente") pieceName = "Vg_gote";
        if (pieceName == "X_gote") pieceName = "X_sente";
        if (pieceName == "plusX_gote") pieceName = "X_sente";
        if (pieceName == "Tg_gote") pieceName = "Tg_sente";
        if (pieceName == "plusTg_gote") pieceName = "Tg_sente";
        if (pieceName == "Th_gote") pieceName = "Th_sente";
        if (pieceName == "plusTh_gote") pieceName = "Th_sente";
        if (pieceName == "M_gote") pieceName = "M_sente";
        if (pieceName == "plusM_gote") pieceName = "M_sente";
        if (pieceName == "B_gote") pieceName = "B_sente";
        if (pieceName == "plusS_gote") pieceName = "B_sente";
        if (pieceName == "T_gote") pieceName = "T_sente";
        if (pieceName == "plusT_gote") pieceName = "T_sente";

        Debug.Log("Changes piece:" +  pieceName);
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }
    public bool IsGameOver()
    {
        return gameover;
    }
    public void NextTurn()
    {
        if (currentPlayer == "Sente")
        {
            currentPlayer = "Gote";
        }
        else
        {
            currentPlayer = "Sente";
        }
        if (Stalemated(currentPlayer, positions))
        {
            Debug.Log("The co be tac !!!!!!!!");
        }
        if (FourfoldRepition(currentPlayer, positions))
        {
            Debug.Log("Hoa co");
        }
    }
    public void Update()
    {
        if (gameover && Input.GetMouseButtonDown(0))
        {
            gameover = false;
            SceneManager.LoadScene("Game");
        }
        /*
        // Kiểm tra nếu vua bị chiếu và in thông báo
        if (CheckBoard(positions, currentPlayer))
        {
            Debug.Log("Chieu!!!");
        }

        if (CheckMate(currentPlayer, positions))
        {
            Debug.Log("Chieu Het!!!");
        }*/
    }
    public void Winner(string playerWinner)
    {
        gameover = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " Win!";
    }
    public void Draw(string playerDraw)
    {
        gameover = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = "Draw!";
    }
    public bool CheckBoard(GameObject[,] board, string player)
    {

        bool kingInCheck = false;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i, j] != null)
                {

                    if (player == "Sente" && board[i, j].name == "V_sente")
                    {
                        if (
                            LookForKnight(board, "M_gote", i, j) ||
                            LookForPawn(board, "T_gote", i, j) ||
                            LookForRook(board, "X_gote", i, j) ||
                            LookForBishop(board, "Tg_gote", i, j) ||
                            LookForKing(board, "V_gote", i, j))
                        {
                            kingInCheck = true;
                        }
                    }
                    else if (player == "Gote" && board[i, j].name == "V_gote")
                    {

                        if (
                            LookForKnight(board, "M_sente", i, j) ||
                            LookForPawn(board, "T_sente", i, j) ||
                            LookForRook(board, "X_sente", i, j) ||
                            LookForBishop(board, "Tg_sente", i, j) ||
                            LookForKing(board, "V_sente", i, j))
                        {
                            kingInCheck = true;
                        }
                    }
                }
            }
        }
        return kingInCheck;
    }
    private bool LookForKing(GameObject[,] board, string name, int i, int j)
    {
        int[] x = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] y = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int k = 0; k < 8; k++)
        {
            int m = i + x[k];
            int n = j + y[k];

            if (PositionOnBoard(m, n) && board[m, n] != null && board[m, n].name == name)
            {
                return true;
            }
        }
        return false;
    }

    // Thêm các hàm tương tự cho Knight, Rook, Bishop, Pawn, và Queen...
    public bool LookForKnight(GameObject[,] board, string name, int i, int j)
    {
        int[] x = { 1, -1 };
        int[] y = { -2, -2 };
        for (int k = 0; k < x.Length; k++)
        {
            if (name == "M_sente")
            {
                int m = i + x[k];
                int n = j + y[k];
                if (PositionOnBoard(m, n) && board[m, n] != null && board[m, n].name == name)
                {
                    return true;
                }
            }
            if (name == "M_gote")
            {
                int m = i - x[k];
                int n = j - y[k];
                if (PositionOnBoard(m, n) && board[m, n] != null && board[m, n].name == name)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool LookForRook(GameObject[,] board, string name, int i, int j)
    {
        // Check downwards
        int k = 0;
        while (PositionOnBoard(i + ++k, j)) // Check if the new position is within bounds
        {
            if (board[i + k, j] != null && board[i + k, j].name == name)
            {
                return true;
            }
            else
            {
                if (board[i + k, j] != null) // If an object is encountered, stop
                {
                    break;
                }
            }
        }
        // Check upwards
        k = 0;
        while (PositionOnBoard(i + --k, j)) // Check if the new position is within bounds
        {
            if (board[i + k, j] != null && board[i + k, j].name == name)
                return true;
            if (board[i + k, j] != null) // If an object is encountered, stop
                break;
        }
        k = 0;
        while (PositionOnBoard(i, j + ++k)) // Check if the new position is within bounds
        {
            if (board[i, j + k] != null && board[i, j + k].name == name)
                return true;
            if (board[i, j + k] != null) // If an object is encountered, stop
                break;
        }

        // Check left
        k = 0;
        while (PositionOnBoard(i, j + --k)) // Check if the new position is within bounds
        {
            if (board[i, j + k] != null && board[i, j + k].name == name)
                return true;
            if (board[i, j + k] != null) // If an object is encountered, stop
                break;
        }
        return false;
    }

    private bool LookForPawn(GameObject[,] board, string name, int i, int j)
    {
        if (name == "T_sente")
        {
            if (PositionOnBoard(i, j + 1) && board[i, j + 1] != null && board[i, j + 1].name == name)
            {
                return true;
            }
        }
        if (name == "T_gote")
        {
            if (PositionOnBoard(i, j - 1) && board[i, j - 1] != null && board[i, j - 1].name == name)
            {
                return true;
            }
        }
        return false;
    }
    private bool LookForBishop(GameObject[,] board, string name, int i, int j)
    {
        int k = 1;
        while (PositionOnBoard(i + k, j + k))
        {
            if (board[i + k, j + k] != null)
            {
                if (board[i + k, j + k].name == name)
                {
                    return true; // Quân Bishop có thể tấn công vua
                }
                else
                {
                    break; // Nếu gặp quân cờ khác thì dừng
                }
            }
            k++;
        }
        k = 1;
        while (PositionOnBoard(i + k, j - k))
        {
            if (board[i + k, j - k] != null)
            {
                if (board[i + k, j - k].name == name)
                {
                    return true; // Quân Bishop có thể tấn công vua
                }
                else
                {
                    break; // Nếu gặp quân cờ khác thì dừng
                }
            }
            k++;
        }

        // Kiểm tra diagonal lên bên phải
        k = 1;
        while (PositionOnBoard(i - k, j + k))
        {
            if (board[i - k, j + k] != null)
            {
                if (board[i - k, j + k].name == name)
                {
                    return true; // Quân Bishop có thể tấn công vua
                }
                else
                {
                    break; // Nếu gặp quân cờ khác thì dừng
                }
            }
            k++;
        }

        // Kiểm tra diagonal lên bên trái
        k = 1;
        while (PositionOnBoard(i - k, j - k))
        {
            if (board[i - k, j - k] != null)
            {
                if (board[i - k, j - k].name == name)
                {
                    return true; // Quân Bishop có thể tấn công vua
                }
                else
                {
                    break; // Nếu gặp quân cờ khác thì dừng
                }
            }
            k++;
        }

        return false; // Không tìm thấy quân cờ tấn công vua
    }
    public bool AllowedMove(GameObject[,] board, (int x, int y) pos, (int x, int y) move, string player)
    {
        GameObject[,] copyBoard = new GameObject[board.GetLength(0), board.GetLength(1)];
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                copyBoard[i, j] = board[i, j];
            }
        }

        // Lấy vị trí hiện tại và vị trí di chuyển
        int x = pos.x, y = pos.y;
        int newX = move.x, newY = move.y;

        // Di chuyển quân cờ và kiểm tra trạng thái vua
        GameObject movedPiece = copyBoard[x, y];
        copyBoard[newX, newY] = movedPiece;
        copyBoard[x, y] = null;
        // Chỉ kiểm tra nếu vua của người chơi hiện tại bị chiếu
        if (player == "Sente" && CheckBoard(copyBoard, "Sente"))
        {
            return false; // Không hợp lệ nếu vua của Sente bị chiếu
        }
        if (player == "Gote" && CheckBoard(copyBoard, "Gote"))
        {
            return false; // Không hợp lệ nếu vua của Gote bị chiếu
        }

        return true;
    }
    public bool Stalemated(string player, GameObject[,] board)
    {
        // Kiểm tra nếu vua đang bị chiếu
        if (CheckBoard(board, player))
        {
            return false; // Không phải stalemate nếu vua đang bị chiếu
        }

        // Duyệt qua tất cả các quân cờ của người chơi
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                GameObject piece = board[x, y];
                if (piece != null && piece.GetComponent<ShogiMan>().GetPlayer() == player)
                {
                    ShogiMan shogiMan = piece.GetComponent<ShogiMan>();
                    List<(int, int)> validMoves = shogiMan.InitiateMovePlates();

                    foreach ((int newX, int newY) in validMoves)
                    {
                        GameObject temp = board[newX, newY];
                        board[x, y] = null;
                        board[newX, newY] = piece;

                        // Nếu vua không bị chiếu sau nước đi này
                        if (!CheckBoard(board, player))
                        {
                            // Khôi phục trạng thái bàn cờ
                            board[x, y] = piece;
                            board[newX, newY] = temp;
                            return false; // Có ít nhất một nước đi hợp lệ
                        }

                        // Khôi phục trạng thái bàn cờ
                        board[x, y] = piece;
                        board[newX, newY] = temp;
                    }
                }
            }
        }

        // Nếu không có nước đi hợp lệ và vua không bị chiếu, stalemate
        return true;
    }

    public bool CheckMate(string player, GameObject[,] board)
    {
        // Kiểm tra nếu vua không bị chiếu
        if (!CheckBoard(board, player))
        {
            return false; // Không phải checkmate nếu không bị chiếu
        }
        // Duyệt qua tất cả quân cờ của người chơi
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                GameObject piece = board[x, y];
                if (piece != null && piece.GetComponent<ShogiMan>().GetPlayer() == player)
                {
                    ShogiMan shogiMan = piece.GetComponent<ShogiMan>();
                    List<(int, int)> validMoves = shogiMan.InitiateMovePlates();
                    foreach ((int newX, int newY) in validMoves)
                    {
                        // Thử di chuyển
                        GameObject temp = board[newX, newY];
                        board[x, y] = null;
                        board[newX, newY] = piece;

                        // Kiểm tra nếu vua thoát khỏi chiếu
                        if (!CheckBoard(board, player))
                        {
                            board[x, y] = piece;
                            board[newX, newY] = temp;
                            return false; // Không phải checkmate
                        }
                        // Hoàn tác
                        board[x, y] = piece;
                        board[newX, newY] = temp;
                    }
                }
            }
        }
        return true;
    }
    private string BoardState(GameObject[,] board, string player)
    {
        StringBuilder state = new StringBuilder();
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                GameObject piece = board[x, y];
                if (piece != null)
                {
                    state.Append($"{piece.name}_{x}_{y};");
                }
            }
        }
        state.Append($"{player}");
        //Debug.Log("Generated State: " + state.ToString());
        return state.ToString();
    }
    public bool FourfoldRepition(string player, GameObject[,] board)
    {
        string currentState = BoardState(board, player);
        int count = boardStates.Count(state => state == currentState);
        if (count == 4)
        {
            Debug.Log("Fourfold repetition detected!");
            return true;
        }
        boardStates.Add(currentState);
        return false;
    }
}

