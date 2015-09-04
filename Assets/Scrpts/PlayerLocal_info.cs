using UnityEngine;
using System.Collections;

public class PlayerLocal_info : MonoBehaviour {

	int NowHeroLevel;
	int MonsterKillNumber;
    int TotalPlayerGold; // ulong 8byte 0 ~ 18,446,744,073,709,551,615 까지..
	string MyPlayerName;


	void SaveData(){
		PlayerPrefs.SetInt ("HeroLevel", NowHeroLevel);
		PlayerPrefs.SetInt ("MonsterKill", MonsterKillNumber);
		PlayerPrefs.SetInt ("PlayerGold", TotalPlayerGold);
		PlayerPrefs.SetString ("PlayerName", MyPlayerName);
	}

	void GetData(){
		//필요할때 이렇게 키값으로 불러옵니다.
		NowHeroLevel = PlayerPrefs.GetInt ("HeroLevel");
		MonsterKillNumber = PlayerPrefs.GetInt("MonsterKill");
		TotalPlayerGold = PlayerPrefs.GetInt ("PlayerGold");
		MyPlayerName = PlayerPrefs.GetString ("PlayerName", MyPlayerName);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
