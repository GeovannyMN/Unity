using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    private float vidaMaxP;

    [SerializeField]
    private float vidaAtualP;

    [SerializeField]
    private BarraDeVida barraDeVidaP;

    private SpriteRenderer sprite;

    public bool invulneravel = false;

    [SerializeField]
    private GameObject menuGameOver;

    private void Awake() {
        this.vidaAtualP = this.vidaMaxP; //O inimigo tem a vida atual igual a vida máxima 
        if (barraDeVidaP != null){
            this.barraDeVidaP.VidaMaximaDoSlider = this.vidaMaxP; //A barra de vida tem a vida máxima igual a vida máxima
            this.barraDeVidaP.VidaAtualDoSlider = this.vidaAtualP; //A barra de vida tem a vida atual igual a vida atual
        }
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ReceberDano(float dano){

        this.vidaAtualP -= Mathf.Abs(dano); //Retira da vida do inimigo, o dano que lhe causaram

        this.invulneravel = true; //Ativa a invulnerabilidade por 1 segundo

        this.barraDeVidaP.VidaAtualDoSlider = this.vidaAtualP; //Atualiza no slider o dano sofrido
        
        if (this.vidaAtualP <= 0){
            soundFX.playSound(sound.DEATH_PLAYER);
            GameObject.Destroy(this.gameObject); //Player morreu
            //ApagaLuzPlayer();
            MostraMenuGameOver();
            //ReiniciarLevel(); //Após 2 segundos, reinicia a fase
        }
        else {
            StartCoroutine(PiscarSpriteSofreuDano()); //Realiza a animação de dano
        }
    }

    public void ReceberVida(float vida) {
        this.vidaAtualP += Mathf.Abs(vida); //Adiciona a vida do player, a vida que lhe deram

        this.barraDeVidaP.VidaAtualDoSlider = this.vidaAtualP; //Atualiza no slider o dano curado

        if (this.vidaAtualP > this.vidaMaxP){ //Se curar mais que o máximo
            this.vidaAtualP = this.vidaMaxP; //Limita a vida no máximo
        }
    }

    IEnumerator PiscarSpriteSofreuDano()
    {
        int i = 0;
        for (; i<10; i++) //Faz isso por dez segundos
        {
            sprite.enabled = false; //Desliga a sprite por 0.1 segundos
            yield return new WaitForSecondsRealtime(0.05f);
            sprite.enabled = true; //Liga a sprite por 0.1 segundos
            yield return new WaitForSecondsRealtime(0.05f);
        }
        this.invulneravel = false; //Após a animação de dano, desativa a invulnerabilidade
    }

    void ReiniciarLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ApagaLuzPlayer()
    {
        PlayerMoves.GameOver = true;
        var luz = FindObjectOfType<PlayerLight>();
        luz.ApagaLuzParaGameOver();
    }

    public void MostraMenuGameOver()
    {
        menuGameOver.SetActive(true);
    }

}
