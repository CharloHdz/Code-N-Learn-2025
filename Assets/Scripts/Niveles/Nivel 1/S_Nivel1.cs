using UnityEngine;

public class S_Nivel1 : MonoBehaviour
{
    [Header ("Player")]
    public Player player;
    [SerializeField] private Transform[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();

        for (int i = 0; i < 5; i++)
        {
            player.transform.position = spawnPoints[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
