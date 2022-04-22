
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class test : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern string GetJWTToken();

	public GameObject Bird;
	public int birdcount = 2;
	public int curbird;

	public int friendbird ;

	// Use this for initialization
	void Start ()
	{
		friendbird = 0;
		curbird = 0;
		StartCoroutine(CallGetRequest("https://game-api.dev.sotadx.com/mini-game/get-list-season?game_id=621cf747494040d139f5c4c0", delegate (bool result, string respone)
		{
			if (result)
			{
				Debug.Log(result);
			}
		}));
	}

	public SessionInfo Session { get; private set; }
	IEnumerator CallGetRequest(string uri, Action<bool, string> callback, Dictionary<string, string> header = null, Dictionary<string, object> form = null)
    {
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			Debug.Log("call requesttt");

			webRequest.method = "GET";
			if (Session != null)
			{
				webRequest.SetRequestHeader("Authorization", $"Bearer {Session.token}");
			}
			if (header != null)
				foreach (var key in header.Keys)
				{
					webRequest.SetRequestHeader(key, header[key]);
				}
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();
			string mess = webRequest.downloadHandler.text;
			switch (webRequest.result)
			{
				case UnityWebRequest.Result.DataProcessingError:
					Debug.Log(mess);
					break;
				case UnityWebRequest.Result.ProtocolError:
					Debug.Log(mess);
					break;
				case UnityWebRequest.Result.Success:
					callback?.Invoke(true, mess);
					break;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(curbird == 1){
			GameObject.Find("1").GetComponent<Image>().enabled = false;
		}
		if(curbird == 2){
			GameObject.Find("2").GetComponent<Image>().enabled = false;
		}

		if(curbird > 2){
			StartCoroutine(loadSc());
		}
		
		if(friendbird > 2){
			
			StartCoroutine(loadnew());
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if (curbird <= birdcount) {
			Destroy (other.gameObject,8);
			StartCoroutine(birdload());
			++curbird;


		} 



	}

	IEnumerator birdload(){
		yield return new WaitForSeconds(5.0f);
		GameObject birdaction = (GameObject)Instantiate (Bird);

	}

	public void birdvoid(){

		friendbird = friendbird + 1;
		Debug.Log(friendbird);

	}

	IEnumerator loadSc(){

		yield return new WaitForSeconds(5);
	Application.LoadLevel ("failed");
		Debug.Log("Level.Fail");
	}

	IEnumerator loadnew(){
		
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("goto2");
		Debug.Log("Level.done");
	}
}

[Serializable]
public class SessionInfo
{
	public string token { get; set; }
}