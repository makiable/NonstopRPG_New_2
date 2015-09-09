using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO; 
using System;


public class XmlController : MonoBehaviour {

	public In_GameManager mIn_GameManager;
	

	//몬스터 스폰 데이터를 가져오는 임시 xml만들기.
	//[HideInInspector]
	public string[] tempMonsterInfoStringArray = new string[15]; //15개 할당 전달 역활.

	//public string[][] public_Temp_Now_Stage_info = new string[1][]; //해당 스테이지의 파트 길이(Count)와 ID값을 저장한다.

	public ArrayList public_Temp_Now_Stage_info = new ArrayList(); //public_Temp_Now_Stage_info = {6,7,8,9}

	public string[] Public_Temp_Part_info = new string[10]; //10개의 스테이지 정보 담을 곳

	public string[] Public_Temp_Monster_info = new string[12];

	//public string StageMonsterInfoXML = "Monster_Respwan_Data";
	
	//스테이지 정보 가져오기.
	public string StageInfo = "Stage_Data_01"; //xml 정보

	//몬스터 리스트 가져오기.
	public string mMonsterInfo = "Monster_List";

	//전달할 스테이지 정보 인수.
	public int now_Stage_Number;

	//스테이저 전체의 배열 정보를 담은 다중 배열.
	//public ArrayList[,] total_Array;


	// Use this for initialization
	void Start () {
	
	}


	
	// Update is called once per frame
	void Update () {
	
	}


