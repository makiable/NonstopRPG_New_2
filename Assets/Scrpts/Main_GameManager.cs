using UnityEngine;
using System.Collections;

public class Main_GameManager : MonoBehaviour {

	public TextMesh gold;

	// Use this for initialization
	void Start () {

		string a = PlayerPrefs.GetInt ("PlayerTotalGold").ToString ();

		gold.text = a+"G";

		Debug.Log("Player Has "+PlayerPrefs.GetInt("PlayerTotalGold")+" Gold");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Use this for initialization
	void OnButtonDown(string trigger){


		if (trigger == "GameStart") {
			//mCameraControl.SetStatus(CameraControl.Status.Start);
			Invoke("StartButton",0.5f);
		}

		if (trigger == "EnterDungeun0001") {
			PlayerPrefs.SetInt ("SelectStage", 1 );
			Application.LoadLevel("DungeunScene01");
		}

		if (trigger == "EnterDungeun0002") {
			PlayerPrefs.SetInt ("SelectStage", 2 );
			Application.LoadLevel("DungeunScene01");
		}

		if (trigger == "EnterDungeun0003") {
			PlayerPrefs.SetInt ("SelectStage", 3 );
			Application.LoadLevel("DungeunScene01");
		}
		if (trigger == "MoneyClear") {
			PlayerPrefs.SetInt ("PlayerTotalGold", 0);
			Debug.Log("Player Has "+PlayerPrefs.GetInt("PlayerTotalGold")+" Gold");
		}


	}
	
	void StartButton(){
		Application.LoadLevel("Menu_default_Scene");
	}

}
