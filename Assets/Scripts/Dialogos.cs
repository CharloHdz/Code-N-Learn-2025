using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogos", menuName = "ObjetosCHIDOS/Dialogos")]
public class Dialogos : ScriptableObject
{
    [TextArea(10, 10)]
    public string dialogo;

    public Vector2 Posicion;
    public bool SetAnim;
}
