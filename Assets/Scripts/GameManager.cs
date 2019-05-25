using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Gm;
	[HideInInspector]public int PlayerAmmo;
	private Weapon _weapon;
	public GameObject Player;
	public bool IsDead = false;
	
	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (Gm == null)
			Gm = this;
	}
	
	// Use this for initialization
	void Start ()
	{
//		PlayerAmmo = 0;
		_weapon = Player.GetComponent<PlayerScript>()._weapon;
		PlayerAmmo = _weapon ? _weapon.Ammo : 0;
		Debug.Log("AMMO :" + PlayerAmmo);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Gm.IsDead)
			GameOver();
		UpdateAmmo();
	}	

	private void UpdateAmmo()
	{
		_weapon = Player.GetComponent<PlayerScript>()._weapon;
		PlayerAmmo = _weapon ? _weapon.Ammo : 0;
	}

	public void GameOver()
	{
		IsDead = true;
		Time.timeScale = 0;

		if (Input.GetKeyDown("r"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
