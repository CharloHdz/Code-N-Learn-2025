using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Nivel1");
    }

    public void N2(){
        
    }

    public void N3(){
        
    }

    public void N4(){
        
    }

    public void N5(){
        
    }

    public void Tutorial(){
        SceneManager.LoadScene("Tutorial");
    }
}
