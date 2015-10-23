using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	
	[HideInInspector] public bool RightFace = true;
	[HideInInspector] public bool jump = false;
	public float movingForce = 350f;
	public float maximumSpeed = 7f;
	public float jumpForce = 1000f;
	//public Transform groundCheck;
	
	
	private bool grounded = false;
	private Animator animate;
	private Rigidbody2D rigid;
	
	
	// Use this for initialization
	void Awake () 
	{
		animate = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	/*void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}*/
	
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		
		animate.SetFloat("Speed", Mathf.Abs(h));
		
		if (h * rigid.velocity.x < maximumSpeed)
			rigid.AddForce(Vector2.right * h * movingForce);
		
		if (Mathf.Abs (rigid.velocity.x) > maximumSpeed)
			rigid.velocity = new Vector2(Mathf.Sign (rigid.velocity.x) * maximumSpeed, rigid.velocity.y);
		
		if (h > 0 && !RightFace)
			Flip ();
		else if (h < 0 && RightFace)
			Flip ();
		
		if (jump)
		{
			animate.SetTrigger("Jump");
			rigid.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
	
	
	void Flip()
	{
		RightFace = !RightFace;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
