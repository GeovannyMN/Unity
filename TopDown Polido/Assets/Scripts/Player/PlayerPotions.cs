using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotions : MonoBehaviour
{
    
    [SerializeField]
    private float valorVidaPocao; //Valor deve ser float

    [SerializeField]
    private float valorDanoPocao; //Valor deve ser float

    [SerializeField]
    private float valorVelocidadePocao; //Valor deve estar entre 0.0f e 3.0f para zero mudança e até 300% de velocidade

    [SerializeField]
    private LayerMask layersPocao; //Camadas com poções para acessar usar e destruir


    private void OnTriggerEnter2D(Collider2D other) {
        if (((1<<other.gameObject.layer) & layersPocao) != 0){ //Se for uma poção
            if (other.gameObject.CompareTag("Vida")){
                StartCoroutine(aplicarVida(valorVidaPocao));    //Adiciona a vida ao longo do tempo por corrotina
            }else if (other.gameObject.CompareTag("Veneno")){
                StartCoroutine(aplicarDano(valorDanoPocao));    //Retira a vida ao longo do tempo por corrotina
            }else if (other.gameObject.CompareTag("Velocidade")){
                StartCoroutine(aplicarNovaVelocidade()); //Aumenta a velocidade ao longo do tempo por corrotina  
            }
            Destroy(other.gameObject); //Consuma, destrua a poção
        }
    }

    IEnumerator aplicarNovaVelocidade(){
        PlayerMoves pm = GetComponent<PlayerMoves>(); //Pega o componente de movimento do player
        if (pm != null)
        {
            pm.AumentarVelocidade(valorVelocidadePocao/2); //Aumenta 50% do acréscimo de velocidade
            yield return new WaitForSecondsRealtime(1f); //Espera 1 segundo
            pm.AumentarVelocidade(valorVelocidadePocao / 2); //Aumenta 100% do acréscimo de velocidade
            yield return new WaitForSecondsRealtime(2f); //Espera 2 segundos
            pm.DiminuirVelocidade(valorVelocidadePocao / 2); //Diminui 50% do acréscimo de velocidade
            yield return new WaitForSecondsRealtime(1f); //Espera 1 segundo
            pm.DiminuirVelocidade(valorVelocidadePocao/2); //Diminui 100% do acréscimo de velocidade
        }
    }

   IEnumerator aplicarVida(float vida){
        PlayerLife pf = GetComponent<PlayerLife>(); //Pega o componente de vida do player
        if (pf != null)
        {
            for (int i = 0; i < 4; i++)
            {
                pf.ReceberVida(vida / 5); //Aplica +20% da cura
                yield return new WaitForSecondsRealtime(1f); //Espera 1 segundo
            }
            pf.ReceberVida(vida / 5); //Aplica os 20% restantes, totalizando 100% da cura
        }
    }

    IEnumerator aplicarDano(float dano){
        PlayerLife pf = GetComponent<PlayerLife>(); //Pega o componente de vida do player
        if (pf != null)
        {
            for (int i = 0; i < 4; i++)
            {
                pf.ReceberDano(dano / 5); //Aplica +20% do dano
                yield return new WaitForSecondsRealtime(1f); //Espera 1 segundo
            }
            pf.ReceberDano(dano / 5); //Aplica os 20% restantes, totalizando 100% do dano
        }
    }
}
