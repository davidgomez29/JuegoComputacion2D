using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
	public GameObject game;
	public GameObject enemyGenerator;

	public AudioClip jumpClip;
	public AudioClip dieClip;
	public AudioClip pointClip;


	public ParticleSystem dust;

    private Animator animator;
    private AudioSource audioPlayer;
    
    
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		audioPlayer = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		bool gamePlaying = game.GetComponent<ControladorJuego>().gameState == GameState.Playing;
		if(gamePlaying && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))){
			UpdateState("PlayerJump");
			audioPlayer.clip = jumpClip;
			audioPlayer.Play();
		}
	}
    
    public void UpdateState(string state = null){
        if(state != null){
            animator.Play(state);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Enemy"){
			UpdateState("PlayerDie");
			//UNA VEZ EL JUGADOR MUERE EL JUEGO DEBE TERMINAR
			game.GetComponent<ControladorJuego>().gameState = GameState.Ended;
			//metodo para que dejen de salir enemigos una vez el jugador pierde
			enemyGenerator.SendMessage("CancelGenerator", true);
			game.SendMessage("ResetTimeScale",0.5f);

			//metodo para parar la musica en caso de  perder
				game.GetComponent<AudioSource>().Stop();
				audioPlayer.clip = dieClip;
				audioPlayer.Play();

				DustStop();
			}else if(other.gameObject.tag == "Point"){
				game.SendMessage("IncreasePoints");
				audioPlayer.clip = pointClip;
				audioPlayer.Play();
			}
		}
			//el juego solo se puede reiniciar hasta que el player haya muerto totalmente
	void GameReady(){
			game.GetComponent<ControladorJuego>().gameState = GameState.Ready;
	}

	//poner en marcha el sistema de particulas
	void DustPlay(){
		dust.Play();
	}
	//parar sistema de particulas
	void DustStop(){
		dust.Stop();

	}
}
