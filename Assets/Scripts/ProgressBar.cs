using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] GameObject finishGO;

    Image progressBar;
    float maxDistance;

    void Start()
    {
        progressBar = GetComponent<Image>();
        maxDistance = finishGO.transform.position.x;

        progressBar.fillAmount = playerGO.transform.position.x / maxDistance;
    }

    void Update()
    {
            progressBar.fillAmount = playerGO.transform.position.x / maxDistance;
    }
}
