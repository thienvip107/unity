using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bird : MonoBehaviour {

	public float maxStretch = 2.0f;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;

	private SpringJoint2D spring;
	private Transform catapult;
	private Ray rayToMouse;
	private Ray leftCatapultToProjecttile;
	private float maxStreachsqr;
	private float circleRadius;
	private bool clickedOn = true;
	private bool test=false;
	private Vector2 prevVelocity;

	public Rigidbody2D rigidbody2D;

	private AudioSource[] sourceslig;
	public AudioClip slig;
	public AudioClip birdshot;
	public AudioClip brek;

	public GameObject bcd;

	public GameObject selectedSc;

	Animator anim;


	void OnMouseDown(){

		test = true;
		spring.enabled = false;
		clickedOn = true;

		sourceslig[0].Play();

		Camera mcam = GameObject.Find("Main Camera").GetComponent<Camera>();
		mcam.orthographicSize = 8;
        Destroy(selectedSc);
	}

	void OnMouseUp(){
		
		spring.enabled = true;
		rigidbody2D.isKinematic = false;
		clickedOn = false;
		Debug.Log(rigidbody2D.transform.position.x+"  "+ rigidbody2D.transform.position.y);
		sourceslig[1].Play();

		Camera mcam = GameObject.Find("Main Camera").GetComponent<Camera>();
		mcam.orthographicSize = 6;
	}

	void Awake(){

		spring = GetComponent<SpringJoint2D>();
		catapult = spring.connectedBody.transform;
		rigidbody2D = GetComponent<Rigidbody2D> ();
		sourceslig = GetComponents<AudioSource>();
		sourceslig[0].clip = slig;
		sourceslig[1].clip = birdshot;
		sourceslig[2].clip = brek;
		Debug.Log("Awakeeeeee");

	}


	void Start () {
		Debug.Log("Startttt");
		anim = GameObject.Find("1").GetComponent<Animator>();

		LineRenderSetup ();
		rayToMouse = new Ray (catapult.position,Vector3.zero);
		leftCatapultToProjecttile = new Ray (catapultLineFront.transform.position, Vector3.zero);
		maxStreachsqr  = maxStretch * maxStretch;

	    CircleCollider2D collider2D = GetComponent<CircleCollider2D> ();
		CircleCollider2D circle = collider2D;
		circleRadius = circle.radius;
		
	}

	public void setItem (Sprite spt, GameObject seGO)
    {
		SpriteRenderer sp = GetComponent<SpriteRenderer>();
		selectedSc = seGO;
		sp.sprite = spt;
	}
	

	void Update () {
	if (clickedOn && test) {
			Dragging ();
		}
		if (spring != null) {
			if (!rigidbody2D.isKinematic && prevVelocity.sqrMagnitude > rigidbody2D.velocity.sqrMagnitude) {
				Destroy (spring);
				rigidbody2D.velocity = prevVelocity;
			
			}
			if (!clickedOn){
				prevVelocity = rigidbody2D.velocity;
			}
			 LineRenderUpdate();
		}
			else{

			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;

			}
	}


	void LineRenderSetup(){

		catapultLineFront.SetPosition (0,catapultLineFront.transform.position);
		catapultLineBack.SetPosition (0,catapultLineBack.transform.position);

		catapultLineFront.sortingLayerName = "ForeGround";
		catapultLineBack.sortingLayerName = "ForeGround";

		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}



	void Dragging(){

		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > maxStreachsqr) {

			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint(maxStretch);

		}

		mouseWorldPoint.z = 0f;
		transform.position = mouseWorldPoint;
	}

	void LineRenderUpdate(){

		Vector2 catapultToProjecttile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjecttile.direction = catapultToProjecttile;
		Vector3 holdPoint = leftCatapultToProjecttile.GetPoint (catapultToProjecttile.magnitude + circleRadius);
		catapultLineFront.SetPosition (1,holdPoint);
		catapultLineBack.SetPosition(1,holdPoint);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log(rigidbody2D.transform.position.x + "  " + rigidbody2D.transform.position.y);
		if (coll.gameObject.tag == "landcore")
        {
			UIHealthBar.instance.SetValue((float)0.5);
			Destroy(rigidbody2D.gameObject);
		}
		if (coll.gameObject.tag == "boxes"){
			anim.SetTrigger("blast");
			Destroy(coll.gameObject);
		}

		if(coll.gameObject.tag == "friend"){
			sourceslig[2].Play();
			GameObject.Find("Ground_DES").GetComponent<test> ().birdvoid();

			Destroy(coll.gameObject);
		
		}
	}
}
