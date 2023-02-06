using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField]
    private Transform pontoDeAtaqueDaEsquerda;

    [SerializeField]
    private Transform pontoDeAtaqueDaDireita;

    [SerializeField]
    private Transform pontoDeAtaqueDeCima;

    [SerializeField]
    private Transform pontoDeAtaqueDeBaixo;

    [SerializeField]
    private float raioDeAtaque;

    [SerializeField]
    private float danoDeAtaque;

    [SerializeField]
    private LayerMask layersAtacaveis;

    [SerializeField]
    private PlayerMoves jogador;

    public Transform pontoDeAtaque;

    public Animator animator;


    private void Awake() {
        pontoDeAtaque = pontoDeAtaqueDaDireita; //Player começa atacando para a direita
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){    //Se apertar o esquerdo do mouse 
            soundFX.playSound(sound.ATTACK_PLAYER);
            animator.SetInteger("Attack", 1);
            Atacar();           //Você ataca
            StartCoroutine(waiter());
        }
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.white; //Por padrão é branco
        if (this.pontoDeAtaqueDaDireita != null){    
            Gizmos.DrawWireSphere(this.pontoDeAtaqueDaDireita.position, this.raioDeAtaque); //Cria a linha de visão do player olhando para direita
        }
        if (this.pontoDeAtaqueDaEsquerda != null){    
            Gizmos.DrawWireSphere(this.pontoDeAtaqueDaEsquerda.position, this.raioDeAtaque); //Cria a linha de visão do player olhando para esquerda
        }
        if (this.pontoDeAtaqueDeCima != null){    
            Gizmos.DrawWireSphere(this.pontoDeAtaqueDeCima.position, this.raioDeAtaque); //Cria a linha de visão do player olhando para cima
        }
        if (this.pontoDeAtaqueDeBaixo != null){    
            Gizmos.DrawWireSphere(this.pontoDeAtaqueDeBaixo.position, this.raioDeAtaque); //Cria a linha de visão do player olhando para baixo
        }
        AtualizarDirecaoDeAtaque();
        Gizmos.color = Color.red; //Se estiver atacando é vermelho
        Gizmos.DrawWireSphere(pontoDeAtaque.position, this.raioDeAtaque);
    }

    private void Atacar(){

        AtualizarDirecaoDeAtaque();
        Collider2D colliderInimigo = Physics2D.OverlapCircle(pontoDeAtaque.position, this.raioDeAtaque, this.layersAtacaveis); //Cria a zona de ataque do player
        if (colliderInimigo != null){
            EnemyLife inimigo = colliderInimigo.GetComponent<EnemyLife>(); //Se encontrou um inimigo, descobre quem é
            if (inimigo != null){
                inimigo.ReceberDano(this.danoDeAtaque); //Se encontrou quem é, causa dano nele
            }
        }
    }

    private void AtualizarDirecaoDeAtaque()
    {
        if (this.jogador.direcaoDeMovimento == DirecaoDeMovimento.Direita){ //Se olha para direita, ataca para direita
            pontoDeAtaque = this.pontoDeAtaqueDaDireita;
            animator.SetInteger("AttackDirection", 0);
        }
        else if (this.jogador.direcaoDeMovimento == DirecaoDeMovimento.Esquerda){ //Se olha para esquerda, ataca para esquerda
            pontoDeAtaque = this.pontoDeAtaqueDaEsquerda;
            animator.SetInteger("AttackDirection", 1);
        }

        if (this.jogador.direcaoDeMovimento == DirecaoDeMovimento.Cima){ //Se olha para cima, ataca para cima
            pontoDeAtaque = this.pontoDeAtaqueDeCima;
            animator.SetInteger("AttackDirection", 2);
        }
        else if (this.jogador.direcaoDeMovimento == DirecaoDeMovimento.Baixo){ //Se olha para baixo, ataca para baixo
            pontoDeAtaque = this.pontoDeAtaqueDeBaixo;
            animator.SetInteger("AttackDirection", 3);
        }
    }

    IEnumerator waiter()
    {
        //Wait for 1 second
        yield return new WaitForSeconds(0.75f);
        animator.SetInteger("Attack", 0);


    }
}
