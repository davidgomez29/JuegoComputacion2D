using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour {


	public GameObject enemyPrefab;
	public float generatorTimer = 2.25f;
	// Use this for initialization

	//crear enemigos cada 1.75 segundos y el 0f es el retardo
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateEnemy(){
		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
	}

	public void StartGenerator(){
		InvokeRepeating("CreateEnemy",0f,generatorTimer);
	}


	public void CancelGenerator(bool clean = false){
		 CancelInvoke("CreateEnemy");
		 //si nuestro jugador muere automaticamente se borran los enemigos, este es un paso opcional que quise añadir
		 if(clean){
		 	Object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		 	foreach(GameObject enemy in allEnemies){
		 		Destroy(enemy);
		 	}
		 }
	}
}
