using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework;
using Unity.VisualScripting;  // Añadir la referencia correcta para Image

public class ObjectID_UI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header ("Player")]
    public T_Player player;
    [Header("Datos del objeto")]
    public int ID;
    public bool IsBlockInsideCanvas;
    public bool instruccionCompletada;
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private Image image;
    private Lienzo_UI lienzoUI;  // Referencia al lienzo para verificar la "entrada" del objeto
    [SerializeField] private List<Sprite> TipeBlockImage;
    private Vector2 originalPosition;
    [SerializeField] private DeleteArea_UI deleteArea;
    [SerializeField] private GameObject blockCopy;

    [Header("Extras")]
    [SerializeField] private bool firstMove = false;

    // Para detectar cambios en tipoBloque
    [SerializeField]private TipoBloque _tipoBloque;
    [SerializeField] private Transform parentTransform;

    public TipoBloque tipoBloque
    {
        get { return _tipoBloque; }
        set
        {
            if (_tipoBloque != value)
            {
                _tipoBloque = value;
            }
        }
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        lienzoUI = FindObjectOfType<Lienzo_UI>();  // Obtenemos referencia al Lienzo en la escena
        originalPosition = rectTransform.anchoredPosition;  // Guardamos la posición original
        deleteArea = FindObjectOfType<DeleteArea_UI>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        UpdateObjectInLienzo();
        UpdateTipeBlockSprite();
    }

    private void UpdateObjectInLienzo()
    {
        // Si el objeto está dentro del panel, agregarlo a la lista, si no, removerlo
        if (lienzoUI.IsObjectInsidePanel(gameObject))
        {
            if (!lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Add(gameObject);
                IsBlockInsideCanvas = true;
            }
        }
        else
        {
            if (lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Remove(gameObject);
                IsBlockInsideCanvas = false;
            }
        }
    }

    private void UpdateTipeBlockSprite(){
        switch (_tipoBloque){
            case TipoBloque.Avanzar:
                image.sprite = TipeBlockImage[0];
            break;
            case TipoBloque.AvanzarNum:
                image.sprite = TipeBlockImage[0];
            break;
            case TipoBloque.Saltar:
                image.sprite = TipeBlockImage[1];
            break;
            case TipoBloque.Disparar:
                image.sprite = TipeBlockImage[2];
            break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Cambiar el parent del objeto al canvas
        if (deleteArea.IsObjectInsidePanel(gameObject) && firstMove == false)
        {
            blockCopy = Instantiate(gameObject, parentTransform); // Crear una copia del objeto
            blockCopy.transform.SetParent(parentTransform.transform); // Añadirlo al canvas principal
            firstMove = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out movePos))
        {
            rectTransform.anchoredPosition = movePos;
        }

        deleteArea.gameObject.SetActive(deleteArea.IsObjectInsidePanel(gameObject));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (deleteArea.IsObjectInsidePanel(gameObject))
        {
            Destroy(gameObject);
            deleteArea.ClosePanel();
        }

        if (Lienzo_UI.Instance.IsObjectInsidePanel(gameObject))
        {
            // Si el objeto está dentro del panel, acomodar el objeto en medio del liezno (En medio en pos Y)
            AcomodarObjeto();
            Lienzo_UI.Instance.MostrarPreview();
        }
    }

    public void ResetInstruction()
    {
        instruccionCompletada = false;
    }


    public void Instruction()
    {
        switch (tipoBloque)
        {
            case TipoBloque.Saltar:
                if(T_Player.Instance.estadoAnterior == "Saltar"){
                    T_Player.Instance.PlayerRB.AddForce(transform.up * 5, ForceMode2D.Impulse);
                } else {
                    T_Player.Instance.PlayerRB.AddForce(transform.up * 5, ForceMode2D.Impulse);
                }
                
                //Player.Instance.AnimJump();
                T_Player.Instance.estado = EstadosJugador.Saltar;
                break;
              case TipoBloque.Agacharse:
                Debug.Log("Agachar");
                T_Player.Instance.estado = EstadosJugador.Idle;
                break;
            case TipoBloque.Avanzar:
                T_Player.Instance.estado = EstadosJugador.Avanzar;
                //Player.Instance.AnimRun();
                break;
            case TipoBloque.AvanzarNum:
                Debug.Log("AvanzarNum");
                T_Player.Instance.estado = EstadosJugador.Avanzar;
                break;
            case TipoBloque.Disparar:
                Debug.Log("Disparar");
                T_Player.Instance.estado = EstadosJugador.Disparar;
                T_Player.Instance.StartCoroutine(T_Player.Instance.Disparar(0.8f));
                break;
        }
    }

    public IEnumerator PlayInstruction()
    {
        yield return new WaitForSeconds(3f);
    }

    public void InstruccionCompleta()
    {
        instruccionCompletada = true;
        T_Player.Instance.ResetTriggers();
    }

    public void AcomodarObjeto()
    {
        //Set parent de el lienzo
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -185);
    }
}

public enum TipoBloque
{
    Avanzar,
    AvanzarNum,
    Saltar,
    Disparar,
    Agacharse
}


