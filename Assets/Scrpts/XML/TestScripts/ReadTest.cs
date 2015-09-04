using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ReadXML ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReadXML(){
		List<RecItem> itemList = ItemIO.Read (Application.dataPath + "/OUTPUT/itemList_Attribute.xml");

		for (int i = 0; i < itemList.Count; i++) {
			RecItem item = itemList[i];
			Debug.Log(string.Format("Item id = [{0}]: ({1},{2},{3})", item.ID, item.Name, item.Level, item.Critical));
		}
	}
}
