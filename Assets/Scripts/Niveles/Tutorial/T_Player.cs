using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_Player : MonoBehaviour
{
    public static T_Player Instance { get; private set; }
    [Header("Parámetros")]
    public float velocidad;
    public float posX;
    public float fuerzaSalto;
    public bool KillSwitch;

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

    [Header ("Versión de Niveles")]
    public VersionPlayer versionPlayer;
    public enum VersionPlayer{T, N1, N2, N3, N4, N5}
    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();

        for (int i = 0; i < Proyectiles.Length; i++)
        {
            Proyectiles[i] = Instantiate(Proyectil, ProyectilSpawn.position, Quaternion.identity);
            Proyectiles[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (KillSwitch)
        {
            estado = EstadosJugador.Idle;
            Lienzo_UI.Instance.Transform();
        }
        else {
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
        }

        if(PararTodo){
            Parar();
        }

        //Movimiento del jugador
        transform.position = new Vector2(posX, transform.position.y);
        if(PlayerRB.linearVelocity.y > 30){
            PlayerRB.linearVelocity = new Vector2(PlayerRB.linearVelocity.x, 30);
        }

        #region Versiones del Jugador por Niveles
        switch(versionPlayer){
            
        }
        #endregion
    }

    public void Parar()
    {
        StopAllCoroutines();
        //Limpiar corutinas
    }

    public IEnumerator Disparar(float sec){
        yield return new WaitForSeconds(sec);
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

    public void Muerte(){
        KillSwitch = true;
        StartCoroutine(DisableKillSwitch(1f));
        Lienzo_UI.Instance.StartCoroutine(Lienzo_UI.Instance.EliminarBloquesEnLienzo(0.5f));
    }

    IEnumerator DisableKillSwitch(float time)
    {
        yield return new WaitForSeconds(time);
        KillSwitch = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MetaTutorial"))
        {
            S_Tutorial.Instance.MetaAlcanzadaTutorial();
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Muerte")){
            Muerte();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Muerte();
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
