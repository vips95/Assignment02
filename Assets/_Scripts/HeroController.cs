using UnityEngine;
using System.Collections;

// VELOCITYRANGE UTILITY CLASS
[System.Serializable]
public class VelocityRange {
	// PUBLIC INSTANC VARIABLES
	public float vMin, vMax;
	
	// CONSTRUCTOR +++++++++++++++++++++++++++++++
	public VelocityRange(float vMin, float vMax) {
		this.vMin = vMin;
		this.vMax = vMax;
	}
}

// PLAYERCONTROLLER CLASS
public class HeroController : MonoBehaviour {
	//PUBLIC INSTANCE VARIABLES +++++++++++++++++++++++++++++++
	public float speed = 50f;
	public float jump = 500f;
	
	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);
	
	
	//PRIVATE INSTANCE VARIABLES ++++++++++++++++++++++++++++++
	private Rigidbody2D rigid;
	private Transform trans;
	private Animator animate; // we'll use this later
	private AudioSource[] audio;
	private AudioSource coinaudio;
	private AudioSource jumpaudio;
	
	private float moveValue = 0;
	private bool facingright = true;
	private bool ground = true;
	
	// Use this for initialization
	void Start () {
		this.rigid = gameObject.GetComponent<Rigidbody2D> ();
		this.trans = gameObject.GetComponent<Transform> ();
		this.animate = gameObject.GetComponent<Animator> ();
		
		this.audio = gameObject.GetComponents<AudioSource> ();
		this.coinaudio = this.audio [0];
		this.jumpaudio = this.audio [1];
	}
	
	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;
		
		float absVelX = Mathf.Abs (this.rigid.velocity.x);
		float absVelY = Mathf.Abs (this.rigid.velocity.y);
		
		this.moveValue = Input.GetAxis ("Horizontal"); // value is between -1 and 1
		
		// check if player is moving
		
		if (this.moveValue != 0) {
			// we're moving
			this.animate.SetInteger("AnimState", 1); // play walk clip
			if(this.moveValue > 0) {
				// moving right
				if(absVelX < this.velocityRange.vMax) {
					forceX = this.speed;
					this.facingright = true;
					this._flip();
				}
			} else if (this.moveValue < 0) {
				// moving left
				if(absVelX < this.velocityRange.vMax) {
					forceX = -this.speed;
					this.facingright = false;
					this._flip();
				}
			}
		} else if (this.moveValue == 0) {
			// we're idle
			this.animate.SetInteger("AnimState", 0);
		}
		
		// Check if player is jumping
		
		if((Input.GetKey("up") || Input.GetKey(KeyCode.W))) {
			// check if the player is grounded
			if(this.ground) {
				// player is jumping
				this.animate.SetInteger("AnimState", 2);
				if(absVelY < this.velocityRange.vMax) {
					forceY = this.jump;
					this.jumpaudio.Play();
					this.ground = false;
				}
			}
			
		}
		
		this.rigid.AddForce (new Vector2 (forceX, forceY));
	}
	
	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Coin")) {
			this.coinaudio.Play();
		}
	}
	
	void OnCollisionStay2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Platform")) {
			this.ground = true;
		}
	}
	
	
	// PRIVATE METHODS +++++++++++++++++++++++++++++++++++++++
	private void _flip() {
		if (this.facingright) {
			this.trans.localScale = new Vector3(1f, 1f, 1f); // flip back to normal
		} else {
			this.trans.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
	
}
