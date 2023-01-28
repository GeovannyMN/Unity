using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;
    public string[] dialogo;
    private int indice = 0;

    //public GameObject botaoProximoDialogo;
    public float velocidadeTexto= 3f;
    public bool pertoParaFalar = false;

    private void Awake()
    {
        panelDialogo.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && pertoParaFalar)
        {
            if (panelDialogo.activeInHierarchy)
            {
                limparTela();
            }
            else
            {
                panelDialogo.SetActive(true);
                StartCoroutine(Digitar());
            }
        }
        // if (textoDialogo.text == dialogo[indice])
        // {
        //     botaoProximoDialogo.SetActive(true);
        // }
    }

    public void limparTela()
    {
        textoDialogo.text = "";
        indice = 0;
        panelDialogo.SetActive(false);
    }

    IEnumerator Digitar()
    {
        foreach (char letra in dialogo[indice].ToCharArray())
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(velocidadeTexto/100);
        }
    }
    
    public void ProximaLinha()
    {
        //botaoProximoDialogo.SetActive(false);

        if (indice < dialogo.Length -1)
        {
            indice++;
            textoDialogo.text = "";
            StartCoroutine(Digitar());
        }
        else
        {
            limparTela();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pertoParaFalar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pertoParaFalar = false;
            limparTela();
        }
    }
}
