using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameManager Gm;
	public Text Ammo;
	GameObject[] _pauseObjects;

//	public Canvas GameOverTitle;
	
	// Use this for initialization
	void Start () {
//		GameOverTitle.GetComponent<Canvas>();
//		GameOverTitle.enabled = false;
		SetProperties();
		Time.timeScale = 1;
		_pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		HidePaused();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Gm.IsDead)
//			GameOverTitle.enabled = true;
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				ShowPaused();
			} else if (Time.timeScale == 0){
				Debug.Log ("high");
				Time.timeScale = 1;
				HidePaused();
			}
		}
		SetProperties();
	}
	
	public void SetProperties()
	{
		Ammo.text = Gm.PlayerAmmo.ToString();
	}
	
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//		Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void PauseControl(){
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			ShowPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			HidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void ShowPaused(){
		foreach(GameObject g in _pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void HidePaused(){
		foreach(GameObject g in _pauseObjects){
			g.SetActive(false);
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
//	//loads inputted level
//	public void LoadLevel(string level){
//		Application.LoadLevel(level);
//	}
}
