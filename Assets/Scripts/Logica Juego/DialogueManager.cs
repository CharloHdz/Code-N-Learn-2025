using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [SerializeField] private Dialogos[] dialogos;
    [SerializeField] private GameObject Prev;

    //Instance
    public static DialogueManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        dialogueText.text = dialogos[0].dialogo;
        rt.anchoredPosition = dialogos[0].Posicion;
        Prev.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextDialogue()
    {
        for (int i = 0; i < dialogos.Length; i++)
        {
            RectTransform rt = GetComponent<RectTransform>();
            if (dialogueText.text == dialogos[i].dialogo)
            {
                if (i + 1 < dialogos.Length)
                {
                    dialogueText.text = dialogos[i + 1].dialogo;
                    rt.anchoredPosition = dialogos[i + 1].Posicion;
                    if (dialogos[i + 1].SetAnim == true)
                    {
                        Prev.SetActive(true);
                    }
                    else
                    {
                        Prev.SetActive(false);
                    }
                    break;
                }
                else
                {
                    dialogueText.text = dialogos[0].dialogo;
                    rt.anchoredPosition = dialogos[0].Posicion;
                    break;
                }
            }
        }
    }
}
