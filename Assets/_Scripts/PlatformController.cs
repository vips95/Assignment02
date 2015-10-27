using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed;
	
	// Use this for initialization
	void Start () {
		this._Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = gameObject.GetComponent<Transform> ().position;
		currentPosition.x -= speed;
		gameObject.GetComponent<Transform> ().position = currentPosition;
		
		// Check bottom boundary
		if (currentPosition.x <= -380) {
			this._Reset();
		}
	}
	
	private void _Reset() {
		Vector2 resetPosition = new Vector2 (-20f,0.0f );
		gameObject.GetComponent<Transform> ().position = resetPosition;
	}
}
