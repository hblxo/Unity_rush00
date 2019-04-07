using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Gm;
	public int PlayerAmmo;
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
		PlayerAmmo = 0;
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
		_weapon = Player.GetComponent<Weapon>();
		PlayerAmmo = _weapon ? _weapon.Ammo : 0;
	}

	public void GameOver()
	{
		IsDead = true;
//		Time.timeScale = 0;
	}
}
