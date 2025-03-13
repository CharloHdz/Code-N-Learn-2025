using UnityEngine;

public class Selector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            print("N");
            Panel();
        }
    }

    void Panel(){
        bool estado = gameObject.activeSelf;
        estado = !estado;
        gameObject.SetActive(estado);
    }

    public void N1(){
        GameManager.instance.CambiarNivel(Niveles.N1);
    }

    public void N2(){
        GameManager.instance.CambiarNivel(Niveles.N2);
    }

    public void N3(){
        GameManager.instance.CambiarNivel(Niveles.N3);
    }

    public void N4(){
        GameManager.instance.CambiarNivel(Niveles.N4);
    }

    public void N5(){
        GameManager.instance.CambiarNivel(Niveles.N5);
    }

    public void Tutorial(){
        GameManager.instance.CambiarNivel(Niveles.Tutorial);
    }
}
