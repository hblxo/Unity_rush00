using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
	public GameManager Gm;

	public Sprite FullAmmoSprite;
	public Sprite EmptyAmmoSprite;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Gm.PlayerAmmo > 0)
			GetComponent<SpriteRenderer>().sprite = FullAmmoSprite;
		else
			GetComponent<SpriteRenderer>().sprite = EmptyAmmoSprite;
	}

}
