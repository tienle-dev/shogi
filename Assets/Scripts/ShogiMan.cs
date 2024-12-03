using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShogiMan : MonoBehaviour
{

    //ref
    public GameObject controller;
    public GameObject movePlate;

    //position
    private int xBoard = -1;
    private int yBoard = -1;

    protected string player;
    public string GetPlayer()
    {
        return player;
    }

    //ref all the peices
    public Sprite V_gote, X_gote, plusX_gote, Tg_gote, plusTg_gote, Vg_gote, B_gote, plusS_gote, M_gote, plusM_gote, Th_gote, plusTh_gote, T_gote, plusT_gote;
    public Sprite V_sente, X_sente, plusX_sente, Tg_sente, plusTg_sente, Vg_sente, B_sente, plusS_sente, M_sente, plusM_sente, Th_sente, plusTh_sente, T_sente, plusT_sente;


    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "V_gote": this.GetComponent<SpriteRenderer>().sprite = V_gote; player = "Gote"; break;
            case "X_gote": this.GetComponent<SpriteRenderer>().sprite = X_gote; player = "Gote"; break;
            case "plusX_gote": this.GetComponent<SpriteRenderer>().sprite = plusX_gote; player = "Gote"; break;
            case "Tg_gote": this.GetComponent<SpriteRenderer>().sprite = Tg_gote; player = "Gote"; break;
            case "plusTg_gote": this.GetComponent<SpriteRenderer>().sprite = plusTg_gote; player = "Gote"; break;
            case "Vg_gote": this.GetComponent<SpriteRenderer>().sprite = Vg_gote; player = "Gote"; break;
            case "B_gote": this.GetComponent<SpriteRenderer>().sprite = B_gote; player = "Gote"; break;
            case "plusS_gote": this.GetComponent<SpriteRenderer>().sprite = plusS_gote; player = "Gote"; break;
            case "M_gote": this.GetComponent<SpriteRenderer>().sprite = M_gote; player = "Gote"; break;
            case "plusM_gote": this.GetComponent<SpriteRenderer>().sprite = plusM_gote; player = "Gote"; break;
            case "Th_gote": this.GetComponent<SpriteRenderer>().sprite = Th_gote; player = "Gote"; break;
            case "plusTh_gote": this.GetComponent<SpriteRenderer>().sprite = plusTh_gote; player = "Gote"; break;
            case "T_gote": this.GetComponent<SpriteRenderer>().sprite = T_gote; player = "Gote"; break;
            case "plusT_gote": this.GetComponent<SpriteRenderer>().sprite = plusT_gote; player = "Gote"; break;

            case "V_sente": this.GetComponent<SpriteRenderer>().sprite = V_sente; player = "Sente"; break;
            case "X_sente": this.GetComponent<SpriteRenderer>().sprite = X_sente; player = "Sente"; break;
            case "plusX_sente": this.GetComponent<SpriteRenderer>().sprite = plusX_sente; player = "Sente"; break;
            case "Tg_sente": this.GetComponent<SpriteRenderer>().sprite = Tg_sente; player = "Sente"; break;
            case "plusTg_sente": this.GetComponent<SpriteRenderer>().sprite = plusTg_sente; player = "Sente"; break;
            case "Vg_sente": this.GetComponent<SpriteRenderer>().sprite = Vg_sente; player = "Sente"; break;
            case "B_sente": this.GetComponent<SpriteRenderer>().sprite = B_sente; player = "Sente"; break;
            case "plusS_sente": this.GetComponent<SpriteRenderer>().sprite = plusS_sente; player = "Sente"; break;
            case "M_sente": this.GetComponent<SpriteRenderer>().sprite = M_sente; player = "Sente"; break;
            case "plusM_sente": this.GetComponent<SpriteRenderer>().sprite = plusM_sente; player = "Sente"; break;
            case "Th_sente": this.GetComponent<SpriteRenderer>().sprite = Th_sente; player = "Sente"; break;
            case "plusTh_sente": this.GetComponent<SpriteRenderer>().sprite = plusTh_sente; player = "Sente"; break;
            case "T_sente": this.GetComponent<SpriteRenderer>().sprite = T_sente; player = "Sente"; break;
            case "plusT_sente": this.GetComponent<SpriteRenderer>().sprite = plusT_sente; player = "Sente"; break;
        }

    }
    public void SetCoords()
    {
        
        float cellSize = 0.7f * 1.121f;
        float x = xBoard * cellSize;
        float y = yBoard * cellSize;

       
        float xOffset = -3.166f; 
        float yOffset = -3.06f; 

        x += xOffset;
        y += yOffset;

        
        this.transform.position = new Vector3(x, y, -1.0f);
    }
    public int getXBoard()
    {
        return xBoard;
    }
    public int getYBoard()
    {
        return yBoard;
    }
    public void setXBoard(int x)
    {
        xBoard = x;
    }
    public void setYBoard(int y)
    {
        yBoard = y;
    }
    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
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
            case "V_gote":
            case "V_sente":
                SurroundMovePlate();
                break;
            case "X_gote":
            case "X_sente":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "plusX_gote":
            case "plusX_sente":
                RookPlusMovePlate(); // Quân xe phong cấp
                break;
            case "Tg_gote":
            case "Tg_sente":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                break;
            case "plusTg_gote":
            case "plusTg_sente":
                BishopPlusMovePlate(); // Tượng phong cấp (di chuyển như tượng + vua)
                break;
            case "Vg_gote":
            case "Vg_sente":
            case "plusT_gote":
            case "plusT_sente":
                GoldMovePlate();
                break;
            case "B_gote":
            case "B_sente":
                SilverMovePlate();
                break;
            case "plusS_gote":
            case "plusS_sente":
                GoldMovePlate(); // Tướng bạc phong cấp di chuyển như quân vàng
                break;
            case "M_gote":
            case "M_sente":
                LMovePlate();
                break;
            case "plusM_gote":
            case "plusM_sente":
                GoldMovePlate(); // Mã phong cấp di chuyển như quân vàng
                break;
            case "Th_gote":
            case "Th_sente":
                LineMovePlate(0, 1);
                LineMovePlate(0, -1);
                break;
            case "plusTh_gote":
            case "plusTh_sente":
                GoldMovePlate(); // Hương xa phong cấp di chuyển như quân vàng
                break;
            case "T_gote":
                Pawn2MovePlate();
                break;
            case "T_sente":
                Pawn1MovePlate();
                break;
        }
    }

    public void BishopPlusMovePlate()
    {
        LineMovePlate(1, 1);
        LineMovePlate(-1, 1);
        LineMovePlate(1, -1);
        LineMovePlate(-1, -1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
    }

    public void RookPlusMovePlate()
    {
        // Nước đi của xe
        LineMovePlate(1, 0);  // Đi ngang phải
        LineMovePlate(-1, 0); // Đi ngang trái
        LineMovePlate(0, 1);  // Đi dọc lên
        LineMovePlate(0, -1); // Đi dọc xuống

        // Kết hợp thêm nước đi của vua
        PointMovePlate(xBoard + 1, yBoard + 1); // Chéo trên phải
        PointMovePlate(xBoard + 1, yBoard - 1); // Chéo dưới phải
        PointMovePlate(xBoard - 1, yBoard + 1); // Chéo trên trái
        PointMovePlate(xBoard - 1, yBoard - 1); // Chéo dưới trái
    }


    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        if(sc.PositionOnBoard(x, y) && sc.GetPosition(x,y).GetComponent<ShogiMan>().player != player)
        {
            MovePlateAttackSpawn(x,y);
        }
    }
    public void SilverMovePlate()
    {
        if (player == "Sente")
        {
            PointMovePlate(xBoard, yBoard + 1);
            PointMovePlate(xBoard - 1, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard + 1);
            PointMovePlate(xBoard + 1, yBoard - 1);
            PointMovePlate(xBoard + 1, yBoard + 1);
        }
        else
        {
            PointMovePlate(xBoard, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard + 1);
            PointMovePlate(xBoard + 1, yBoard - 1);
            PointMovePlate(xBoard + 1, yBoard + 1);
        }
    }
    public void GoldMovePlate()
    {
        if(player == "Sente") 
        { 
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
        }
        else
        {
            PointMovePlate(xBoard, yBoard + 1);
            PointMovePlate(xBoard, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard - 0);
            PointMovePlate(xBoard + 1, yBoard - 1);
            PointMovePlate(xBoard + 1, yBoard - 0);
        }
    }
    public void LMovePlate()
    {
        if (this.name == "M_sente")
        {
            // Nước đi của quân M_sente
            PointMovePlate(xBoard + 1, yBoard + 2);
            PointMovePlate(xBoard - 1, yBoard + 2);
        }
        else if (this.name == "M_gote")
        {
            // Nước đi của quân M_gote (ngược hướng)
            PointMovePlate(xBoard + 1, yBoard - 2);
            PointMovePlate(xBoard - 1, yBoard - 2);
        }
    }
    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard , yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1 , yBoard - 1 );
        PointMovePlate(xBoard - 1, yBoard - 0 );
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }
    public void Pawn1MovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
    }
    public void Pawn2MovePlate()
    {
        PointMovePlate(xBoard, yBoard - 1);
    }
    public void PointMovePlate(int x, int y)
    {
        if (x < 0 || x >= 9 || y < 0 || y >= 9)
        {
            Debug.LogWarning("Attempted to access position out of bounds: (" + x + ", " + y + ")");
            return;
        }
        Game sc = controller.GetComponent<Game>();
        GameObject sp = sc.GetPosition(x, y);
        if (sp == null)
        {
            Debug.Log("Position is empty at: (" + x + ", " + y + ")");
            MovePlateSpawn(x, y);
        }
        else
        {
            // Kiểm tra xem GameObject sp có component ShogiMan không
            ShogiMan shogiMan = sp.GetComponent<ShogiMan>();
            if (shogiMan == null)
            {
                Debug.LogError("The object at position (" + x + ", " + y + ") does not have a 'ShogiMan' component.");
                return;
            }

            // Kiểm tra player của ShogiMan
            if (shogiMan.player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float cellSize = 0.7f * 1.121f;
        float x = matrixX * cellSize;
        float y = matrixY * cellSize;


        float xOffset = -3.166f;
        float yOffset = -3.06f;

        x += xOffset;
        y += yOffset;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f),Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float cellSize = 0.7f * 1.121f;
        float x = matrixX * cellSize;
        float y = matrixY * cellSize;


        float xOffset = -3.166f;
        float yOffset = -3.06f;

        x += xOffset; 
        y += yOffset;
        
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
    public void Update()
    {
        PromotionManager promotionManager = FindObjectOfType<PromotionManager>();

        switch (this.name)
        {
            case "T_sente":
            case "M_sente":
            case "Th_sente":
                if ((yBoard == 6 || yBoard == 7))
                {
                    promotionManager.OpenPromotionPanel(this);
                }
                else if (yBoard == 8)
                {
                    Promote(this);
                }
                else if (yBoard < 6)
                {
                }
                break;
            case "T_gote":
            case "M_gote":
            case "Th_gote":
                if ((yBoard == 2 || yBoard == 1))
                {
                    promotionManager.OpenPromotionPanel(this);
                }
                else if (yBoard == 0)
                {
                    Promote(this);
                }
                else if (yBoard > 2)
                {
                }
                break;
            case "B_sente":
            case "X_sente":
            case "Tg_sente":
                if ((yBoard >= 6 && yBoard <= 8))
                {
                    promotionManager.OpenPromotionPanel(this);
                }
                else if (yBoard < 6)
                {
                }
                break;
            case "B_gote":
            case "X_gote":
            case "Tg_gote":
                if ((yBoard >= 0 && yBoard <= 2))
                {
                    promotionManager.OpenPromotionPanel(this);
                }
                else if (yBoard > 2)
                {
                }
                break;
        }
    }

    public void Promote(bool isPromoted)
    {
        if (isPromoted)
        {
            switch (this.name)
            {
                case "T_sente":
                    this.name = "plusT_sente";
                    this.GetComponent<SpriteRenderer>().sprite = plusT_sente;
                    break;
                case "T_gote":
                    this.name = "plusT_gote";
                    this.GetComponent<SpriteRenderer>().sprite = plusT_gote;
                    break;
                // Thêm các trường hợp khác nếu cần
                case "Th_sente":
                    this.name = "plusTh_sente";
                    this.GetComponent<SpriteRenderer>().sprite = plusTh_sente;
                    break;
                case "Th_gote":
                    this.name = "plusTh_gote";
                    this.GetComponent<SpriteRenderer>().sprite = plusTh_gote;
                    break;
                case "M_sente":
                    this.name = "plusM_sente";
                    this.GetComponent<SpriteRenderer>().sprite = plusM_sente;
                    break;
                case "M_gote":
                    this.name = "plusM_gote";
                    this.GetComponent<SpriteRenderer>().sprite = plusM_gote;
                    break;
                case "B_sente":
                    this.name = "plusS_sente";
                    this.GetComponent<SpriteRenderer>().sprite = plusS_sente;
                    break;
                case "B_gote":
                    this.name = "plusS_gote";
                    this.GetComponent<SpriteRenderer>().sprite = plusS_gote;
                    break;
                case "Tg_sente":
                    this.name = "plusTg_sente";
                    this.GetComponent<SpriteRenderer>().sprite = plusTg_sente;
                    break;
                case "Tg_gote":
                    this.name = "plusTg_gote";
                    this.GetComponent<SpriteRenderer>().sprite = plusTg_gote;
                    break;
                case "X_sente":
                    this.name = "plusX_sente";
                    this.GetComponent<SpriteRenderer>().sprite = plusX_sente;
                    break;
                case "X_gote":
                    this.name = "plusX_gote";
                    this.GetComponent<SpriteRenderer>().sprite = plusX_gote;
                    break;
            }
        }
        else
        {
            // Giữ nguyên trạng thái
        }
    }
}