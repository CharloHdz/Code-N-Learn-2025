using System;
using UnityEngine;

public class ImagenesEscena : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando se precione la tecla "S" tomara la captura de pantalla
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Captura la pantalla y la guarda en la carpeta "Capturas" dentro de "Assets" con un nombre diferente
            ScreenCapture.CaptureScreenshot("Assets/Screenshots/" + Time.time + ".png");
            Debug.Log("Captura de pantalla guardada en la carpeta 'Capturas' dentro de 'Assets'.");
        }
    }
}
