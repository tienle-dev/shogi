using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ShogiPeice;

    private GameObject[,] positions = new GameObject[9, 9];
    private GameObject[] playerGote = new GameObject[20];
    private GameObject[] playerSente = new GameObject[20];

    private string currentPlayer = "Sente"; 

    private bool gameover = false;

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
        for (int i = 0; i < playerSente.Length; i++)
        {
            SetPosition(playerSente[i]);
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
        if(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
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
    }
    public void Update()
    {
        if(gameover == true && Input.GetMouseButtonDown(0))
        {
            gameover = false;
            SceneManager.LoadScene("Game");
        }
        
    }
    public void Winner(string playerWinner)
    {
        gameover = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " Win!";

    }
    public bool IsKingReachabel(string player, GameObject[,] positions)
    {
        GameObject king = null;
        if (player == "Gote")
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (positions[i, j] != null && positions[i, j].name == "V_gote")
                    {
                        king = positions[i, j];
                        break;
                    }
                }
            }
        }
        else if (player == "Sente")
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (positions[i, j] != null && positions[i, j].name == "V_sente")
                    {
                        king = positions[i, j];
                        break;
                    }
                }
            }
        }
        int kingX = king.GetComponent<ShogiMan>().getXBoard();
        int kingY = king.GetComponent<ShogiMan>().getYBoard();
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                GameObject piece = positions[i, j];
                if (piece != null)
                {
                    ShogiMan sm = piece.GetComponent<ShogiMan>();
                    if(sm.GetPlayer() != player)
                    {
                        int originalX = sm.getXBoard();
                        int originalY = sm.getYBoard();
                    
                    sm.InitiateMovePlates();
                    sm.DestroyMovePlates();

                    GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
                    foreach (GameObject movePlate in movePlates)
                    {
                        MovePlate mp = movePlate.GetComponent<MovePlate>();
                        if (mp.matrixX == kingX && mp.matrixY == kingY)
                        {
                            sm.DestroyMovePlates();
                            return true;
                        }
                    }
                    sm.DestroyMovePlates();

                    sm.setXBoard(originalX);
                    sm.setYBoard(originalY);
                    }
                }
            }
        }
        return false;
    }
   
}
