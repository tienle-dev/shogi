using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    public int matrixX;
    public int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

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
            GameObject sp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            if(sp.name == "V_gote") controller.GetComponent<Game>().Winner("Sente");
            if (sp.name == "V_sente") controller.GetComponent<Game>().Winner("Gote");
            Destroy(sp);

        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<ShogiMan>().getXBoard(),
            reference.GetComponent<ShogiMan>().getYBoard());

        reference.GetComponent<ShogiMan>().setXBoard(matrixX);
        reference.GetComponent<ShogiMan>().setYBoard(matrixY);
        reference.GetComponent<ShogiMan>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<ShogiMan>().DestroyMovePlates();
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
