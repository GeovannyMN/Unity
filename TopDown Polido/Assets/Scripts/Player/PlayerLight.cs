using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    private ControlLight controladorDeLuz;
    //private PlayerLife pf; 

    void Start()
    {
        controladorDeLuz = GameObject.FindObjectOfType<ControlLight>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if("Luz".Equals(collision.gameObject.tag))
            controladorDeLuz.IniciaRecargaDaLuz();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if("Luz".Equals(collision.gameObject.tag))
            controladorDeLuz.ParaRecargaDaLuz();
    }

    public void ApagaLuzParaGameOver()
    {
        controladorDeLuz.ApagaLuzParaGameOver();
	  
    }
}
