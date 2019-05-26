using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameManager Gm;
	public Text Ammo;
	public Text EndingText;
	public Text TimerText;
	private Time _timer;

	public GameObject EndingPanel;
	GameObject[] _pauseObjects;

	// Use this for initialization
	void Start () {
		SetProperties();
		Time.timeScale = 1;
		_pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		HidePaused();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Gm.IsDead || !AreStillEnemiesAlive() || Gm.Win)
			ShowEndPanel();
		else if (Input.GetKeyDown("r"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				ShowPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				HidePaused();
			}
		}
		
		SetProperties();
	}

	private bool AreStillEnemiesAlive()
	{
		foreach (var enemy in Gm._enemies)
		{
			if (enemy)
				return (true);
		}
		return (false);
	}
	
	public void SetProperties()
	{
		Ammo.text = Gm.PlayerAmmo.ToString();
	}
	
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

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

	public void ShowPaused(){
		foreach(GameObject g in _pauseObjects){
			g.SetActive(true);
		}
	}

	public void HidePaused(){
		foreach(GameObject g in _pauseObjects){
			g.SetActive(false);
		}
	}

	public void ShowEndPanel()
	{
		EndingText.text = Gm.IsDead ? "You Lose !" : "Roxxxor";
		Time.timeScale = 0;
		TimerText.text = Time.timeSinceLevelLoad.ToString();
		EndingPanel.SetActive(true);
	}
	
	public void Exit()
	{
		Application.Quit();
	}
}
