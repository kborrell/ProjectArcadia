using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraVision : MonoBehaviour {

	RectTransform m_visibleArea;
	// Use this for initialization
	void Start () {
		m_visibleArea = this.GetComponent<RectTransform> ();

		//remove update code
		Debug.LogError ("DEBUG CODE - CAMERA VISION REMOVE BEFORE SUBMIT");
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.KeypadPlus)) 
		{
			setSize (m_visibleArea.sizeDelta.x + 1);
		} 
		else if (Input.GetKeyDown (KeyCode.KeypadMinus)) 
		{
			setSize (m_visibleArea.sizeDelta.x - 1);
		}
			
	}

	public void setSize(float size)
	{
		m_visibleArea.sizeDelta = new Vector2 (size, size);
	}
}
