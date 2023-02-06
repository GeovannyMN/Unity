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
            soundFX.playSound(sound.HEAL);
            if (other.gameObject.CompareTag("Vida")){
                aplicarVida(valorVidaPocao);    //Adiciona a vida
            }else if (other.gameObject.CompareTag("Veneno")){
                aplicarDano(valorDanoPocao);    //Retira a vida
            }else if (other.gameObject.CompareTag("Velocidade")){
                StartCoroutine(aplicarNovaVelocidade()); //Usa uma corrotina para mudar a velocidade por 5 segundos  
            }
            Destroy(other.gameObject); //Consuma, destrua a poção
        }
    }

    IEnumerator aplicarNovaVelocidade(){
        PlayerMoves pm = GetComponent<PlayerMoves>(); //Pega o componente de movimento do player
        if (pm != null)
        {
            pm.AumentarVelocidade(valorVelocidadePocao); //Muda a velocidade de acordo com o parâmetro valorVelocidadePocao
            yield return new WaitForSecondsRealtime(5f); //Espera 5 segundos de uso
            pm.DiminuirVelocidade(valorVelocidadePocao); //Desfaz a mudança na velocidade
        }
    }

    private void aplicarVida(float vida){
        PlayerLife pf = GetComponent<PlayerLife>(); //Pega o componente de vida do player
        if (pf != null)
        {
            pf.ReceberVida(vida);
        }
    }

    private void aplicarDano(float dano){
        PlayerLife pf = GetComponent<PlayerLife>(); //Pega o componente de vida do player
        if (pf != null)
        {
            pf.ReceberDano(dano);
        }
    }
}
