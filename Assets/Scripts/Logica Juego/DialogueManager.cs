using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject[] FondosDialogos;
    [SerializeField] private Dialogos[] dialogos;

    [Header("Objetos")]
    public GameObject[] DialogueObjects;

    //Instance
    public static DialogueManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        //Cargar el primer dialog
        CerrarDialogo();
        switch (dialogos)
        {
            case Dialogos[] dialogos when dialogos[0].tipoDialogo == Dialogos.TipoDialogo.FondoCortoBoton:
                FondosDialogos[0].SetActive(true);
                break;
            case Dialogos[] dialogos when dialogos[0].tipoDialogo == Dialogos.TipoDialogo.FondoCorto:
                FondosDialogos[1].SetActive(true);
                break;
            case Dialogos[] dialogos when dialogos[0].tipoDialogo == Dialogos.TipoDialogo.FondoLargoBoton:
                FondosDialogos[2].SetActive(true);
                break;
            case Dialogos[] dialogos when dialogos[0].tipoDialogo == Dialogos.TipoDialogo.FondoLargo:
                FondosDialogos[3].SetActive(true);
                break;
            case Dialogos[] dialogos when dialogos[0].tipoDialogo == Dialogos.TipoDialogo.SinFondo:
                FondosDialogos[4].SetActive(true);
                break;
        }


    }

    public void NextDialogue()
    {
        for (int i = 0; i < dialogos.Length; i++)
        {
            if (dialogos[i].ID == 0)
            {
                //Desactivar el dialogo actual
                FondosDialogos[i].SetActive(false);
                //Activar el siguiente dialogo
                FondosDialogos[i + 1].SetActive(true);
                //Actualizar el texto del dialogo
                DialogueObjects[0].GetComponent<TextMeshProUGUI>().text = dialogos[i + 1].dialogo;
                //Actualizar la posicion del dialogo
                RectTransform rt = GetComponent<RectTransform>();
                rt.anchoredPosition = dialogos[i + 1].Posicion;
            }
        }
    }

    public void CerrarDialogo()
    {
        for (int i = 0; i < FondosDialogos.Length; i++)
        {
            FondosDialogos[i].SetActive(false);
        }
    }
}
