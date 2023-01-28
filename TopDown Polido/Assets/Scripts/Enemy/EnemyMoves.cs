using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMoves : MonoBehaviour
{

    public Animator animator;

    public EstadosData state;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float velocidadeDeMovimento;

    [SerializeField]
    private float distanciaMinima;

    private float constante = 100;

    [SerializeField]
    private float raioDeVisao = 4f;

    [SerializeField]
    private LayerMask layersBuscaveis;

    private Transform alvo;

    private void Awake () {
	animator = GetComponent<Animator>();
	
}

    void Update()
    {
        ProcurarAlvo(); //Procura alguém para atacar
        if (this.alvo != null){
            Mover();
        }else{
            Parar();
        }
        
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, this.raioDeVisao);
        if (this.alvo != null){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.alvo.position);
        }
    }

    private void ProcurarAlvo() {
        Collider2D colliderJogador = Physics2D.OverlapCircle(this.transform.position, this.raioDeVisao, this.layersBuscaveis); //Encontrou o jogador
        if (colliderJogador != null){ //Se tem um jogador para perseguir
            this.alvo = colliderJogador.transform; //Coloca o player como alvo para o inimigo seguir
        }else{
            this.alvo = null; //A
        }
    }

    private void Mover (){
	  
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;
        //soundFX.playSound(sound.FOLLOW);

        float distanciaAtual = Vector2.Distance(posicaoAtual, posicaoAlvo); //O quão perto o player está

        if (distanciaAtual > this.distanciaMinima){ //Se ainda não chegou no player

            Vector2 direcao = posicaoAlvo - posicaoAtual; //Observa a direção dele
            direcao = direcao.normalized; //Normaliza ela

            this.rb.velocity = direcao * velocidadeDeMovimento * constante * Time.fixedDeltaTime; //Se move até ela

             if (distanciaAtual> 3)
             {
                state = EstadosData.Moving;
                animator.SetInteger("estado", (int)state);
            }
              

            if (this.rb.velocity.x > 0){ //Olha para à direita
                this.sprite.flipX = false; //Vira o sprite para a direita
            }else if (this.rb.velocity.x < 0){ //Olha para à esquerda
                this.sprite.flipX = true; //Vira o sprite para a esquerda
            }
        }else{ //Se já chegou no player
            Parar();

  
        }
    }

    private void Parar()
    {
     		
        this.rb.velocity = Vector2.zero; //Para de se mover
        state = EstadosData.Idle;
        animator.SetInteger("estado", (int)state);
    }
}