	//스테이지 정보 가져오기.
	public void Stage_Load_Form_Xml(int id_number){ //int id_number에 넣고 싶은 스테이지 번호를 넣자.

		public_Temp_Now_Stage_info.Clear(); //초기화.

		var StageNumerArray = new ArrayList ();

		string filepath = Application.dataPath+"/Resources/Stage_Data_01.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			TextAsset textAsset = (TextAsset)Resources.Load ("Stage_Data_02");
			XmlDocument xmldoc = new XmlDocument (); 
			xmldoc.LoadXml (textAsset.text); 

			//노드 카운트 획득. (몇 개 있는지 모르잖아..)
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			
			int ListNumber = 0;

			for (int i = 0; i < nodes.Count; i++) { 
				XmlNode xSearch = nodes.Item(i).SelectSingleNode("STAGE"); //STAGE영역을 검색합니다.

				if (xSearch.InnerText == id_number.ToString()) {
					ListNumber = i;
					StageNumerArray.Add(ListNumber);  //검색한 스테이지의 ID값을 저장함.
					//Debug.Log("=============="+StageNumerArray[i].ToString());
					public_Temp_Now_Stage_info.Add(ListNumber); //외부로 전달 할 값.
					//Debug.Log("++++++++++++++"+public_Temp_Now_Stage_info[i].ToString());
				}
			}//public_Temp_Now_Stage_info = {6,7,8,9}

			now_Stage_Number = StageNumerArray.Count; //검색한 스테이지의 총 숫자를 넣음 

			//루트 엘리먼트 참조.
			XmlElement root = xmldoc.DocumentElement;

			for (int i = 0; i < StageNumerArray.Count; i++) {
				int a = (int)public_Temp_Now_Stage_info[i]; //선택한 스테이지의 파트 부분.
				//몇 번째 ID 정보를 가져올 것인가?
				XmlElement FirstChildElement = (XmlElement)root.ChildNodes [a];  //위에 꺼랑 같은 정보 노출.
			//	Debug.Log (i+"nd ChildElement.InnerText : " + FirstChildElement.InnerText); //i노드의  모든 정보 출력. 

				//선택한 노드의 자식 개수는?? 카운터 값음?
				//int count = FirstChildElement.ChildNodes.Count;

				//여기서 4번 돌아서 으음..-_-);;
				//for (int j = 0; j < count; j++) {
				//	XmlElement ElementText = (XmlElement)FirstChildElement.ChildNodes [j]; //i 노드의 n번째 값을 읽어옴. (i의 n번째 innerText는??)
				//	Debug.Log ("j = "+j+"-->"+ElementText.InnerText);
				//	Public_Temp_Part_info[j] = (string)ElementText.InnerText; //값을 전달함. //여기에서 값을 전달 해야됨.
				//	Debug.Log (j+" temp = " + Public_Temp_Stage_info[j]);
				//} 
			}
		} else
			Debug.Log ("file not exist");
	}

	public string Part_load_From_Xml(int id_number, int Part_Address) //스테이지 ID값을 넣으면 배열로 해당 "파트" 정보만 가지고 온다. ok?!
	{
		string filepath = Application.dataPath+"/Resources/Stage_Data_01.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			TextAsset textAsset = (TextAsset)Resources.Load ("Stage_Data_02");
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 

			//노드 카운트 획득. (몇 개 있는지 모르잖아..)
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			//Debug.Log("Nodes.Count = "+nodes.Count );
			
			int ListNumber = 0;
			
			for (int i = 0; i < nodes.Count; i++) { //노드 카운트 만큼 돌며 ID영역을 검색합니다.
				XmlNode xSearch = nodes.Item(i).SelectSingleNode("ID");
				
				if (xSearch.InnerText == id_number.ToString()) {
					ListNumber = i; //이걸로 검색한 ID의 카운트 숫자를 찾았슴다.
				}
			} //여기까지 ID_Number서치해서 몇번째 있는지 찾음.

			//여기서 스테이지 값 반환.
			//루트 엘리먼트 참조.
			XmlElement root = xmldoc.DocumentElement;
			
			//몇 번째 ID 정보를 가져올 것인가?
			XmlElement SearchXmlData = (XmlElement)root.ChildNodes [ListNumber];  //위에 꺼랑 같은 정보 노출.
			//Debug.Log ("FirstChildElement.InnerText : " + SearchXmlData.InnerText); //i노드의  모든 정보 출력. 
			//FirstChildElement.InnerText : 11121001100210013411
			
			//선택한 노드의 자식 개수는??
			int count = SearchXmlData.ChildNodes.Count;
			//Debug.Log ("SearchXmlData.ChildNodes.Count = " + count);

			//검색할 임시 배열을 생성함.
			for (int i = 0; i < count; i++) {
				XmlElement ElementText = (XmlElement)SearchXmlData.ChildNodes [i]; //i 노드의 n번째 값을 읽어옴. (i의 n번째 innerText는??)
				//Debug.Log ("i = "+i+"-->"+ElementText.InnerText);
				Public_Temp_Part_info[i] = (string)ElementText.InnerText; //값을 전달함.
				//Debug.Log ("temp = " + Public_Temp_Stage_info[i]);
			} 

			//여기서 결과 값만 전달.
			return Public_Temp_Part_info[Part_Address];
		}
		else
			Debug.Log ("file not exist");
			
		return "is Null!!!!";

	}


	
	//몬스터 정보 가져오기. (몬스터 ID값을 넣으면 XML정보를 가져옵니다.
	public void Monster_Load_Form_Xml(int id_numner){
		
		string filepath = Application.dataPath+"/Resources/Monster_List.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			TextAsset textAsset = (TextAsset)Resources.Load ("Monster_List");
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 

			//노드 카운트 획득. (몇 개 있는지 모르잖아..)
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			Debug.Log("Nodes.Count = "+nodes.Count );

			int ListNumber = 0;

			for (int i = 0; i < nodes.Count; i++) { //노드 카운트 만큼 돌며 ID영역을 검색합니다.
				XmlNode xSearch = nodes.Item(i).SelectSingleNode("ID");

				if (xSearch.InnerText == id_numner.ToString()) {
					ListNumber = i; //이걸로 검색한 ID의 카운트 숫자를 찾았슴다.
				}
			}
			//루트 엘리먼트 참조.
			XmlElement root = xmldoc.DocumentElement;

	
			//몇 번째 ID 정보를 가져올 것인가?
			XmlElement SearchXmlData = (XmlElement)root.ChildNodes [ListNumber];  //위에 꺼랑 같은 정보 노출.
		//	Debug.Log ("FirstChildElement.InnerText : " + SearchXmlData.InnerText); //i노드의  모든 정보 출력. 
			//FirstChildElement.InnerText : 11121001100210013411
			
			//선택한 노드의 자식 개수는??
			int count = SearchXmlData.ChildNodes.Count;
		//	Debug.Log ("SearchXmlData.ChildNodes.Count = " + count);
			
			for (int i = 0; i < count; i++) {
				XmlElement ElementText = (XmlElement)SearchXmlData.ChildNodes [i]; //i 노드의 n번째 값을 읽어옴. (i의 n번째 innerText는??)
		//		Debug.Log ("i = "+i+"-->"+ElementText.InnerText);
				Public_Temp_Monster_info[i] = (string)ElementText.InnerText; //값을 전달함.
		//		Debug.Log ("temp = " + Public_Temp_Monster_info[i]);
			} 

			/* 확인용
			Debug.Log("ID : "+nodes[ListNumber].SelectSingleNode("ID").InnerText);
			Debug.Log("MONSTER_PREFAB_NAME : "+nodes[ListNumber].SelectSingleNode("MONSTER_PREFAB_NAME").InnerText);
			Debug.Log("MONSTER_WEAPON_PREFAB : "+nodes[ListNumber].SelectSingleNode("MONSTER_WEAPON_PREFAB").InnerText);
			Debug.Log("MONSTER_SPWAN_NUMBER : "+nodes[ListNumber].SelectSingleNode("MONSTER_SPWAN_NUMBER").InnerText);
			Debug.Log("MONSTER_HP : "+nodes[ListNumber].SelectSingleNode("MONSTER_HP").InnerText);
			Debug.Log("MONSTER_MP : "+nodes[ListNumber].SelectSingleNode("MONSTER_MP").InnerText);
			Debug.Log("MONSTER_POWER : "+nodes[ListNumber].SelectSingleNode("MONSTER_POWER").InnerText);
			Debug.Log("MONSTER_ATTACK_SPEED : "+nodes[ListNumber].SelectSingleNode("MONSTER_ATTACK_SPEED").InnerText);
			Debug.Log("DROP_GOLD : "+nodes[ListNumber].SelectSingleNode("DROP_GOLD").InnerText);
			Debug.Log("DROP_ITEM01 : "+nodes[ListNumber].SelectSingleNode("DROP_ITEM01").InnerText);
			Debug.Log("DROP_ITEM02 : "+nodes[ListNumber].SelectSingleNode("DROP_ITEM02").InnerText);
			Debug.Log("MONSTERSCRIPT : "+nodes[ListNumber].SelectSingleNode("MONSTERSCRIPT").InnerText);
			*/
		} else
			Debug.Log ("file not exist");
	}
	//몬스터 정보 가져오기. (몬스터 ID값을 넣으면 XML정보를 가져옵니다.
	public string Monster_Load_Data(string id_numner, int ID_Address){
		
		string filepath = Application.dataPath+"/Resources/Monster_List.xml";
		
		if (File.Exists (filepath)) {
			//Debug.Log ("file exist");
			TextAsset textAsset = (TextAsset)Resources.Load ("Monster_List");
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 
			
			//노드 카운트 획득. (몇 개 있는지 모르잖아..)
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			//Debug.Log("Nodes.Count = "+nodes.Count );
			
			int ListNumber = 0;
			
			for (int i = 0; i < nodes.Count; i++) { //노드 카운트 만큼 돌며 ID영역을 검색합니다.
				XmlNode xSearch = nodes.Item(i).SelectSingleNode("ID");
				
				if (xSearch.InnerText == id_numner) {
					ListNumber = i; //이걸로 검색한 ID의 카운트 숫자를 찾았슴다.
				}
			}
			//루트 엘리먼트 참조.
			XmlElement root = xmldoc.DocumentElement;
			
			
			//몇 번째 ID 정보를 가져올 것인가?
			XmlElement SearchXmlData = (XmlElement)root.ChildNodes [ListNumber];  //위에 꺼랑 같은 정보 노출.
			//Debug.Log ("FirstChildElement.InnerText : " + SearchXmlData.InnerText); //i노드의  모든 정보 출력. 
			
			//선택한 노드의 자식 개수는??
			int count = SearchXmlData.ChildNodes.Count;
			//Debug.Log ("SearchXmlData.ChildNodes.Count = " + count);
			
			for (int i = 0; i < count; i++) {
				XmlElement ElementText = (XmlElement)SearchXmlData.ChildNodes [i]; //i 노드의 n번째 값을 읽어옴. (i의 n번째 innerText는??)
				//Debug.Log ("i = "+i+"-->"+ElementText.InnerText);
				Public_Temp_Monster_info[i] = (string)ElementText.InnerText; //값을 전달함.
				//Debug.Log ("temp = " + Public_Temp_Monster_info[i]);
			} 
		//	Debug.Log ("return= " + Public_Temp_Monster_info[ID_Address]);
			return Public_Temp_Monster_info[ID_Address];

		} else
			Debug.Log ("file not exist");
		return "is Null!!!!";
	}

	//스테이지 정보 보여주기.
	public void Stage_Xml_Load(){
		
		string filepath = Application.dataPath+"/Resources/Stage_Data_01.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			TextAsset textAsset = (TextAsset)Resources.Load ("Stage_Data_01");
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 
			
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			Debug.Log("Read Test Result");
			
			foreach (XmlNode node in nodes) { //임시파일 전체 돌리기.
				//Debug.Log ("A :"+node.SelectSingleNode("A").InnerText);
				Debug.Log ("ID: " + node.SelectSingleNode ("ID").InnerText);
				Debug.Log ("STAGE: " + node.SelectSingleNode ("STAGE").InnerText);
				Debug.Log ("PART: " + node.SelectSingleNode ("PART").InnerText);
				Debug.Log ("MONSTER_NUMBER: " + node.SelectSingleNode ("MONSTER_NUMBER").InnerText);
				Debug.Log ("MONSTER_ID_01: " + node.SelectSingleNode ("MONSTER_ID_01").InnerText);
				Debug.Log ("MONSTER_ID_02: " + node.SelectSingleNode ("MONSTER_ID_02").InnerText);
				Debug.Log ("MONSTER_ID_03: " + node.SelectSingleNode ("MONSTER_ID_03").InnerText);
				Debug.Log ("DROPITEM_ID_01 :"+node.SelectSingleNode("DROPITEM_ID_01").InnerText);
				Debug.Log ("DROPITEM_ID_02 :"+node.SelectSingleNode("DROPITEM_ID_02").InnerText);
				Debug.Log ("DROPGOLD :"+node.SelectSingleNode("DROPGOLD").InnerText);
			}
		} else
			Debug.Log ("file not exist");
	}
	/*
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

	public void Read_Monsterinfo_With_ID(int ID_number){
		
		string filepath = Application.dataPath+"/Resources/Monster_Respwan_Data.xml";
		
		if (File.Exists (filepath)) {
			Debug.Log ("file exist");
			
			TextAsset textAsset = (TextAsset)Resources.Load (StageMonsterInfoXML);
			XmlDocument xmldoc = new XmlDocument (); //임시 파일 셍성.
			xmldoc.LoadXml (textAsset.text); 
			
			//전체 가져오기..
			XmlNodeList nodes = xmldoc.SelectNodes ("dataroot/Node");
			
			//루트 엘리먼트 참조.
			XmlElement root = xmldoc.DocumentElement;
			
			//몇 번째 노드의 전체 정보를 가져올 것인가?
			XmlElement FirstChildElement = (XmlElement)root.ChildNodes [ID_number];  //위에 꺼랑 같은 정보 노출.
			Debug.Log ("FirstChildElement.InnerText : " + FirstChildElement.InnerText); //i노드의  모든 정보 출력. Result : 0111Monster00MonserHarm011.....

			//선택한 노드의 자식 개수는??
			int count = FirstChildElement.ChildNodes.Count;
			Debug.Log ("count = " + count);

			for (int i = 0; i < count; i++) {
				XmlElement ElementText = (XmlElement)FirstChildElement.ChildNodes [i]; //i 노드의 n번째 값을 읽어옴. (i의 n번째 innerText는??)
				Debug.Log ("i = "+i+"-->"+ElementText.InnerText);
				tempMonsterInfoStringArray[i] = (string)ElementText.InnerText; //값을 전달함.
				Debug.Log ("temp = " + tempMonsterInfoStringArray[i]);
			} 

			//Debug.Log("Send ID :"+ID_number+"NodeResult-firstChild.InnerText: "+nodes[ID_number].SelectSingleNode ("ID").InnerText);


		} else {
			Debug.Log ("file not exist");

		}
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
	*/

}
