using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputBlocks : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;

    [Header("Objetos UI")]
    public Lienzo lienzo;
    public AnchorIB anchorIB;

    public GameObject Block;
    public Block blockScript;
    public bool Blockegange;

    [Header ("Valores")]
    public TMP_InputField inputField;
    public string InputKey;
    public TMP_Dropdown dropdown;
    public bool Engaged;
    public DeleteArea_UI deleteArea;

    [Header("Input Buttons0")]
    public InputButtonHandler inputButtonHandler;
    void OnEnable()
    {
        lienzo = FindObjectOfType<Lienzo>();
        canvas = FindAnyObjectByType<Canvas>();
        deleteArea = FindObjectOfType<DeleteArea_UI>();
        blockScript = Block.GetComponent<Block>();
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

            if (Engaged){
            // Dentro del Update()
                switch (dropdown.value){
                    case 0:
                        if (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0 || InputButtonController.instance.UP){
                            print("W Presionado");
                            blockScript.Instruccion();
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.A) || Input.GetAxis("Horizontal") < 0 || InputButtonController.instance.LEFT){
                            print("A Presionado");
                            blockScript.Instruccion();
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < 0 || InputButtonController.instance.DOWN){
                            print("S Presionado");
                            blockScript.Instruccion();
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.D) || Input.GetAxis("Horizontal") > 0 || InputButtonController.instance.RIGHT){
                            print("D Presionado");
                            blockScript.Instruccion();
                        }
                        break;
                }

            }
        }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Código para manejar el inicio del arrastre
        Debug.Log("Inicio del arrastre: " + gameObject.name);
        Engaged = false;
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

        deleteArea.deleteArea.SetActive(deleteArea.IsObjectInsidePanel(gameObject));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (deleteArea.IsObjectInsidePanel(gameObject))
        {
            Destroy(gameObject);
            deleteArea.ClosePanel();
        }
        // Código para manejar el final del arrastre
        Debug.Log("Fin del arrastre: " + gameObject.name);
        Engaged = false;

        if (lienzo.IsObjectInsidePanel(gameObject))
        {
            // Si el objeto está dentro del panel, acomodar el objeto en medio del lienzo (En medio en pos Y)
            transform.position = new Vector3(lienzo.rectTransform.position.x, transform.position.y, 0);
            Engaged = true;
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


    // Input Buttons
    
}
