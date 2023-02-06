using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScaller : MonoBehaviour
{
    private bool recebendoDano = false;
    private bool recebendoCura = false;
    
    private float vidaAtual = 3.0f;
    private float vidaAnterior = 3.0f;

    private float escalaOriginal;
    private float passoReducaoEscala;

    void Awake(){
        escalaOriginal = gameObject.transform.localScale.x;
        vidaAtual = escalaOriginal;
        vidaAnterior = escalaOriginal;
        passoReducaoEscala = escalaOriginal / 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recebendoDano)
            DiminuiVida();
        if(recebendoCura)
            AumentaVida();
    }

    private void DiminuiVida(){
        if(vidaAtual > vidaAnterior - passoReducaoEscala){
            gameObject.transform.localScale -= new Vector3(0.1f, 0, 0);
            vidaAtual -= 0.1f;
        }else {
            recebendoDano = false;
        }
    }

    private void AumentaVida(){
        if(vidaAtual < vidaAnterior + passoReducaoEscala){
            gameObject.transform.localScale += new Vector3(0.1f, 0, 0);
            vidaAtual += 0.1f;
        }
        else 
            recebendoCura = false;
    }

    public void RecebeDano(float dano){
        vidaAnterior = vidaAtual;
        recebendoDano = true;
    }

    public void RecebeCura(float vida){
        vidaAnterior = vidaAtual;
        recebendoCura = true;
    }
}
