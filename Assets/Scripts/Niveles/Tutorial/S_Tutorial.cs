using UnityEngine;

public class S_Tutorial : MonoBehaviour
{
    public Player player;
    public GameObject FinishLevelPos;

    //singleton
    public static S_Tutorial Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lienzo_UI.Instance.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressBar.Instance.playerGO = player.gameObject;
        ProgressBar.Instance.finishGO = FinishLevelPos;
    }
}
