using Microsoft.Unity.VisualStudio.Editor;
using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogos", menuName = "ObjetosCHIDOS/Dialogos")]
public class Dialogos : ScriptableObject
{
    [Header("Propiedades Principales")]
    public int ID;
    public Sprite Fondo;
    public Vector2 sizeRectTransform;
    [TextArea(10, 10)]
    public string dialogo;
    public float MaxFontSize;
    public Vector2 Posicion;

    [Header("Propiedades Secundarias")]
    //Activa la animacion de iluminacion del bloque para mostrar que se puede arrastrar
    public bool AnimBloque;
    //Bool Mostrar Paso Siguiente
    public bool ShowNextStep;
}
