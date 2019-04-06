using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	public GameObject Projectile;
	public float FireRate;
	public float Range;
	public float BulletSpeed;
	public int Ammo;
	public float NextShot;
	
	// Use this for initialization
	void Start ()
	{
		NextShot = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Shoot(Transform source, Vector3 target)
	{
		if (Time.time > NextShot)
		{
			var clone = Instantiate(Projectile, transform.position, transform.rotation);
			clone.GetComponent<Bullet>().Target = target;
			NextShot = Time.time + FireRate;
		}
		Debug.Log("NextShot, Time " + NextShot + " " + Time.time);
	}
}
