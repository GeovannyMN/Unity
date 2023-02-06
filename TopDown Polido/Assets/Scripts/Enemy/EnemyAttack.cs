using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public EstadosData state;

    [SerializeField]
    private float danoDoInimigo = 0.7f;

   private void Awake () {
	animator = GetComponent<Animator>();
	
}
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
		state = EstadosData.Attack;
            animator.SetInteger("estado", (int)state);
            PlayerLife player = other.GetComponent<PlayerLife>();
            
            if (!player.invulneravel){
                soundFX.playSound(sound.ATTACK_GREEN);
                player.ReceberDano(danoDoInimigo);
            } 
        }
    }
	private void OnTriggerExit2D(Collider2D other) {
		state = EstadosData.Idle;
            animator.SetInteger("estado", (int)state);
		
}
}
