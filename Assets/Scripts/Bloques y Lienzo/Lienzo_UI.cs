using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Lienzo_UI : MonoBehaviour
{
    // =================== VARIABLES ===================
    [Header("General Settings")]
    public List<GameObject> ObjectIDList;  // Lista de objetos UI
    public RectTransform panelRectTransform;  // Referencia al área del lienzo (Panel)
    public Canvas canvas;  // Referencia al Canvas, necesario para calcular las posiciones en pantalla
    public Player player;
    
    [Header("Play Button Settings")]
    [SerializeField] private GameObject PlayBtn;
    [SerializeField] private List<Sprite> PlayBtnState;
    private Image PlayBtnImage;
    public string EstadoJuego;

    [Header("Preview For Player")]
    public GameObject[] PreviewWalk;
    public GameObject[] PreviewJump;
    public GameObject[] PreviewShoot;
    private float PreviewPosX;
    public bool Salto;

    // Singleton
    public static Lienzo_UI Instance { get; private set; }

    // =================== SINGLETON ===================
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // =================== MÉTODOS DE UNITY ===================
    void Start()
    {
        player = FindObjectOfType<Player>();
        ObjectIDList = new List<GameObject>();  // Inicializamos la lista de objetos
        PlayBtnImage = PlayBtn.GetComponent<Image>();

    }

    void Update()
    {
        RevisarObjetosDentroDelPanel();
        Sort();
    }

    // =================== MÉTODOS PERSONALIZADOS ===================
    
    // ---- Inicialización ----
    public void InicializarPreviews(GameObject[] previews)
    {
        for (int i = 0; i < previews.Length; i++)
        {
            previews[i] = Instantiate(previews[i], Vector3.zero, Quaternion.identity);
            previews[i].SetActive(false);
        }
    }

    // ---- Funcionalidades de Juego ----
    public void Play()
    {
        StartCoroutine(PlayGame());
        RestartPreviewPos();
    }

    public IEnumerator PlayGame()
    {
        switch(Player.Instance.KillSwitch)
        {
            case true:
                print("PERRAAAAAA");
                break;
            case false:
                EstadoJuego = "Leyendo";
                PlayBtnImage.sprite = PlayBtnState[1];
                player.transform.position = Player.Instance.SpawnPoint.position;
                Player.Instance.posX = player.transform.position.x;

                for (int i = 0; i < ObjectIDList.Count; i++)
                {
                    // Llamar a la instrucción de cada objeto si está dentro del panel
                    if (IsObjectInsidePanel(ObjectIDList[i]))
                    {
                        Player.Instance.GuardarEstadoJugador();
                        ObjectIDList[i].GetComponent<ObjectID_UI>().Instruction();
                        ObjectIDList[i].GetComponent<ObjectID_UI>().InstruccionCompleta();
                    }

                    // Esperar 1 segundo antes de pasar al siguiente
                    yield return new WaitForSeconds(1f);

                    //Ejecutar acción cuando se hayan completado todas las instrucciones
                    if (i == ObjectIDList.Count - 1)
                    {
                        Player.Instance.estado = EstadosJugador.Idle;
                        EstadoJuego = "Lectura Completada";
                        PlayBtnImage.sprite = PlayBtnState[0];
                    }
                }
                break;
        }

    }

    public void Transform(){
        player.transform.position = Player.Instance.SpawnPoint.position;
    }

    public void ResetBlock()
    {
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            ObjectIDList[i].GetComponent<ObjectID_UI>().InstruccionCompleta();
        }
    }

    public IEnumerator EliminarBloquesEnLienzo(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            Destroy(ObjectIDList[i]);
        }
        ObjectIDList.Clear();
    }

    // ---- Previews ----
    public void MostrarPreview()
    {
        // Reinicia valores y borra los previews anteriores
        PreviewPosX = 0;
        foreach (GameObject preview in GameObject.FindGameObjectsWithTag("Preview"))
        {
            preview.SetActive(false);
        }

        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            switch (ObjectIDList[i].GetComponent<ObjectID_UI>().tipoBloque)
            {
                case TipoBloque.Avanzar:
                    Salto = false;
                    PreviewPosX += 2;
                    Vector3 previewPosition = Player.Instance.SpawnPoint.position + new Vector3(PreviewPosX, 0, 0);
                    LlamarPreview("Avanzar", previewPosition);
                    break;

                case TipoBloque.Saltar:
                    Salto = true;
                    PreviewPosX += 2;
                    Vector3 previewPosition1 = Player.Instance.SpawnPoint.position + new Vector3(PreviewPosX, 0, 0);
                    LlamarPreview("Saltar", previewPosition1);
                    break;

                case TipoBloque.Disparar:
                    Salto = false;
                    break;
            }
        }
    }

    public void LlamarPreview(string tipoPreview, Vector3 Pos)
    {
        GameObject[] previewArray = tipoPreview switch
        {
            "Avanzar" => PreviewWalk,
            "Saltar" => PreviewJump,
            "Disparar" => PreviewShoot,
            _ => null
        };

        if (previewArray == null) return;

        foreach (var preview in previewArray)
        {
            if (!preview.activeInHierarchy)
            {
                preview.transform.position = Pos;
                preview.SetActive(true);
                break;
            }
        }
    }

    public void RestartPreviewPos()
    {
        PreviewPosX = 0;
    }

    // ---- Utilidades ----
    void Sort()
    {
        ObjectIDList = ObjectIDList.OrderByDescending(obj => obj.transform.position.x * -1).ToList();
    }

    public bool IsObjectInsidePanel(GameObject obj)
    {
        RectTransform objRectTransform = obj.GetComponent<RectTransform>();
        if (objRectTransform == null) return false;

        // Verificar si el punto está dentro del RectTransform del panel
        return RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, objRectTransform.position, canvas.worldCamera);
    }

    private void RevisarObjetosDentroDelPanel()
    {
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            if (!IsObjectInsidePanel(ObjectIDList[i]))
            {
                ObjectIDList.RemoveAt(i);
            }
        }
    }
}



