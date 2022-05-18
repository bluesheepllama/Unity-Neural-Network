using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NeuralNetwork;

public class Player : MonoBehaviour {
	public GameObject currentGroundTile;
	public GameObject restartText;
	public UIPauseMenu uiPauseMenu;
	//This is the distance between the player and the end of the current staying tile / platform
	public double distanceInPercent;
	public float speed;
	public int jumpHeight = 400;
	public double canJump;

	private const double timeBetweenDatasets = 0.3; 
	float countedTime = 0;

	public GameObject explosion;

	public List<GameObject> enableList;
	public NetLayer net;

	private bool die = false;
	private float deathTimer = 0;
	public float timeToDie = 5;
	public bool isPlayer = false;

	public void Start(){
		canJump = 1; 
		foreach(GameObject go in enableList)
        {
			go.SetActive(true);
        }
	}



	// Update is called once per frame
	void Update () {
		countedTime += Time.deltaTime;
		deathTimer += Time.deltaTime;
		//Move the parent (Camera + player)
		this.transform.parent.position += Vector3.right*3F * Time.deltaTime *speed; 
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(isPlayer || !NetLayer.trained)
				jump (); 
		} else {
			//Adding a new dataset
			if (countedTime >  timeBetweenDatasets && !NetLayer.trained) {
				countedTime = 0; 
				net.Train (canJump, 0);
			}
		}

		//Calculate the distance from player to the end of the current triggered platform in percent
		Vector3 startPointTile = currentGroundTile.transform.position - (Vector3.right * currentGroundTile.transform.localScale.x) / 2; 
		Vector3 endPointTile = currentGroundTile.transform.position + (Vector3.right * currentGroundTile.transform.localScale.x) / 2; 
		Vector3 platformLength = endPointTile - startPointTile; 
		Vector3 distanceToEndOfPlatform = endPointTile - transform.position;
		distanceInPercent = distanceToEndOfPlatform.x / platformLength.x;
		//TODO: check for obstacles and make that distnce distanceInPercent

		//Just for visualization draw a line to the end of the platform
		this.gameObject.GetComponent<LineRenderer>().SetPositions (new Vector3[]{ transform.position, endPointTile }); 

		if (distanceToEndOfPlatform.x < 0) {
			distanceToEndOfPlatform = Vector3.zero; 
		}

		checkForGameOver (); 
	}

	//Cube will trigger a platform and can jump again, also we also to transmit the currently triggered platform to the network
	public void OnTriggerEnter(Collider other){
		canJump = 1; 
		currentGroundTile = other.gameObject;
		deathTimer = 0;

	}

    public void OnTriggerStay(Collider other)
    {
		deathTimer = 0;

	}

	private void checkForGameOver(){
		//The player is basically game over
		//if (transform.position.y < -24) {
		if (deathTimer >= timeToDie)
		{
			restartText.SetActive(true);
			//Time.timeScale = 0;
			uiPauseMenu.Die();
		}
	}

	//Perform a jump
	public void jump(){
		if (canJump == 1) {
			//Send dataset to the net 
			if(!isPlayer) { 
				GameObject.Find("Network").GetComponent<NetLayer>().Train(1, 1);
				}
			GameObject exp = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y-1.5f, transform.position.z), Quaternion.identity);
			Destroy(exp,1);
			//Jump
			this.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up* jumpHeight); 
			canJump = 0; 
		}
	}
}


// define classes, different heights,
//jump over based off height