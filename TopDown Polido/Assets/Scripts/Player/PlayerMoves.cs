using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public static bool GameOver = false;
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sprite;

    public float velocidadeDeMovimento;

    [SerializeField]
    private Vector2 direcao;

    public DirecaoDeMovimento direcaoDeMovimento;

    private float constante = 100;

    public Animator animator;

    public Transform pontoParaChaveSeguir;

    public Key chaveSeguindo;

    private void Awake() {
        this.direcaoDeMovimento = DirecaoDeMovimento.Direita;
        GameOver = false;
    }

    void Update()
    {
        if (GameOver)
        {

            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direcao = new Vector2 (horizontal, vertical); //Direção de movimentação nos eixos horizontal e vertical
        direcao = direcao.normalized;

        AtualizarDirecaoDeMovimento();

        this.rb.velocity = direcao * this.velocidadeDeMovimento * constante * Time.fixedDeltaTime; //Movimento constante em 8 direções

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", direcao.sqrMagnitude);

        if(direcao.y > 0){
            animator.SetFloat("Direction", 1);
        }else if (direcao.y < 0)
        {
            animator.SetFloat("Direction", 0);
        }
    }
    
    private void LateUpdate() {
        AtualizarAnimacao();
    }
    
    private void AtualizarDirecaoDeMovimento()
    {
        if (direcao.x > 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Direita;
        }else if (direcao.x < 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Esquerda;
        }

        if (direcao.y > 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Cima;
        }else if (direcao.y < 0){
            this.direcaoDeMovimento = DirecaoDeMovimento.Baixo;
        }
    }

    private void AtualizarAnimacao()
    {
        if (direcao.x > 0){
            this.sprite.flipX = false;
        }else if (direcao.x < 0)
        {
            this.sprite.flipX = true;
        }
    }

    public void AumentarVelocidade (float mudanca){
        this.velocidadeDeMovimento += Mathf.Abs(mudanca);
    }

    public void DiminuirVelocidade (float mudanca){
        this.velocidadeDeMovimento -= Mathf.Abs(mudanca);
    }
    
}