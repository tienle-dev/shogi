using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDrop : MonoBehaviour
{
    public GameObject ShogiPiece;
    public Button X_sente; // Nút để tạo quân cờ
    public Button Tg_sente; // Nút để tạo quân cờ
    public Button Th_sente; // Nút để tạo quân cờ
    public Button M_sente; // Nút để tạo quân cờ
    public Button B_sente; // Nút để tạo quân cờ
    public Button Vg_sente; // Nút để tạo quân cờ
    public Button T_sente; // Nút để tạo quân cờ

    public Button X_gote; // Nút để tạo quân cờ
    public Button Tg_gote; // Nút để tạo quân cờ
    public Button Th_gote; // Nút để tạo quân cờ
    public Button M_gote; // Nút để tạo quân cờ
    public Button B_gote; // Nút để tạo quân cờ
    public Button Vg_gote; // Nút để tạo quân cờ
    public Button T_gote; // Nút để tạo quân cờ


    public GameObject[,] positions = new GameObject[9, 9];

    void Start()
    {
        if (X_sente != null) X_sente.onClick.AddListener(OnCreatePieceButtonClicked_X_s);
        if (Tg_sente != null) Tg_sente.onClick.AddListener(OnCreatePieceButtonClicked_Tg_s);
        if (Th_sente != null) Th_sente.onClick.AddListener(OnCreatePieceButtonClicked_Th_s);
    }

    // Hàm được gọi khi bấm nút
    public void OnCreatePieceButtonClicked_X_s()
    {
        int x = 4; // Tạo vị trí ngẫu nhiên cho ví dụ
        int y = 4;


        if (positions[x, y] == null) // Kiểm tra nếu vị trí trống
        {
            CreatePiece("X_sente", x, y);
        }
        else
        {
            Debug.Log("Vị trí này đã có quân cờ!");
        }
    }

    public void OnCreatePieceButtonClicked_Tg_s()
    {
        int x = 3; // Tạo vị trí ngẫu nhiên cho ví dụ
        int y = 4;

        if (positions[x, y] == null) // Kiểm tra nếu vị trí trống
        {
            CreatePiece("Tg_sente", x, y);
        }
        else
        {
            Debug.Log("Vị trí này đã có quân cờ!");
        }
    }

    public void OnCreatePieceButtonClicked_Th_s()
    {
        int x = 3; // Tạo vị trí ngẫu nhiên cho ví dụ
        int y = 4;

        if (positions[x, y] == null) // Kiểm tra nếu vị trí trống
        {
            CreatePiece("Tg_sente", x, y);
        }
        else
        {
            Debug.Log("Vị trí này đã có quân cờ!");
        }
    }

    // Hàm tạo quân cờ
    public GameObject CreatePiece(string name, int x, int y)
    {
        GameObject obj = Instantiate(ShogiPiece, new Vector3(x, y, -1), Quaternion.identity);
        ShogiMan sm = obj.GetComponent<ShogiMan>();
        sm.name = name;
        sm.setXBoard(x);
        sm.setYBoard(y);
        sm.Activate();
        positions[x, y] = obj; // Cập nhật vị trí trên bàn cờ
        return obj;
    }
}

