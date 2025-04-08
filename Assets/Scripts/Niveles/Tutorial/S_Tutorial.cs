using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Tutorial : MonoBehaviour
{
    public Player player;

    [Header ("Elementos Tutorial")]
    public GameObject[] TutorialElements;
    public int MetaTutorial;
    public Transform InicioNivel2;
    [SerializeField] private float maxDistanceTutorial;
    //singleton
    public static S_Tutorial Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lienzo_UI.Instance.player = player;

        //Inicializar los previews
        Lienzo_UI.Instance.InicializarPreviews(Lienzo_UI.Instance.PreviewWalk);
        Lienzo_UI.Instance.InicializarPreviews(Lienzo_UI.Instance.PreviewJump);
        Lienzo_UI.Instance.InicializarPreviews(Lienzo_UI.Instance.PreviewShoot);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MetaAlcanzadaTutorial(){
        MetaTutorial++;
        switch (MetaTutorial)
        {
            case 1:
                Player.Instance.SpawnPoint.transform.position = InicioNivel2.position;
                Player.Instance.Parar();
                StartCoroutine(Lienzo_UI.Instance.EliminarBloquesEnLienzo(0.5f));
                maxDistanceTutorial = InicioNivel2.position.x;
                break;
            case 2:
                print("Tutorial Superado");
                GameManager.instance.TutorialSuperado = true;
                SceneManager.LoadScene("Game");
                break;
        }
    }

    #region Botones
    public void OmitirTutorial()
    {
        GameManager.instance.TutorialSuperado = true;
        SceneManager.LoadScene("Nivel1");
    }
    #endregion
}
