using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sprite;

    public float velocidadeDeMovimento;

    [SerializeField]
    private Vector2 direcao;

    public DirecaoDeMovimento direcaoDeMovimento;

    private float constante = 100;

    //Controladores do sistema de chave
    public Transform pontoParaChaveSeguir;

    public Key chaveSeguindo;

    private void Awake() {
        this.direcaoDeMovimento = DirecaoDeMovimento.Direita;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direcao = new Vector2 (horizontal, vertical); //Direção de movimentação nos eixos horizontal e vertical
        direcao = direcao.normalized;

        AtualizarDirecaoDeMovimento();

        this.rb.velocity = direcao * this.velocidadeDeMovimento * constante * Time.fixedDeltaTime; 
        //Movimento constante em 8 direções

    }

    private void LateUpdate() {
        AtualizarAnimacao();
    }
    
    private void AtualizarDirecaoDeMovimento()
    {
        if (direcao.x > 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Direita; //Se olha para a direita
        }else if (direcao.x < 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Esquerda; //Se olha para a esquerda
        }

        if (direcao.y > 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Cima; //Se olha para cima
        }
        else if (direcao.y < 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Baixo; //Se olha para baixo
        }
    }

    private void AtualizarAnimacao()
    {
        if (direcao.x > 0){
            this.sprite.flipX = false; //Se olha a direita, "espelha" a sprite para a direita
        }else if (direcao.x < 0)
        {
            this.sprite.flipX = true; //Se olha a esquerda, "espelha" a sprite para a esquerda
        }
    }

    public void AumentarVelocidade (float mudanca){
        this.velocidadeDeMovimento += Mathf.Abs(mudanca); //Muda a velocidade de movimento para mais
    }

    public void DiminuirVelocidade (float mudanca){
        this.velocidadeDeMovimento -= Mathf.Abs(mudanca); //Musa a velocidade de movimento para menos
    }

    


}