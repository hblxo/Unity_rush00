using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

//	public Text TheText;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GetComponent<Text>().color = new Color(0.3f, 0.88f, 0.76f); //Or however you do your color
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponent<Text>().color = new Color(0.97f, 0.29f, 0.69f); //Or however you do your color
	}
}
