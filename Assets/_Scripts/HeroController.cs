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
	private Rigidbody2D _rigidBody2D;
	private Transform _transform;
	private Animator _animator; // we'll use this later
	private AudioSource[] _audioSources;
	private AudioSource _coinSound;
	private AudioSource _jumpSound;
	
	private float _movingValue = 0;
	private bool rightface = true;
	private bool ground = true;
	
	// Use this for initialization
	void Start () {
		this._rigidBody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();
		
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._coinSound = this._audioSources [0];
		this._jumpSound = this._audioSources [1];
	}
	
	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;
		
		float absVelX = Mathf.Abs (this._rigidBody2D.velocity.x);
		float absVelY = Mathf.Abs (this._rigidBody2D.velocity.y);
		
		this._movingValue = Input.GetAxis ("Horizontal"); // value is between -1 and 1
		
		if (this._movingValue != 0) {

			// we're moving
			if(this._movingValue > 0) {
				// moving right
				if(absVelX < this.velocityRange.vMax) {
					forceX = this.speed;
					this.rightface = true;
					this.turn ();
				}
			} else if (this._movingValue < 0) {
				//moving left
				if(absVelX < this.velocityRange.vMax) {
					forceX = -this.speed;
					this.rightface = false;
					this.turn ();
				}
			}

		} else if (this._movingValue == 0) {
			// we're idle
			this._animator.SetInteger("AnimState",0); // player walk and idle 
		}
		//Check if player is jumping
		if ((Input.GetKey ("up") || Input.GetKey (KeyCode.Space))) { //if player grounded
			if (this.ground) {     //player is jumping
				 
				if (absVelY < this.velocityRange.vMax) {
					this._animator.SetInteger("AnimState",1);
					forceY = this.jump;
					this._jumpSound.Play();
					this.ground = false;
				}
			}


		}

		
		this._rigidBody2D.AddForce (new Vector2 (forceX, forceY));
	}
	
	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Coin")) {
			this._coinSound.Play();
		}
	}
	void OnCollisionStay2D(Collision2D otherCollider) {
				if (otherCollider.gameObject.CompareTag ("Platform")) {
						this.ground = true;
				}
	}
	//Private methods

	private void turn()
	{
		/*rightface = !rightface;
		Vector3 theScale = _transform.localScale;
		theScale.x *= -1;
		_transform.localScale = theScale; */
		if (this.rightface) {
			this._transform.localScale = new Vector3(.5f,.5f,.5f); // turn to normal
		} else {
			this._transform.localScale = new Vector3(-.5f,.5f,.5f);
		}
	}
}