using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float tempoMaximoDaPartida;
    [SerializeField]
    private float tempoAtualDaPartida;
    [SerializeField]
    private TextMeshProUGUI textoDoTempoRestante;

    private void Start()
    {
        tempoMaximoDaPartida = 60f;
        tempoAtualDaPartida = 0f;
    }


    void Update()
    {
        contarTempo();
    }

    private void contarTempo() {
        if (tempoMaximoDaPartida != tempoAtualDaPartida)
        {
            tempoAtualDaPartida += Time.deltaTime;
            float tmp = tempoMaximoDaPartida - tempoAtualDaPartida;
            var str = $"Tempo: {(int)tmp} segundos";
            Debug.Log(str);
            textoDoTempoRestante.text = str;
        }
        else
        {
            tempoAtualDaPartida = 0f;
            //Chamar funcao de morte por falta de tempo
        }
    }



}
