using UnityEngine;

public class SensorPreview : MonoBehaviour
{
    private PreviewScript previewScript;
    void Start()
    {
        previewScript = GetComponentInParent<PreviewScript>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            previewScript.NoDetecta();
            print("No Detecta");
        }
    }
}
