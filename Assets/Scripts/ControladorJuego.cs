using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour {


	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;
	public RawImage fondo;
	public RawImage plataforma;
	public GameObject UiIdle;

	public enum GameState {Idle, Playing};
	public GameState gameState = GameState.Idle;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//empieza juego
		if(gameState == GameState.Idle && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))){
			gameState = GameState.Playing;
			UiIdle.SetActive(false);
			
		}
		//juego en marcha
		else if(gameState == GameState.Playing){
				Parallax();
		}

		
	}

	void Parallax(){
				float finalSpeed = parallaxSpeed * Time.deltaTime;
		        fondo.uvRect = new Rect(fondo.uvRect.x + finalSpeed, 0f, 1f, 1f);
		        plataforma.uvRect = new Rect(plataforma.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
	}
}
