using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;

    private InputBlocks currentInputBlock;
    public enum TipoBlock {Avanzar, Disparar}
    public TipoBlock tipoBlock;
    public TMP_Dropdown DD_direccion;
    public N2_Player player;

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
            currentInputBlock = null; // Reset currentInputBlock to null while dragging
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Fin del arrastre: " + gameObject.name);
        currentInputBlock = null; // Reset currentInputBlock to null when dragging ends

        InputBlocks closestInput = FindClosestInputBlock();
        if (closestInput != null && closestInput.IsObjectInsidePanel(gameObject))
        {
            currentInputBlock = closestInput;
            currentInputBlock.Block = gameObject;
            currentInputBlock.Attach();
            currentInputBlock.blockScript = this;
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

    public void Instruccion(){
        switch (tipoBlock)
        {
            case TipoBlock.Avanzar:
                if (DD_direccion.value == 0)
                {
                    //mover al player hacia arriba
                    Debug.Log("Dirección: Arriba");
                    player.transform.position += new Vector3(0, player.speed * Time.deltaTime, 0);
                }
                else if (DD_direccion.value == 1)
                {
                    Debug.Log("Dirección: Abajo");
                    //mover al player hacia abajo
                    player.transform.position += new Vector3(0, -player.speed * Time.deltaTime, 0);
                }
                else if (DD_direccion.value == 2)
                {
                    Debug.Log("Dirección: Izquierda");
                    //mover al player hacia la izquierda
                    player.transform.position += new Vector3(-player.speed * Time.deltaTime, 0, 0);
                }
                else if (DD_direccion.value == 3)
                {
                    Debug.Log("Dirección: Derecha");
                    //mover al player hacia la derecha
                    player.transform.position += new Vector3(player.speed * Time.deltaTime, 0, 0);
                }

                break;
            case TipoBlock.Disparar:
                Debug.Log("Instrucción: Disparar");
                break;
            default:
                Debug.Log("Instrucción no válida");
                break;
        }
    }
}
