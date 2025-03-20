using UnityEngine;

public class EnemyCS : MonoBehaviour
{
    public bool perseguir;
    public float vel;
    public GameObject player;
    private bool der;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (perseguir)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.05f);
        } else
        {
            //patrullar
            if(der)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + vel * Time.deltaTime, transform.position.y), 0.05f);
            } else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - vel * Time.deltaTime, transform.position.y), 0.05f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            perseguir = true;
        } else
        {
            perseguir = false;
        }

        if (other.gameObject.tag == "Pared")
        {
            if (der)
            {
                der = false;
            } else
            {
                der = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
