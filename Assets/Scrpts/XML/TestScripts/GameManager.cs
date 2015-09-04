using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO; 
using System;

public class GameManager : MonoBehaviour {

	public string _filename = "xml_Test_01";

	// Use this for initialization
	void Start () {
		Xml_Load (_filename);
		UpdateXml ();

		//Xml_Load (_filename);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//읽어오기..
	void Xml_Load(string filename){

		string filepath = Application.dataPath+"/Resources/xml_Test_01.xml";

		if (File.Exists (filepath)) {
			Debug.Log ("file exist");

			TextAsset textAsset = (TextAsset)Resources.Load (filename);
			XmlDocument xmldoc = new XmlDocument ();
			xmldoc.LoadXml (textAsset.text);
			//xml 생성.

			//각 요소 별로 가져오기.
			XmlNodeList KillCount = xmldoc.GetElementsByTagName ("MONSTERKILLCOUNT");
			int killcount = int.Parse (KillCount [0].InnerText);
			int killcount2 = int.Parse (KillCount [1].InnerText);
			Debug.Log ("1=" + killcount + ", 2=" + killcount2);


			//각 요소 별로 가져오기.
			XmlNodeList name = xmldoc.GetElementsByTagName ("NAME");
			string name01 = name [0].InnerText;
			string name02 = name [1].InnerText;
			Debug.Log ("name01 = " + name01 + "name02 = " + name02);


			//전체 가져오기..
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");

			//string s = "";
			foreach (XmlNode node in nodes) {
				Debug.Log ("ID: " + node.SelectSingleNode ("ID").InnerText);
				Debug.Log ("NAME: " + node.SelectSingleNode ("NAME").InnerText);
				Debug.Log ("LEVEL: " + node.SelectSingleNode ("LEVEL").InnerText);
				Debug.Log ("GOLD: " + node.SelectSingleNode ("GOLD").InnerText);
				Debug.Log ("MONSTERKILLCOUNT: " + node.SelectSingleNode ("MONSTERKILLCOUNT").InnerText);

			}
		} else
			Debug.Log ("file not exist");

	}

	public void UpdateXml()
	{

		string filepath = Application.dataPath+"/Resources/xml_Test_01.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			
			TextAsset textAsset = (TextAsset)Resources.Load (_filename);
			//xmlDoc.Load(filepath); 이렇게 해도 된다..

			//임시 사용할 파일 을 새로 생성.
			XmlDocument xmldoc = new XmlDocument ();
			xmldoc.LoadXml (textAsset.text);
			//xml 생성.

			int a = 3;
			if (a == 1) {
				//노드를 이용해서 세이브..전체 가져오기..
				XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
				foreach (XmlNode node in nodes) { //이건 노드로 접근해서 하는 방법. 고속 열거..
					
					node.SelectSingleNode("ID").InnerText = "3";
					node.SelectSingleNode("NAME").InnerText = "3";
					node.SelectSingleNode("LEVEL").InnerText = "3";
					node.SelectSingleNode("GOLD").InnerText = "3";
					node.SelectSingleNode("MONSTERKILLCOUNT").InnerText = "3";		
				}
			}
			else if ( a == 2) {
				//여기에서는 엘리맨드로 접근 하는 벙법을 만들어 보자..
				//각 요소 별로 가져오기.

				XmlNodeList name = xmldoc.GetElementsByTagName ("NAME"); //이름항목들을 배열로 만들어서 가져오서 사용하다.
				name [0].InnerText = "DQ";
				name [1].InnerText = "OB";
				Debug.Log ("name01 = " + name[0] + "name02 = " + name[1]);
			}

			else if (a == 3) {
				//하나의 요소에 접근해서 한개만 바꾸기..킬카운트에 접근해서. 킬 카운트 수를 놀리기.
				XmlNodeList killCount = xmldoc.GetElementsByTagName ("MONSTERKILLCOUNT");
				killCount[0].InnerText = "1000";
			}

			//전체 가져오기..
			XmlNodeList nodes1 = xmldoc.SelectNodes ("dataroot/Node");
			
			//string s = "";
			foreach (XmlNode node in nodes1) {

				Debug.Log ("업데이트 후");
				Debug.Log ("ID: " + node.SelectSingleNode ("ID").InnerText);
				Debug.Log ("NAME: " + node.SelectSingleNode ("NAME").InnerText);
				Debug.Log ("LEVEL: " + node.SelectSingleNode ("LEVEL").InnerText);
				Debug.Log ("GOLD: " + node.SelectSingleNode ("GOLD").InnerText);
				Debug.Log ("MONSTERKILLCOUNT: " + node.SelectSingleNode ("MONSTERKILLCOUNT").InnerText);
				
			}

			xmldoc.Save(filepath);
			Debug.Log("UpdateXml OK!");
		}
		
	}




}






















