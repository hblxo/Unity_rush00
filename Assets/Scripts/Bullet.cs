using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float Speed;
	public float StartTime;
	public bool Enemy;
	public Vector3 Target;
	public Vector3 Direction;
	public float Lifespan;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		if (this.StartTime + this.Lifespan < Time.time)
		{
			Destroy(gameObject);
		}
		transform.position += this.Direction * this.Speed;
	}
}
