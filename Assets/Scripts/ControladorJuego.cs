using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Idle, Playing, Ended, Ready};

public class ControladorJuego : MonoBehaviour {


	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;
	public RawImage fondo;
	public RawImage plataforma;
	//textos
	public GameObject UiIdle;
	public GameObject UiScore;
	public Text pointsText;
	public Text recordText;

	
	public GameState gameState = GameState.Idle;

	public GameObject player;
	public GameObject enemyGenerator;

	public float scaleTime = 6f;
	public float scaleInc = .25f;
//instanciamos el recurso del audio
	private AudioSource musicPlayer;
	private int points = 0;

	// Use this for initialization
	void Start () {
		musicPlayer = GetComponent<AudioSource>();
		recordText.text = "BEST: "+ GetMaxScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {

		bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

		//empieza juego gracias a click o tecla arriba
		if(gameState == GameState.Idle && userAction){
			gameState = GameState.Playing;
			//desaparece texto nombre juego
			UiIdle.SetActive(false);
			//aparece puntaje juego
			UiScore.SetActive(true);
			//envia animacion al jugador para que corra
			player.SendMessage("UpdateState", "PlayerRun");
			//inicializa efecto de piedras en el jugador
			player.SendMessage("DustPlay");
			enemyGenerator.SendMessage("StartGenerator");
			//el audio comienza cuando presionamos click o tecla arriba
			musicPlayer.Play();
			//llamado a metodo que a los 6 segundos de empezar el juego inicia a aumentar la escala y vuelve a aumentar hasta los otros 6 segundos
			InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
			
		}
		//juego en marcha
		else if(gameState == GameState.Playing){
				Parallax();
		}
		//juego preparado para reiniciarse
		else if(gameState == GameState.Ready){
				if(userAction){
					RestartGame();
				}
		}
		
	}

	void Parallax(){
				float finalSpeed = parallaxSpeed * Time.deltaTime;
		        fondo.uvRect = new Rect(fondo.uvRect.x + finalSpeed, 0f, 1f, 1f);
		        plataforma.uvRect = new Rect(plataforma.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
	}

	public void RestartGame(){
		//resetea tiempo de escala de caida al normal
		ResetTimeScale();
		SceneManager.LoadScene("Principal");
	}
//aumenta dificultad del juego
	void GameTimeScale(){
		Time.timeScale += scaleInc;
		Debug.Log("Ritmo incrementado: "+Time.timeScale.ToString());
	}
//resetear la escala de dificultad a facil una vez se reinicia la escena xd
	public void ResetTimeScale(float newTimeScale = 1f){
		CancelInvoke("GameTimeScale");
		Time.timeScale =newTimeScale;
		Debug.Log("Ritmo Reestablecido: "+Time.timeScale.ToString());
	}
//asignamos marcador cada vez que incrementamos un punto
	public void IncreasePoints(){
		pointsText.text = (++points).ToString();
		//actualizar record maximo
		if(points >= GetMaxScore()){
			recordText.text = "BEST: " + points.ToString();
			SaveScore(points);
		}
	}
//OBTENER PUNTAJE MAS ALTO
	public int GetMaxScore(){
		return PlayerPrefs.GetInt("Max Points",0);
	}
//GUARDAR UN PUNTAJE MAS ALTO
	public void SaveScore(int currentPoints){
		PlayerPrefs.SetInt("Max Points", currentPoints);
	}
}
