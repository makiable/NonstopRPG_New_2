using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	int nowPlayerMonsterTotalKill;
	int nowPlayerGetGold;

	string PlayerName;

	void SaveMonsterKill(int nowPlayerMonsterKill){
		int a = PlayerPrefs.GetInt ("TotalKill");
		a += nowPlayerMonsterKill;
		PlayerPrefs.SetInt ("TotalKill", nowPlayerMonsterKill);
	}

	int GetMonsterKillTotalData(){
		int a = PlayerPrefs.GetInt ("TotalKill");

		return a;
	}
	
}
