using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    public List<DropSlot> Drop = new List<DropSlot>(); // Danh sách các ô trên bảng trưng bày
    public Transform dropContainer; // Khu vực hiển thị danh sách Drop
    public GameObject controller;

    [System.Serializable]
    public class DropSlot
    {
        public GameObject piecePrefab; // Prefab quân cờ (hiển thị trên bảng trưng bày)
        public int count; // Số lượng quân cờ bị bắt
        public GameObject display; // Đối tượng UI hiển thị quân cờ
    }

    // Hàm xử lý khi quân cờ bị tấn công
    public void HandleAttack(GameObject attackedPiece)
    {
        // Xóa quân cờ khỏi bàn cờ
        RemovePieceFromBoard(attackedPiece);

        // Tìm loại quân cờ trong danh sách Drop
        bool found = false;
        foreach (DropSlot slot in Drop)
        {
            if (slot.piecePrefab.name == attackedPiece.name)
            {
                slot.count++; // Cộng dần số lượng
                UpdateSlotDisplay(slot); // Cập nhật hiển thị
                found = true;
                break;
            }
        }

        // Nếu chưa có, thêm vào danh sách và hiển thị
        if (!found)
        {
            DropSlot newSlot = new DropSlot
            {
                piecePrefab = attackedPiece,
                count = 1,
                display = Instantiate(attackedPiece, dropContainer) // Tạo đối tượng hiển thị
            };

            // Chuyển quân cờ về bảng trưng bày
            newSlot.display.transform.localPosition = new Vector3(Drop.Count * 100, 0, 0); // Sắp xếp vị trí
            newSlot.display.transform.localScale = Vector3.one; // Đặt tỉ lệ hiển thị đúng
            newSlot.display.SetActive(true);

            Drop.Add(newSlot);
            UpdateSlotDisplay(newSlot); // Cập nhật hiển thị ban đầu
        }
    }

    // Cập nhật hiển thị trên bảng trưng bày
    private void UpdateSlotDisplay(DropSlot slot)
    {
        // Tìm UI Text để hiển thị số lượng quân cờ
        Text countText = slot.display.GetComponentInChildren<Text>();
        if (countText != null)
        {
            countText.text = slot.count.ToString(); // Cập nhật số lượng
        }

        // Đảm bảo vị trí hiển thị chính xác
        int index = Drop.IndexOf(slot);
        slot.display.transform.localPosition = new Vector3(index * 100, 0, 0); // Sắp xếp ngang
    }

    // Xóa quân cờ khỏi bàn cờ
    private void RemovePieceFromBoard(GameObject piece)
    {
        if (piece == null)
        {
            Debug.LogError("Piece is null, cannot remove.");
            return; // Trả về nếu piece là null
        }

        ShogiMan shogiMan = piece.GetComponent<ShogiMan>();
        if (shogiMan == null)
        {
            Debug.LogError("ShogiMan component not found on the piece.");
            return; // Trả về nếu không tìm thấy component ShogiMan
        }

        // Đảm bảo controller không phải là null
        if (controller == null)
        {
            Debug.LogError("Controller is null, cannot remove piece.");
            return;
        }

        Game gameController = controller.GetComponent<Game>();
        if (gameController == null)
        {
            Debug.LogError("Game controller not found.");
            return;
        }

        gameController.SetPositionEmpty(shogiMan.getXBoard(), shogiMan.getYBoard());
        piece.SetActive(false); // Ẩn quân cờ
    }

    // Tái sử dụng quân cờ từ bảng trưng bày
    public void ReturnPieceToBoard(DropSlot slot, int x, int y)
    {
        if (slot.count > 0)
        {
            // Lấy quân cờ từ danh sách Drop
            GameObject piece = Instantiate(slot.piecePrefab);
            piece.SetActive(true); // Hiển thị lại quân cờ

            // Đặt quân cờ vào vị trí trên bàn cờ
            piece.GetComponent<ShogiMan>().setXBoard(x);
            piece.GetComponent<ShogiMan>().setYBoard(y);
            piece.GetComponent<ShogiMan>().SetCoords();

            controller.GetComponent<Game>().SetPosition(piece);

            // Giảm số lượng quân cờ trong Drop
            slot.count--;
            UpdateSlotDisplay(slot); // Cập nhật hiển thị

            // Nếu số lượng bằng 0, ẩn quân cờ trên bảng trưng bày
            if (slot.count <= 0 && slot.display != null)
            {
                slot.display.SetActive(false);
            }
        }
    }

    // Xóa toàn bộ quân cờ trên bảng trưng bày
    public void ClearDropBoard()
    {
        foreach (DropSlot slot in Drop)
        {
            if (slot.display != null)
            {
                Destroy(slot.display); // Xóa đối tượng hiển thị
            }
        }
        Drop.Clear(); // Xóa danh sách
    }
}
