using UnityEngine;

public class PreviewScript : MonoBehaviour
{
    [SerializeField] private SensorPreview sensorPreview;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensorPreview = GetComponentInChildren<SensorPreview>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            transform.position = new Vector3(sensorPreview.transform.position.x, transform.position.y +1, sensorPreview.transform.position.z);
            print("Detecta");
        }
    }

    //Sensor
    public void Detecta(){

    }

    public void NoDetecta(){
        transform.position = new Vector3(sensorPreview.transform.position.x, transform.position.y -1, sensorPreview.transform.position.z);
    }
}
