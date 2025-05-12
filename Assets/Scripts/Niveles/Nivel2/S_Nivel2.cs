using UnityEngine;

public class S_Nivel2 : MonoBehaviour
{
    public GameObject[] Blocks;
    public DeleteArea_UI deleteArea;
    public Canvas canvas;

    [Header("Capas")]
    public GameObject[] layers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnActionBlock1(){
        //Spawn como hijo de canvas y dentro de deleteArea
        GameObject block = Instantiate(Blocks[1], transform.position, Quaternion.identity);
        block.transform.SetParent(layers[1].transform, false);
        block.transform.position = deleteArea.rectTransform.position;
    }

    public void SpawnInputBlock(){
        GameObject block = Instantiate(Blocks[0], transform.position, Quaternion.identity);
        block.transform.SetParent(layers[0].transform, false);
        block.transform.position = deleteArea.rectTransform.position;
    }
}
