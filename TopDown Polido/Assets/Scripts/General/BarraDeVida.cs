using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField]
    private Slider slider; 

    public float VidaMaximaDoSlider {
        set{
            this.slider.maxValue = value;
        }
    }

    public float VidaAtualDoSlider {
        set{
            this.slider.value = value;
        }
    }

}
