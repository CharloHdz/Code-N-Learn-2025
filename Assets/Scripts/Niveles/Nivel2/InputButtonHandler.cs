using UnityEngine;
using UnityEngine.EventSystems;

public class InputButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum Direction { UP, DOWN, LEFT, RIGHT, SPACE }
    public Direction direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (direction)
        {
            case Direction.UP:
                InputButtonController.instance.UP = true;
                break;
            case Direction.DOWN:
                InputButtonController.instance.DOWN = true;
                break;
            case Direction.LEFT:
                InputButtonController.instance.LEFT = true;
                break;
            case Direction.RIGHT:
                InputButtonController.instance.RIGHT = true;
                break;
            case Direction.SPACE:
                InputButtonController.instance.SPACE = true;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (direction)
        {
            case Direction.UP:
                InputButtonController.instance.UP = false;
                break;
            case Direction.DOWN:
                InputButtonController.instance.DOWN = false;
                break;
            case Direction.LEFT:
                InputButtonController.instance.LEFT = false;
                break;
            case Direction.RIGHT:
                InputButtonController.instance.RIGHT = false;
                break;
            case Direction.SPACE:
                InputButtonController.instance.SPACE = false;
                break;
        }
    }
}
