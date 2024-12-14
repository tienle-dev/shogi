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

    //tha quan co
    public bool isDrop = false;

    public void Update()
    {

    }

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (isDrop)
        {
            // Nếu là ô để thả quân, tô màu xanh lam
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f, 0.5f);
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

                // Kiểm tra nếu quân bị ăn là Vua
                if (sp.name == "V_gote") controller.GetComponent<Game>().Winner("Sente");
                if (sp.name == "V_sente") controller.GetComponent<Game>().Winner("Gote");

                if(sp.name == "X_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusX_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "Tg_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusTg_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "B_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusS_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "M_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusM_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "Th_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusTh_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "T_sente") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusT_sente") controller.GetComponent<Game>().GetName(sp.name);

                if (sp.name == "X_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusX_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "Tg_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusTg_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "B_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusS_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "M_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusM_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "Th_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusTh_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "T_gote") controller.GetComponent<Game>().GetName(sp.name);
                if (sp.name == "plusT_gote") controller.GetComponent<Game>().GetName(sp.name);

                Destroy(sp); // Xóa quân bị ăn
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
