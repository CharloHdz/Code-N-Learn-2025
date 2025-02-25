using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [Header("Parámetros")]
    public float velocidad;
    public float posX;
    public float fuerzaSalto;

    [Header("Componentes")]
    public Rigidbody2D PlayerRB;
    public Animator animator;

    [Header("Proyectil")]
    [SerializeField] private GameObject Proyectil;
    [SerializeField] private GameObject[] Proyectiles;
    [SerializeField] private Transform ProyectilSpawn;

    [Header("Puntos de Referencia")]
    public Transform SpawnPoint;

    [Header("Estado del Jugador")]
    public EstadosJugador estado;
    public string estadoAnterior;
    public bool PararTodo;

    private void Awake()
    {
        // Configuración Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        SpawnPoint = GameObject.Find("SpawnPoint").transform;

        for (int i = 0; i < Proyectiles.Length; i++)
        {
            Proyectiles[i] = Instantiate(Proyectil, ProyectilSpawn.position, Quaternion.identity);
            Proyectiles[i].SetActive(false);
        }
    }

    private void Update()
    {

        //Cambia el estado del jugador
        switch (estado)
        {
            case EstadosJugador.Idle:
                animator.SetInteger("Estado", 0);
                break;
            case EstadosJugador.Avanzar:
                posX = transform.position.x + velocidad * Time.deltaTime;
                animator.SetInteger("Estado", 1);
                break;
            case EstadosJugador.Saltar:
                posX = transform.position.x + velocidad * Time.deltaTime;
                animator.SetInteger("Estado", 2);
                break;
            case EstadosJugador.Disparar:
                animator.SetInteger("Estado", 3);
                break;
        }

        if(PararTodo){
            Parar();
        }

        //Movimiento del jugador
        transform.position = new Vector2(posX, transform.position.y);
        if(PlayerRB.linearVelocity.y > 30){
            PlayerRB.linearVelocity = new Vector2(PlayerRB.linearVelocity.x, 30);
        }
    }

    public void Parar()
    {
        StopAllCoroutines();
        //Limpiar corutinas
    }

    public void Disparar()
    {
        for (int i = 0; i < Proyectiles.Length; i++)
        {
            if (!Proyectiles[i].activeInHierarchy)
            {
                Proyectiles[i].transform.position = ProyectilSpawn.position;
                Proyectiles[i].SetActive(true);
                break;
            }
        }
    }

    public void ResetTriggers(){
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
    }

    public void GuardarEstadoJugador(){
    //GUardar el estado anterio del jugador
        estadoAnterior = estado.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MetaTutorial"))
        {
            GameManager.instance.MetaAlcanzadaTutorial();
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Muerte")){
            transform.position = SpawnPoint.position;
            Lienzo_UI.Instance.EliminarBloquesEnLienzo(0.5f);
        }
    }

}

public enum EstadosJugador
{
    Idle,
    Avanzar,
    Saltar,
    Disparar
}
