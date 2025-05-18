using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [SerializeField] private Dialogos[] dialogos;
    [SerializeField] private GameObject Prev;
    public GameObject NextStepBtn;
    public Image Fondo;

    [Header("Objetos")]
    public GameObject[] DialogueObjects;

    //Instance
    public static DialogueManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        dialogueText.text = dialogos[0].dialogo;
        rt.anchoredPosition = dialogos[0].Posicion;
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
                    NextStepBtn.SetActive(dialogos[i + 1].ShowNextStep);
                    Fondo.sprite = dialogos[i + 1].Fondo;
                    rt.sizeDelta = dialogos[i + 1].sizeRectTransform;
                    dialogueText.fontSize = dialogos[i + 1].MaxFontSize;


                    //Aparicion de Objetos
                    //Desactivamos todos los objetos+
                    for (int j = 0; j < DialogueObjects.Length; j++)
                    {
                        DialogueObjects[j].SetActive(false);
                    }
                    switch (dialogos[i + 1].ID)
                    {
                        case 1:
                            //DialogueObjects[0].SetActive(true);
                            break;
                        case 2:
                            //DialogueObjects[1].SetActive(true);
                            break;
                        case 3:
                            DialogueObjects[0].SetActive(true);
                            break;
                        case 4:
                            //DialogueObjects[3].SetActive(true);
                            break;
                        case 5:
                            //DialogueObjects[4].SetActive(true);
                            break;
                        default:
                            break;
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
