using UnityEngine;

public class SelectorPlayer : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody2D rb;
    private Vector3 input;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Captura input de teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        input = new Vector3(horizontal, 0f, vertical).normalized;

        //Salto con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }

        //Animaciones Set Float
        //1. Idle, 2. Run, 3. Attack, 4. Jump
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
            anim.SetFloat("Action", 1);
        }
        else if (Input.GetKeyDown(KeyCode.F)){
            anim.SetFloat("Action", 3);
        }
        else if (Input.GetKeyDown(KeyCode.Space)){
            anim.SetFloat("Action", 4);
        }
        else if (input == Vector3.zero){
            anim.SetFloat("Action", 1);
        }
        else{
            anim.SetFloat("Action", 2);
        }
    }

    void FixedUpdate()
    {
        // Movimiento con física
        Vector3 movimiento = input * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);
    }

}
