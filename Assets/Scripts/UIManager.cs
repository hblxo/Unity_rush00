using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameManager Gm;
	public Text Ammo;
//	public Canvas GameOverTitle;
	
	// Use this for initialization
	void Start () {
//		GameOverTitle.GetComponent<Canvas>();
//		GameOverTitle.enabled = false;
		SetProperties();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Gm.IsDead)
//			GameOverTitle.enabled = true;
		SetProperties();
	}
	
	public void SetProperties()
	{
		Ammo.text = Gm.PlayerAmmo.ToString();
	}
}
