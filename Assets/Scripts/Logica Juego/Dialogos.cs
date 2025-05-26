using UnityEngine;


[CreateAssetMenu(fileName = "Dialogos", menuName = "ObjetosCHIDOS/Dialogos")]
public class Dialogos : ScriptableObject
{
    [Header("Propiedades Principales")]
    public int ID;
    public enum TipoDialogo
    {
        FondoCortoBoton,
        FondoCorto,
        FondoLargoBoton,
        FondoLargo,
        SinFondo
    }
    public TipoDialogo tipoDialogo;
    [TextArea(10, 10)]
    public string dialogo;
    public Vector2 Posicion;

    [Header("Propiedades Secundarias")]
    //Activa la animacion de iluminacion del bloque para mostrar que se puede arrastrar
    public bool ShowNextStep;
}
