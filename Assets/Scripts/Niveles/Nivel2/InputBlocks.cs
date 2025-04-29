using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputBlocks : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;

    [Header("Objetos UI")]
    public Lienzo lienzo;
    public AnchorIB anchorIB;

    public GameObject Block;
    public bool Blockegange;
    void OnEnable()
    {
        lienzo = FindObjectOfType<Lienzo>();
        canvas = GetComponentInParent<Canvas>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Acomodar bloques en el centro cuando está dentro del lienzo
        if (Blockegange)
        {
            Block.transform.position = new Vector2(anchorIB.rectTransform.position.x, anchorIB.rectTransform.position.y);
        }
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

        if (lienzo.IsObjectInsidePanel(gameObject))
        {
            // Si el objeto está dentro del panel, acomodar el objeto en medio del lienzo (En medio en pos Y)
            transform.position = new Vector3(lienzo.rectTransform.position.x, transform.position.y, 0);
        }
    }

    public bool IsObjectInsidePanel(GameObject obj)
    {
        RectTransform objRectTransform = obj.GetComponent<RectTransform>();
        if (objRectTransform == null) return false;

        // Verificar si el punto está dentro del RectTransform del panel
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, objRectTransform.position, canvas.worldCamera);
    }

    public void Attach(){
        Block.transform.position = new Vector2(anchorIB.rectTransform.position.x, anchorIB.rectTransform.position.y);
        Blockegange = true;
    }

    public void Detach(){
        Blockegange = false;
    }
}
