using UnityEngine;

public class N2_Player : MonoBehaviour
{
    public static N2_Player instance;
    public Rigidbody2D rb;
    public float speed = 5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
