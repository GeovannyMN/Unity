using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool estaSeguindo = false;

    [SerializeField]
    private float velocidadeSeguir = 1f;

    public Transform alvoSeguir;

    private PlayerMoves pm;

    void Start()
    {
        pm = FindObjectOfType<PlayerMoves>();
    }


    void Update()
    {
        if (estaSeguindo)
        {
            transform.position = Vector2.Lerp(transform.position, alvoSeguir.position, velocidadeSeguir * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!estaSeguindo)
            {
                alvoSeguir = pm.pontoParaChaveSeguir;
                estaSeguindo = true;
                pm.chaveSeguindo = this;
            }
        }
    }
}
