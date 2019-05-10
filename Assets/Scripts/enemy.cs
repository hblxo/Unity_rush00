using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	public float Speed = 1f;
	private Vector3 _playerPos;
	private Animator _animator;
	private Vector3 _target;
	private bool _isMoving;
	private int _layerMask;
	
	// Use this for initialization
	void Start ()
	{
		_layerMask = 1 << 2;
		_layerMask = ~_layerMask;
		_playerPos = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (_isMoving)
			transform.position = Vector3.MoveTowards(transform.position, _playerPos, Speed * Time.deltaTime);
		if (transform.position == _playerPos)
		{
			_isMoving = false;
		}
	}

	private void OnTriggerStay2D(Collider2D charac)
	{
		if (!charac.gameObject.CompareTag("Player")) return;
		var hit = Physics2D.Linecast(transform.position, charac.transform.position, _layerMask);
		Debug.DrawRay(transform.position,
			hit.point - (Vector2)transform.position, Color.yellow);
		if (hit.rigidbody.gameObject.CompareTag("Player"))
		{
			Move(charac.gameObject);
		}
	}
		
	public void Move(GameObject player)
	{
		_playerPos = player.transform.position;
		_playerPos.z = transform.position.z;
		_target = player.transform.position;
		_target.z = 0;

		Vector3 objectPos = transform.position;
		_target.x = _target.x - objectPos.x;
		_target.y = _target.y - objectPos.y;
		float angle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
		angle += 90;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		_isMoving = true;
	}
}
