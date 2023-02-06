using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField]
    private Slider slider; 
	
    private void awake () 
{
	
	slider.gameObject.SetActive(false);
}
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

   private void OnTriggerEnter2D(Collider2D other)
{
	if (other.tag == "Player")
{
	slider.gameObject.SetActive(true);
}
}

}
