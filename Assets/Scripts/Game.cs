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

    void Start()
    {
        playerSente = new GameObject[]
        {
Create("V_sente",4,0), Create("X_sente",3,0),Create("X_sente",5,0),Create("X_sente",8,7)
        };
        playerGote = new GameObject[]
        {
Create("V_gote",4,8)
        };
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

        // Kiểm tra nếu vua bị chiếu và in thông báo
        if (CheckBoard(positions, currentPlayer))
        {
            Debug.Log("Chieu!!!");
        }

        if (CheckMate(currentPlayer, positions))
        {
            Debug.Log("Chieu Het!!!");
        }
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
        Debug.Log("Generated State: " + state.ToString());
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

