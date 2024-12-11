using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlatePrefab;
    GameObject reference = null;

    public int matrixX;
    public int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    public void Update()
    {

    }

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            // Lấy quân cờ bị tấn công
            GameObject sp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (sp != null) // Nếu có quân cờ tại vị trí bị tấn công
            {
                // Chuyển quân cờ bị tấn công vào Drop (bảng trưng bày)
                GameObject dropManager = GameObject.FindGameObjectWithTag("Sente"); // DropManager quản lý Drop
                if (dropManager != null)
                {
                    dropManager.GetComponent<Move>().HandleAttack(sp); // Chuyển quân cờ bị bắt vào Drop
                }

                // Ẩn quân cờ bị tấn công (không xóa hoàn toàn)
                sp.SetActive(false);
            }
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<ShogiMan>().getXBoard(),
            reference.GetComponent<ShogiMan>().getYBoard());

        reference.GetComponent<ShogiMan>().setXBoard(matrixX);
        reference.GetComponent<ShogiMan>().setYBoard(matrixY);
        reference.GetComponent<ShogiMan>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<ShogiMan>().DestroyMovePlates();
        string currentPlayer = controller.GetComponent<Game>().GetCurrentPlayer();
        if (controller.GetComponent<Game>().CheckMate(currentPlayer, controller.GetComponent<Game>().positions))
        {
            string winner = currentPlayer == "Sente" ? "Gote" : "Sente";
            controller.GetComponent<Game>().Winner(winner); // Hiển thị chiến thắng
        }
        if (controller.GetComponent<Game>().FourfoldRepition(currentPlayer, controller.GetComponent<Game>().positions) || controller.GetComponent<Game>().Stalemated(currentPlayer, controller.GetComponent<Game>().positions))
        {
            string draw = currentPlayer == "Sente" ? "Gote" : "Sente";
            controller.GetComponent<Game>().Draw(draw);
        }
    }
    public void SetCoords(int x, int y)
    {
        matrixY = y;
        matrixX = x;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
    public GameObject GetReference()
    {
        return reference;
    }
}
