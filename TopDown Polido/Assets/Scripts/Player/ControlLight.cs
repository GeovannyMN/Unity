using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControlLight : MonoBehaviour
{

    private Light2D playerLight;
    [SerializeField]
    private float velocidadeDeReducaoDaLuz = 0.01f;
    [SerializeField]
    private float velocidadeAumentoDaLuz = 0.5f;

    private bool reduzindoLuz = true;
       
    void Start()
    {
        playerLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMoves.GameOver)
        {
            ReduzLugParaGameOver();
            return;
        }
        if(reduzindoLuz)
            ReduzLuz();
        else
            AumentaLuz();

        if (InnerRadiusLuzNaoChegouLimiteInferior() && OuterRadiusLuzChegouLimiteInferior())
        {
            PlayerMoves.GameOver = true;
        }
    }

    private void ReduzLuz()
    {
        if(InnerRadiusLuzNaoChegouLimiteInferior()){
            playerLight.pointLightInnerRadius -= velocidadeDeReducaoDaLuz * Time.deltaTime;
        }
        if(!InnerRadiusLuzNaoChegouLimiteInferior())
        {
            playerLight.pointLightOuterRadius -= velocidadeDeReducaoDaLuz * Time.deltaTime;
        }
    }

    private void ReduzLugParaGameOver()
    {
        if(playerLight.pointLightInnerRadius > 0)
            playerLight.pointLightInnerRadius -= velocidadeDeReducaoDaLuz * Time.deltaTime;
        if(playerLight.pointLightOuterRadius > 0)
            playerLight.pointLightOuterRadius -= velocidadeDeReducaoDaLuz * Time.deltaTime;
    }

    private void AumentaLuz(){
        if(InnerRadiusNaoChegouLimiteSuperior())
            playerLight.pointLightInnerRadius += velocidadeAumentoDaLuz * Time.deltaTime;
        if(OuterLightNaoChegouLimiteSuperior())
            playerLight.pointLightOuterRadius += velocidadeAumentoDaLuz * Time.deltaTime;        
    }

    private bool InnerRadiusNaoChegouLimiteSuperior(){
        return playerLight.pointLightInnerRadius <= 3;
    }

    private bool OuterLightNaoChegouLimiteSuperior(){
        return playerLight.pointLightOuterRadius <= 4;
    }

    private bool InnerRadiusLuzNaoChegouLimiteInferior(){
        return playerLight.pointLightInnerRadius > 1;
    }

    private bool OuterRadiusLuzChegouLimiteInferior()
    {
        return playerLight.pointLightOuterRadius <= 0;
    }

    public void IniciaRecargaDaLuz()
    {
        reduzindoLuz = false;
    }

    public void ParaRecargaDaLuz(){
        reduzindoLuz = true;
    }

    public void ApagaLuzParaGameOver()
    {
        reduzindoLuz = true;
        velocidadeDeReducaoDaLuz = 2.0f;
    }
}
