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
    void Start()
    {
        pm = FindObjectOfType<PlayerMoves>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (esperandoAbrir)
        {
            if (Vector2.Distance(pm.chaveSeguindo.transform.position, transform.position) < 0.1f)
            {
                esperandoAbrir = false;
                estaAberta = true;
                sr.sprite = spritePortaAberta;

                pm.chaveSeguindo.gameObject.SetActive(false);
                pm.chaveSeguindo = null;
            }
        }
        //Se a porta está aberta e o player está perto e pressiona a tecla T então
        if (estaAberta && Input.GetKey(KeyCode.T))
        {
            //Teleporte-se para algum outro lugar
            SceneManager.LoadScene("Nivel Zero");
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
