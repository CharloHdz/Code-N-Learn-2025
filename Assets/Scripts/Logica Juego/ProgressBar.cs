using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar Instance { get; private set; }
    public GameObject playerGO;
    public GameObject finishGO;

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

            switch (GameManager.instance.nivelNum){
                case GameManager.Niveles.Tutorial:
                    finishGO = S_Tutorial.Instance.FinishLevelPos;
                break;
            }
    }
}
