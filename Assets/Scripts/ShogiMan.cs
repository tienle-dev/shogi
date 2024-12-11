using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShogiMan : MonoBehaviour
{
    //ref
    public GameObject controller;
    public GameObject movePlate;
    public GameObject[,] board = new GameObject[9, 9];

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
            DisplayMove(this);
        }
    }
    public List<(int, int)> Pawn1MovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        moves.AddRange(PointMovePlate(xBoard, yBoard + 1));
        return moves;
    }
    public List<(int, int)> Pawn2MovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        moves.AddRange(PointMovePlate(xBoard, yBoard - 1));
        return moves;
    }
    public List<(int, int)> PointMovePlate(int x, int y)
    {
        List<(int, int)> moves = new List<(int, int)>();
        // Kiểm tra vị trí nằm trong giới hạn bàn cờ
        if (x < 0 || x >= 9 || y < 0 || y >= 9)
        {
            Debug.LogWarning("Attempted to access position out of bounds: (" + x + ", " + y + ")");
            return moves;
        }

        Game sc = controller.GetComponent<Game>();
        GameObject sp = sc.GetPosition(x, y);

        // Nếu vị trí trống
        if (sp == null)
        {
            // Kiểm tra xem nước đi có hợp lệ không
            if (sc.AllowedMove(sc.positions, (xBoard, yBoard), (x, y), sc.GetCurrentPlayer()))
            {
                Debug.Log("Position is empty at: (" + x + ", " + y + ")");
                moves.Add((x, y));
            }
        }
        else
        {
            // Lấy component ShogiMan từ GameObject tại vị trí
            ShogiMan shogiMan = sp.GetComponent<ShogiMan>();

            // Nếu đối tượng không phải là ShogiMan
            if (shogiMan == null)
            {
                Debug.LogError("The object at position (" + x + ", " + y + ") does not have a 'ShogiMan' component.");
                return moves;
            }
            if (shogiMan.player != player && sc.AllowedMove(sc.positions, (xBoard, yBoard), (x, y), sc.GetCurrentPlayer()))
            {
                moves.Add((x, y));
            }
        }
        return moves;
    }
    public void DisplayMove(ShogiMan shogiMan)
    {
        List<(int, int)> validMoves = shogiMan.InitiateMovePlates();

        foreach ((int newX, int newY) in validMoves)
        {
            Game sc = controller.GetComponent<Game>();
            GameObject sp = sc.GetPosition(newX, newY);
            if (sp == null)
            {
                MovePlateSpawn(newX, newY);
            }
            else
            {
                ShogiMan sm = sp.GetComponent<ShogiMan>();
                if (sm != null && sm.player != shogiMan.player)
                {
                    MovePlateAttackSpawn(newX, newY);
                }
            }
        }
    }
    private ShogiMan FindKing(string player, GameObject[,] board)
    {
        foreach (GameObject piece in board)
        {
            if (piece != null)
            {
                ShogiMan shogiMan = piece.GetComponent<ShogiMan>();
                if (shogiMan != null && shogiMan.player == player && (shogiMan.name == "V_gote" || shogiMan.name == "V_sente"))
                {
                    return shogiMan;
                }
            }
        }
        return null;
    }
    public void CheckedKing()
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.CheckBoard(sc.positions, player))
        {
            ShogiMan king = FindKing(player, sc.positions);
            if (king != null)
            {
                MovePlateCheckSpawn(king.xBoard, king.yBoard);
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

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
    public void MovePlateCheckSpawn(int matrixX, int matrixY)
    {
        float cellSize = 0.7f * 1.121f;
        float x = matrixX * cellSize;
        float y = matrixY * cellSize;

        float xOffset = -3.166f;
        float yOffset = -3.06f;

        x += xOffset;
        y += yOffset;

        // Tạo MovePlate tại vị trí cần spawn
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        // Cấu hình MovePlate
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject); // Set quân cờ gốc
        mpScript.SetCoords(matrixX, matrixY);

        // Kiểm tra xem MovePlate có trùng vị trí với vua không
        GameObject[,] board = controller.GetComponent<Game>().positions; // Lấy bảng cờ hiện tại
        string currentPlayer = gameObject.GetComponent<ShogiMan>().player; // Người chơi hiện tại

        ShogiMan king = FindKing(currentPlayer, board); // Tìm quân vua
        if (king != null && king.getXBoard() == matrixX && king.getYBoard() == matrixY)
        {
            SpriteRenderer renderer = mp.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            Collider2D collider = mp.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
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
        CheckedKing();
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
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }
    public List<(int, int)> InitiateMovePlates()
    {
        List<(int, int)> validMoves = new List<(int, int)>();
        switch (this.name)
        {
            case "V_gote":
            case "V_sente":
                validMoves.AddRange(SurroundMovePlate());
                break;
            case "X_gote":
            case "X_sente":
                validMoves.AddRange(LineMovePlate(1, 0));
                validMoves.AddRange(LineMovePlate(0, 1));
                validMoves.AddRange(LineMovePlate(-1, 0));
                validMoves.AddRange(LineMovePlate(0, -1));
                break;
            case "Tg_gote":
            case "Tg_sente":
                validMoves.AddRange(LineMovePlate(1, 1));
                validMoves.AddRange(LineMovePlate(-1, 1));
                validMoves.AddRange(LineMovePlate(1, -1));
                validMoves.AddRange(LineMovePlate(-1, -1));
                break;
            case "plusTg_gote":
            case "plusTg_sente":
                validMoves.AddRange(BishopPlusMovePlate());
                break;
            case "plusX_gote":
            case "plusX_sente":
                validMoves.AddRange(RookPlusMovePlate()); // Quân xe phong cấp
                break;
            case "Vg_gote":
            case "Vg_sente":
            case "plusT_gote":
            case "plusT_sente":
            case "plusS_sente":
            case "plusS_gote":
            case "plusM_gote":
            case "plusM_sente":
            case "plusTh_gote":
            case "plusTh_sente":
                validMoves.AddRange(GoldMovePlate());
                break;
            case "B_gote":
            case "B_sente":
                validMoves.AddRange(SilverMovePlate());
                break;
            case "M_gote":
            case "M_sente":
                validMoves.AddRange(LMovePlate());
                break;
            case "Th_gote":
            case "Th_sente":
                validMoves.AddRange(LineMovePlate(0, 1));
                validMoves.AddRange(LineMovePlate(0, -1));
                break;
            case "T_gote":
                validMoves.AddRange(Pawn2MovePlate());
                break;
            case "T_sente":
                validMoves.AddRange(Pawn1MovePlate());
                break;
        }
        return validMoves;
    }
    public List<(int, int)> LineMovePlate(int xIncrement, int yIncrement)
    {
        List<(int, int)> moves = new List<(int, int)>();
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y))
        {
            GameObject targetPiece = sc.GetPosition(x, y);

            // Nếu ô trống
            if (targetPiece == null)
            {
                // Chỉ spawn MovePlate nếu nước đi hợp lệ
                if (sc.AllowedMove(sc.positions, (xBoard, yBoard), (x, y), player))
                {
                    Debug.Log("Position is empty at: (" + x + ", " + y + ")");
                    moves.Add((x, y));
                }
            }
            // Nếu gặp một quân cờ
            else
            {
                ShogiMan shogiMan = targetPiece.GetComponent<ShogiMan>();

                // Nếu là quân của đối thủ
                if (shogiMan != null && shogiMan.player != player)
                {
                    // Chỉ spawn MovePlateAttack nếu nước đi hợp lệ
                    if (sc.AllowedMove(sc.positions, (xBoard, yBoard), (x, y), player))
                    {
                        moves.Add((x, y));
                    }
                }
                // Dừng vòng lặp nếu gặp bất kỳ quân cờ nào (đồng minh hoặc đối thủ)
                break;
            }

            // Tiếp tục di chuyển trên đường đi
            x += xIncrement;
            y += yIncrement;
        }
        return moves;
    }
    public List<(int, int)> BishopPlusMovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        moves.AddRange(LineMovePlate(1, 1));
        moves.AddRange(LineMovePlate(-1, 1));
        moves.AddRange(LineMovePlate(1, -1));
        moves.AddRange(LineMovePlate(-1, -1));
        moves.AddRange(PointMovePlate(xBoard + 1, yBoard));
        moves.AddRange(PointMovePlate(xBoard - 1, yBoard));
        moves.AddRange(PointMovePlate(xBoard, yBoard + 1));
        moves.AddRange(PointMovePlate(xBoard, yBoard - 1));
        return moves;
    }
    public List<(int, int)> RookPlusMovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        // Nước đi của xe
        moves.AddRange(LineMovePlate(1, 0));  // Đi ngang phải
        moves.AddRange(LineMovePlate(-1, 0)); // Đi ngang trái
        moves.AddRange(LineMovePlate(0, 1));  // Đi dọc lên
        moves.AddRange(LineMovePlate(0, -1)); // Đi dọc xuống

        // Kết hợp thêm nước đi của vua
        moves.AddRange(PointMovePlate(xBoard + 1, yBoard + 1)); // Chéo trên phải
        moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 1)); // Chéo dưới phải
        moves.AddRange(PointMovePlate(xBoard - 1, yBoard + 1)); // Chéo trên trái
        moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 1)); // Chéo dưới trái
        return moves;
    }
    public List<(int, int)> SilverMovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        if (player == "Sente")
        {
            moves.AddRange(PointMovePlate(xBoard, yBoard + 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard + 1));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard + 1));
        }
        else
        {
            moves.AddRange(PointMovePlate(xBoard, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard + 1));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard + 1));
        }
        return moves;
    }
    public List<(int, int)> GoldMovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        if (player == "Sente")
        {
            moves.AddRange(PointMovePlate(xBoard, yBoard + 1));
            moves.AddRange(PointMovePlate(xBoard, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 0));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard + 1));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 0));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard + 1));
        }
        else
        {
            moves.AddRange(PointMovePlate(xBoard, yBoard + 1));
            moves.AddRange(PointMovePlate(xBoard, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 0));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 1));
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 0));
        }
        return moves;
    }
    public List<(int, int)> LMovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        if (player == "Sente")
        {
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard + 2));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard + 2));
        }
        if (player == "Gote")
        {
            moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 2));
            moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 2));
        }
        return moves;
    }
    public List<(int, int)> SurroundMovePlate()
    {
        List<(int, int)> moves = new List<(int, int)>();
        moves.AddRange(PointMovePlate(xBoard, yBoard + 1));
        moves.AddRange(PointMovePlate(xBoard, yBoard - 1));
        moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 1));
        moves.AddRange(PointMovePlate(xBoard - 1, yBoard - 0));
        moves.AddRange(PointMovePlate(xBoard - 1, yBoard + 1));
        moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 1));
        moves.AddRange(PointMovePlate(xBoard + 1, yBoard - 0));
        moves.AddRange(PointMovePlate(xBoard + 1, yBoard + 1));
        return moves;
    }
    
}