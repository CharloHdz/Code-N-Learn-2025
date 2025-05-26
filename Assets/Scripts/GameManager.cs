using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    [Header("Referencias UI")]
    private Lienzo_UI lienzoUI;

    [Header("UI Panels - Estados de Juego")]
    public GameObject[] GamePanels;
    public EstadosJuego estadoJuego;
    public string EstadoAnterior;
    public Canvas CanvasActual;


    [Header("Variables Generales")]
    public bool TutorialSuperado = true;
    public Idiomas IdiomaActual;
    public Resoluciones ResolucionActual;
    [Header ("Niveles")]
    public Niveles nivelNum;
    public enum Niveles
    {
        Tutorial,
        N1,
        N2,
        N3,
        N4,
        N5
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CambiarEstado(EstadosJuego.Menu);
    }

    private void Update()
    {

        //Pausa del Juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pausar en juego
            if (estadoJuego == EstadosJuego.PlayGame || estadoJuego == EstadosJuego.ResumeGame)
            {
                CambiarEstado(EstadosJuego.Pause);
            }
            else if (estadoJuego == EstadosJuego.Pause)
            {
                CambiarEstado(EstadosJuego.ResumeGame);
            }

            //Pausar en tutorial
            if (estadoJuego == EstadosJuego.PlayTutorial || estadoJuego == EstadosJuego.ResumeTutorial)
            {
                CambiarEstado(EstadosJuego.Pause);
            }
            else if (estadoJuego == EstadosJuego.Pause && EstadoAnterior == "PlayTutorial"
            || estadoJuego == EstadosJuego.Pause && EstadoAnterior == "ResumeTutorial")
            {
                CambiarEstado(EstadosJuego.ResumeTutorial);
            }
        }

        #region Comandos
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (estadoJuego == EstadosJuego.PlayGame || estadoJuego == EstadosJuego.ResumeGame
            || estadoJuego == EstadosJuego.PlayTutorial || estadoJuego == EstadosJuego.ResumeTutorial)
            {
                //Si esta abierto el panel de niveles, cerrarlo y viceversa
                if (GamePanels[7].activeSelf)
                {
                    GamePanels[7].SetActive(false);
                }
                else
                {
                    GamePanels[7].SetActive(true);
                }
            }
        }
        #endregion

        //Al cambiar escena, buscar el canvas de la escena
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            CanvasActual = GameObject.Find("CanvasNivel").GetComponent<Canvas>();
        }
        else if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            CanvasActual = GameObject.Find("CanvasNivel").GetComponent<Canvas>();
        }
        else if (SceneManager.GetActiveScene().name == "NIvel2")
        {
            CanvasActual = GameObject.Find("CanvasNivel").GetComponent<Canvas>();
        }
        else if (SceneManager.GetActiveScene().name == "Nivel3")
        {
            CanvasActual = GameObject.Find("CanvasNivel").GetComponent<Canvas>();
        }
    }

#region Botones de Juego
    //Acciones de Botones
    public void B_Jugar(){
        if(!TutorialSuperado){
            CambiarEstado(EstadosJuego.PlayTutorial);
        }else{
            SceneManager.LoadScene("Tutorial");
            CambiarEstado(EstadosJuego.PlayGame);
        }
    }
    public void B_Pause()
    {
        if (estadoJuego == EstadosJuego.PlayGame || estadoJuego == EstadosJuego.ResumeGame)
        {
            CambiarEstado(EstadosJuego.Pause);
        }
        else if (estadoJuego == EstadosJuego.Pause)
        {
            CambiarEstado(EstadosJuego.ResumeGame);
        }
    }
    public void B_Continuar()
    {
        if (EstadoAnterior == "PlayTutorial" || EstadoAnterior == "ResumeTutorial")
        {
            CambiarEstado(EstadosJuego.ResumeTutorial);
        }
        else if (EstadoAnterior == "PlayGame" || EstadoAnterior == "ResumeGame")
        {
            CambiarEstado(EstadosJuego.ResumeGame);
        }
    }
    public void B_Config(){
        CambiarEstado(EstadosJuego.Config);
    }
    public void B_Niveles()
    {
        if (SceneManager.GetActiveScene().name == "NIvel2")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("NIvel2");
        }
        CambiarEstado(EstadosJuego.PlayGame);
    }
    public void B_RegresarMenu()
    {
        SceneManager.LoadScene("Menu");
        CambiarEstado(EstadosJuego.Menu);
    }
    public void B_Creditos(){
        CambiarEstado(EstadosJuego.Creditos);
    }

    public void B_CambiarIdioma(){
        if(IdiomaActual == Idiomas.Español){
            IdiomaActual = Idiomas.English;
        }else{
            IdiomaActual = Idiomas.Español;
        }
    }

    public void B_OmitirTutorial(){
        TutorialSuperado = true;
        CambiarEstado(EstadosJuego.PlayGame);
        SceneManager.LoadScene("Nivel1");
    }
#endregion

    /*NOTA: Número de lista de Paneles de Juego
    [0]: Menu Panel
    [1]: Tutorial Panel
    [2]: Game Panel
    [3]: Pause Panel
    [4]: Config Panel
    [5]: Game Over Panel
    [6]: Creditos Panel
    [7]: Selector de Niveles
    */

    //Cambiar de estado de juego y guardar el estado anterior
    public void CambiarEstado(EstadosJuego nuevoEstado)
    {
        GuardarEstadoJuego();
        estadoJuego = nuevoEstado;
        cerrarTodo();
        switch (estadoJuego)
        {
            case EstadosJuego.Menu:
                GamePanels[0].SetActive(true);
                break;
            case EstadosJuego.PlayTutorial:
                //Cambiar escena a la del tutorial
                SceneManager.LoadScene("Tutorial");
                CanvasActual.enabled = true;
                break;
            case EstadosJuego.ResumeTutorial:
                CanvasActual.enabled = true;
                break;
            case EstadosJuego.PlayGame:
                CanvasActual.enabled = true;
                break;
            case EstadosJuego.Pause:
                GamePanels[1].SetActive(true);
                CanvasActual.enabled = false;
                break;
            case EstadosJuego.ResumeGame:
                Lienzo_UI.Instance.RestartPreviewPos();
                CanvasActual.enabled = true;
                break;
            case EstadosJuego.Config:
                GamePanels[2].SetActive(true);
                break;
            case EstadosJuego.GameOver:
                GamePanels[3].SetActive(true);
                break;
            case EstadosJuego.Creditos:
                GamePanels[4].SetActive(true);
                break;
        }
    }

    //Guardar el estado anterior
    public void GuardarEstadoJuego()
    {
        EstadoAnterior = estadoJuego.ToString();
    }

    private void cerrarTodo(){
        foreach (GameObject panel in GamePanels)
        {
            panel.SetActive(false);
        }
    }
}
// Enum para representar los diferentes estados del juego
public enum EstadosJuego
{
    Menu,
    PlayTutorial,
    ResumeTutorial,
    PlayGame,
    Pause,
    ResumeGame,
    Config,
    GameOver,
    Creditos
}

public enum Niveles
{
    Tutorial,
    N1,
    N2,
    N3,
    N4,
    N5
}

public enum Resoluciones
{
    r1920x1080,
    r1280x720,
    r720x405
}

public enum Idiomas
{
    Español,
    English
}