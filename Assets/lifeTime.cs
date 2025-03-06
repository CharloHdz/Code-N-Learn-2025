using UnityEngine;

public class lifeTime : MonoBehaviour
{
    [SerializeField] private float TiempoDeVida;
    private float tiempoActual;
    void OnEnable()
    {
        tiempoActual = TiempoDeVida;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
