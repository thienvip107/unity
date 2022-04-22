using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BirdsReload : MonoBehaviour {


	public GameObject Bird;

	public int Birdcount =  0;

	Transform projecttile;
	public Transform farleft;
	public Transform farright;
	

	// Use this for initialization
	void Start () {
		Debug.Log("Startt reloadddd");
		while(Birdcount < 1){
		GameObject birdaction = (GameObject)Instantiate(Bird);
			Birdcount++;
			Bird player = GameObject.Find("Player").GetComponent<Bird>();
			List listG = GameObject.Find("List").GetComponent<List>();
			GameObject fSc = listG.transform.GetChild(0).gameObject;
			player.setItem(fSc.GetComponent<Image>().sprite, fSc);
		}

		}
	
	// Update is called once per frame
	void Update () {

		projecttile = GameObject.Find("Player").GetComponent<Transform>();
		Vector3 newPosition = transform.position;
		
		newPosition.x = projecttile.position.x;
		newPosition.x = Mathf.Clamp(newPosition.x,farleft.position.x,farright.position.x);
		
		transform.position = newPosition;


	}
	
}
