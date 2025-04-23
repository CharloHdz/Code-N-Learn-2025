using UnityEngine;
using UnityEngine.EventSystems;

public class InputBlocks : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Código para manejar el inicio del arrastre
        Debug.Log("Inicio del arrastre: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Código para manejar el arrastre
        Debug.Log("Arrastrando: " + gameObject.name);
        Vector2 movePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out movePos))
        {
            rectTransform.anchoredPosition = movePos;
        } // Ajustar la posición del objeto arrastrado
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Código para manejar el final del arrastre
        Debug.Log("Fin del arrastre: " + gameObject.name);
    }
}
