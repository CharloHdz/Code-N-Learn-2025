using UnityEngine;

public class Lienzo : MonoBehaviour
{
    public RectTransform rectTransform;  // Referencia al área del lienzo (Panel)
    public Canvas canvas;  // Referencia al Canvas, necesario para calcular las posiciones en pantalla
    public Animator anim;
    public bool Opened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsObjectInsidePanel(GameObject obj)
    {
        RectTransform objRectTransform = obj.GetComponent<RectTransform>();
        if (objRectTransform == null) return false;

        // Verificar si el punto está dentro del RectTransform del panel
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, objRectTransform.position, canvas.worldCamera);
    }

    public void HideShow(){
        if (Opened)
        {
            anim.SetInteger("Open", 0);
            Opened = false;
        }
        else
        {
            anim.SetInteger("Open", 1);
            Opened = true;
        }
    }
}
