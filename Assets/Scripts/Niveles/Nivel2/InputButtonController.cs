using UnityEngine;

public class InputButtonController : MonoBehaviour
{
    public static InputButtonController instance;

    public bool UP;
    public bool DOWN;
    public bool LEFT;
    public bool RIGHT;
    public bool SPACE;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
