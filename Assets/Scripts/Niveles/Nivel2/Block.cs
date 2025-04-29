using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;

    private InputBlocks currentInputBlock;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Inicio del arrastre: " + gameObject.name);
        if (currentInputBlock != null)
        {
            currentInputBlock.Detach();
            currentInputBlock.Block = null;
            currentInputBlock = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Arrastrando: " + gameObject.name);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 movePos))
        {
            rectTransform.anchoredPosition = movePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Fin del arrastre: " + gameObject.name);

        InputBlocks closestInput = FindClosestInputBlock();
        if (closestInput != null && closestInput.IsObjectInsidePanel(gameObject))
        {
            currentInputBlock = closestInput;
            currentInputBlock.Block = gameObject;
            currentInputBlock.Attach();
        }
    }

    private InputBlocks FindClosestInputBlock()
    {
        InputBlocks[] inputs = GameObject.FindObjectsOfType<InputBlocks>();
        float closestDistance = float.MaxValue;
        InputBlocks closest = null;

        foreach (var input in inputs)
        {
            float dist = Vector2.Distance(input.rectTransform.position, rectTransform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = input;
            }
        }

        return closest;
    }
}
