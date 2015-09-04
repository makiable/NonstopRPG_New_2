using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WriteTest : MonoBehaviour {
	// Use this for initialization
	void Start () {

		List<RecItem> itemList = new List<RecItem> ();

		for (int i = 0; i < 100; i++) {

			RecItem item = new RecItem();

			item.ID = i;
			item.Name = "아이템";
			item.Level = 1;
			item.Critical = Random.Range(0.1f, 1.0f);
			itemList.Add(item);
		}
		ItemIO.Write(itemList,Application.dataPath+"/OUTPUT/itemList_Attribute.xml");

		Debug.Log ("Write Test ok");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
