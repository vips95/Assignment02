/* Name : Vipul Arora
 * Last Modified by : Vipul Arora
 * Date Last Modified: 26th October 2015
 * Description: It helps the player to earn score and it keeps the record of lives and score
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour {
	
	//PUBLIC INSTANCE VARIABLES
	public Text scoreLabel;
	public Text livesLabel;
	public Text gameOverLabel;
	public Text finalScoreLabel;
	public int  scoreValue = 0;
	public int  livesValue = 3;
	

	
	
	// Use this for initialization
	void Start () {

		this._SetScore ();
		this.gameOverLabel.enabled = false;
		this.finalScoreLabel.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D otherGameObject) {
		if (otherGameObject.tag == "Coin") {
			 // play coin sound
			this.scoreValue += 50; // add 50 points
		}
		if (otherGameObject.tag == "Enemy") {
			 // play thunder sound
			this.livesValue--; // remove one life
			if(this.livesValue <= 0) {
				this._EndGame();
			}
		}
		
		
		this._SetScore ();
	}
	
	
	// PRIVATE METHODS
	private void _SetScore() {
		this.scoreLabel.text = "Score: " + this.scoreValue;
		this.livesLabel.text = "Lives: " + this.livesValue;
	}
	
	private void _EndGame() {
		Destroy(gameObject);
		this.scoreLabel.enabled = false;
		this.livesLabel.enabled = false;
		this.gameOverLabel.enabled = true;
		this.finalScoreLabel.enabled = true;
		this.finalScoreLabel.text = "Score: " + this.scoreValue;
		
	}
	
}
