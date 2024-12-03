using UnityEngine;

public class PromotionManager : MonoBehaviour
{
    public GameObject promotionPanel; // Panel chứa các nút chọn phong cấp
    public GameObject promotedSprite; // Sprite sau khi phong cấp
    public GameObject normalSprite;   // Sprite giữ nguyên

    private ShogiMan currentPiece;

    public void OpenPromotionPanel(ShogiMan piece)
    {
        currentPiece = piece; // Lưu quân cờ hiện tại đang cần xử lý
        promotionPanel.SetActive(true);
    }
    public void ChoosePromoted()
    {
        currentPiece.Promote(true);
        promotionPanel.SetActive(false);
    }
    public void ChooseNormal()
    {
        currentPiece.Promote(false);
        promotionPanel.SetActive(false);
    }
}