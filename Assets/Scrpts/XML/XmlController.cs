using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO; 
using System;


public class XmlController : MonoBehaviour {

	In_GameManager mIn_GameManager;


	//몬스터 스폰 데이터를 가져오는 임시 xml만들기.
	public string StageMonsterInfoXML = "Monster_Respwan_Data";
	XmlDocument mXml = new XmlDocument(); //각 데이터를 저장할 임시 xml 파일.
	XmlDocument TempMonsterXml = new XmlDocument(); //해당 값만 소환할 xml파일


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Monster_Xml_Load(string filename){
		
		string filepath = Application.dataPath+"/Resources/Monster_Respwan_Data.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			
			TextAsset textAsset = (TextAsset)Resources.Load (filename);
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 
			
			//전체 가져오기..
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			Debug.Log("Read Test Result");

			//이걸 For 문으로 만들어야 될텐데...일단 넘어가자.
			foreach (XmlNode node in nodes) { //임시파일 전체 돌리기.
				//Debug.Log ("A :"+node.SelectSingleNode("A").InnerText);
				Debug.Log ("ID: " + node.SelectSingleNode ("ID").InnerText);
				Debug.Log ("STAGE: " + node.SelectSingleNode ("STAGE").InnerText);
				Debug.Log ("CHEPTER: " + node.SelectSingleNode ("CHEPTER").InnerText);
				Debug.Log ("PART: " + node.SelectSingleNode ("PART").InnerText);
				Debug.Log ("MONSTER_PREFAB_NAME: " + node.SelectSingleNode ("MONSTER_PREFAB_NAME").InnerText);
				Debug.Log ("MONSTER_WEAPON_PREFAB :"+node.SelectSingleNode("MONSTER_WEAPON_PREFAB").InnerText);
				Debug.Log ("MONSTER_SPWAN_NUMBER :"+node.SelectSingleNode("MONSTER_SPWAN_NUMBER").InnerText);
				Debug.Log ("MONSTER_HP :"+node.SelectSingleNode("MONSTER_HP").InnerText);
				Debug.Log ("MONSTER_MP :"+node.SelectSingleNode("MONSTER_MP").InnerText);
				Debug.Log ("MONSTER_POWER :"+node.SelectSingleNode("MONSTER_POWER").InnerText);
				Debug.Log ("MONSTER_ATTACK_SPEED :"+node.SelectSingleNode("MONSTER_ATTACK_SPEED").InnerText);
				Debug.Log ("DROP_GOLD :"+node.SelectSingleNode("DROP_GOLD").InnerText);
				Debug.Log ("DROP_ITEM01 :"+node.SelectSingleNode("DROP_ITEM01").InnerText);
				Debug.Log ("DROP_ITEM02 :"+node.SelectSingleNode("DROP_ITEM02").InnerText);
				Debug.Log ("MONSTERKILLCOUNT :"+node.SelectSingleNode("MONSTERKILLCOUNT").InnerText);

			}
		} else
			Debug.Log ("file not exist");
		
	}
	public void Test_Monster_ID_Xml_Load(string filename, int ID_number){
		
		string filepath = Application.dataPath+"/Resources/Monster_Respwan_Data.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			
			TextAsset textAsset = (TextAsset)Resources.Load (filename);
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 
			
			//전체 가져오기..
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");

			//확인
			Debug.Log("Read Test Result");
			Debug.Log("Send ID :"+ID_number+"NodeResult-firstChild.InnerText: "+nodes[ID_number].SelectSingleNode ("ID").InnerText);
			Debug.Log("Send ID :"+ID_number+"NodeResult-MonserPrefab : "+nodes[ID_number].SelectSingleNode ("MONSTER_PREFAB_NAME").InnerText);

			//루트 엘리먼트 참조.
			XmlElement root = xmldoc.DocumentElement;
			//Debug.Log("xmldoc.Name :"+xmldoc.Name); //Result "#document"
			//Debug.Log("Root.Name :"+root.Name); //Result "dataroot"

			//몇 번째 노드의 전체 정보를 가져올 것인가?
			XmlNode FristChileNode = root.ChildNodes[0]; //i에 해당하는 노드를 참조.
			Debug.Log("FristChileNode.InnerText :"+FristChileNode.InnerText); //FristChileNode.InnerText : 0111Monster00monsterHarm01110050301.5201510

			XmlElement FirstChildElement = (XmlElement) root.ChildNodes[0];  //위에 꺼랑 같은 정보 노출.
			Debug.Log("FirstChildElement.InnerText : "+FirstChildElement.InnerText); //i노드의  모든 정보 출력. Result : 0111Monster00MonserHarm011.....

				//선택한 노드의 몇번째 값을 가져올 것 인가?
				XmlElement ElementText = (XmlElement) FirstChildElement.ChildNodes[5]; //i 노드의 n번째 값을 읽어옴. (i의 n번째 innerText는??)
				Debug.Log("name.innerText: "+ElementText.InnerText); //ElementText.innerText: monsterHarm01

				XmlText Text01 = (XmlText) ElementText.ChildNodes[0]; //엘리먼트의 텍스트 값을 읽어오는 다른 방법.
				Debug.Log("Text :"+ Text01.Value); //Text :monsterHarm01

			//전체 정보중 최후의 노드에 접근.
			XmlElement LastChild = (XmlElement) root.LastChild;
			Debug.Log("LastChild의 전체 정보 :"+LastChild.InnerText); //LastChild의 전체 정보 :2113Monster02monsterHarm013500100702.35037100

			//부노 노드에 접근.
			XmlElement temp = LastChild.ParentNode as XmlElement;
			Debug.Log("Temp = "+ temp.InnerText); // 상위는 전체라서 모든 정보 출력: 0111Monster00monsterHarm01110050301.52015101112Monster01monsterHarm0123001005023026502113Monster02monsterHarm013500100702.35037100

			//마지막 노드의 앞의 노드에 접근.
			XmlElement pre = LastChild.PreviousSibling as XmlElement; 
			Debug.Log("pre : "+pre.InnerText); //앞노드의 전체 정보. pre : 1112Monster01monsterHarm012300100502302650 

				//앞노드의 4번쩨 데이터 가져오기.
				XmlElement temp4text = (XmlElement) pre.ChildNodes[4]; 
				Debug.Log("4 childText in pre ="+temp4text.InnerText); //4 childText in pre =Monster01

			//앞 노드의 뒷쪽 노드에 접근.
			XmlElement nextNode = pre.NextSibling as XmlElement;
			Debug.Log("nextNode total : "+nextNode.InnerText); //nextNode total : 2113Monster02monsterHarm013500100702.35037100

				//뒷노드의 4번째 데이터 불러오기.
				XmlElement _4thElementText = (XmlElement) nextNode.ChildNodes[4];
				Debug.Log("_4thElementText = "+_4thElementText.InnerText); //_4thElementText = Monster02


			//root (전체는 xmlNode)
			//  |-> ChildNodes[i] 
			//          |-> ChildNods[i] 

		
			//XML 정보
			//<dataroot xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
			//	<Node>
			//		<ID>0</ID>
			//		<STAGE>1</STAGE>
			//		<CHEPTER>1</CHEPTER>
			//		<PART>1</PART>
			//		<MONSTER_PREFAB_NAME>Monster00</MONSTER_PREFAB_NAME>
			//		<MONSTER_WEAPON_PREFAB>monsterHarm01</MONSTER_WEAPON_PREFAB>
			//		<MONSTER_SPWAN_NUMBER>1</MONSTER_SPWAN_NUMBER>

			// all node total : 0111Monster00monsterHarm01110050301.52015101112Monster01monsterHarm0123001005023026502113Monster02monsterHarm013500100702.35037100
			// node0 total : 0111Monster00monsterHarm01110050301.5201510
			// node1 total : 1112Monster01monsterHarm012300100502302650 
			// node2 total : 2113Monster02monsterHarm013500100702.35037100


		} else
			Debug.Log ("file not exist");
		
	}


}
