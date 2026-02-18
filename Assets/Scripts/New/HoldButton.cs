using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum Direction { Left, Right }
    public Direction direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (direction == Direction.Left)
            Player.Instance.OnLeftButtonDown();
        else
            Player.Instance.OnRightButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (direction == Direction.Left)
            Player.Instance.OnLeftButtonUp();
        else
            Player.Instance.OnRightButtonUp();
    }
}