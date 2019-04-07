using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Text txt;
	string story;

	void Awake () 
	{
		transform.Rotate(Vector3.forward,-12);
//		txt = GetComponent<Text> ();
//		story = txt.text;
//		txt.text = "";
//		Random.Range()
		// TODO: add optional delay when to start

		StartCoroutine("PlayText");
	}

	IEnumerator PlayText()
	{

		while (true)
		{
			for (int i = 0; i < 6; i++)
			{
				transform.Rotate(Vector3.forward,3);
				transform.position += Vector3.down * 0.06f;
				yield return new WaitForSeconds (0.12f);
			}
			for (int i = 0; i < 6; i++)
			{
				transform.Rotate(Vector3.forward,-3);
				transform.position += Vector3.up * 0.06f;
				yield return new WaitForSeconds (0.12f);
			}
			
//			txt.text = "";
//			foreach (char c in story) 
//			{
//				txt.text += c;
//				yield return new WaitForSeconds (0.125f);
//			}
//		yield return new WaitForSeconds(0f);
		}
	}
	
}
