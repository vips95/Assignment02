/* Name : Vipul Arora
 * Last Modified by : Vipul Arora
 * Date Last Modified: 26th October 2015
 * Description: It helps the player to collect the coin
 */
using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Hero")) {
			Destroy(gameObject);
		}
	}
}
