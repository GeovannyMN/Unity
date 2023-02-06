using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private PlayerMoves pm;

    private SpriteRenderer sr;

    [SerializeField]
    private Sprite spritePortaAberta;

    [SerializeField]
    private bool estaAberta;

    [SerializeField]
    private bool esperandoAbrir;

    [SerializeField]
    private PolygonCollider2D colliderBarreira;

    void Start()
    {
        pm = FindObjectOfType<PlayerMoves>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (esperandoAbrir)
        {
            //Distância entre a chave e a porta chegou no mínimo
            if (Vector2.Distance(pm.chaveSeguindo.transform.position, transform.position) < 0.1f) 
            { 
                //Muda o estado da porta para aberta, trocando o sprite
                esperandoAbrir = false;
                estaAberta = true;
                sr.sprite = spritePortaAberta;

                //Desativa o objeto chave da cena e aponta o ponteiro da chave para null
                pm.chaveSeguindo.gameObject.SetActive(false);
                pm.chaveSeguindo = null;

                //Desativa a barreira no topo da porta para poder passar por ela
                colliderBarreira.enabled = false;
            }
        }
        //Se a porta está aberta e o player está perto e pressiona a tecla T então
        if (estaAberta)
        {
            //Teleporte-se para algum outro lugar
      
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("Nivel Um");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //Se econtrou com o Player
        {
            if (pm.chaveSeguindo != null) //E tem a chave
            {
                pm.chaveSeguindo.alvoSeguir = transform; //Chave para de seguir o player para flutuar na porta
                esperandoAbrir = true;
            }
        }
    }

}
