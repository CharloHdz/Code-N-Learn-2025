using UnityEngine;

public class SelectorPlayer : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private float moveInput;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    [Header("Salto")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private int jumpCount = 0;
    public int maxJumps = 2;

    [Header("Ataque")]
    public bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento lateral
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Mirar hacia el lado correcto
        if (moveInput != 0)
            sr.flipX = moveInput < 0;

        // Verificar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGrounded) jumpCount = 0;

        // Saltar / Doble salto
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }

        // Ataque (placeholder con click izquierdo)
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        // Animaciones
        if (isAttacking)
        {
            animator.SetFloat("Action", 3f); // Ataque
        }
        else if (!isGrounded)
        {
            animator.SetFloat("Action", 4f); // Salto
        }
        else if (moveInput != 0)
        {
            animator.SetFloat("Action", 2f); // Correr
        }
        else
        {
            animator.SetFloat("Action", 1f); // Idle
        }
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetFloat("Action", 3f);
        yield return new WaitForSeconds(0.5f); // Duración del ataque
        isAttacking = false;
    }

}
