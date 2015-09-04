using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO; 
using System;


public class test_xml : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createXml()
	{
		//xml 저장할 경로, 여기 여기 Assets 경로 주의 경로.
		string filepath=Application.dataPath+@"/my.xml";
		//현재 경로를 계속 판단 아래 있는지 그 파일
		if(!File.Exists(filepath))
		{
			//XML 문서 만들기 인스턴스
			XmlDocument xmlDoc=new XmlDocument();

			//생성 루트 노드, 즉 가장 위로 노드
			XmlElement root=xmlDoc.CreateElement("transforms");

			//계속 만든 다음 층 노드
			XmlElement elmNew=xmlDoc.CreateElement("rotation");

			//설정 노드 두 속성 ID와 NAME
			elmNew.SetAttribute("id","0");

			elmNew.SetAttribute("name","momo");
			//계속 만든 다음 층 노드
			XmlElement rotation_X=xmlDoc.CreateElement("x");
			//설정 노드 중 수치
			rotation_X.InnerText="0";
			XmlElement rotation_Y=xmlDoc.CreateElement("y");
			rotation_Y.InnerText="1";
			XmlElement rotation_Z=xmlDoc.CreateElement("z");
			rotation_Z.InnerText="2";
			//여기 추가합니다 노드 속성 을 데 구분. .
			rotation_Z.SetAttribute("id","1");
			
			//그 노드 겹겹이 추가 ~ XMLDoc 중 좀 그들 사이의 선착순 봐, 이 될 생성 XML 파일 순서
			elmNew.AppendChild(rotation_X);
			elmNew.AppendChild(rotation_Y);
			elmNew.AppendChild(rotation_Z);
			root.AppendChild(elmNew);
			xmlDoc.AppendChild(root);
			//그 XML 파일 저장 ~ 로컬
			xmlDoc.Save(filepath);
			Debug.Log("createXml OK!");
		}
	}

	public void UpdateXml()
	{
		string filepath = Application.dataPath+@"/my.xml";
		if(File.Exists(filepath))
		{
			XmlDocument xmlDoc=new XmlDocument();
			//따라 나와 XML 읽기 경로 것이다
			xmlDoc.Load(filepath);
			//transforms 아래 모든 하위 노드 받다
			XmlNodeList nodeList=xmlDoc.SelectSingleNode("transforms").ChildNodes;
			//사이를 옮겨다니기 모든 하위 노드
			foreach(XmlElement xe in nodeList)
			{
				//받은 노드 중 속성 ID =0 있는 노드
				if(xe.GetAttribute("id")=="0")
				{
					//업데이트 노드 속성
					xe.SetAttribute("id","1000");
					//계속 사이를 옮겨다니기
					foreach(XmlElement x1 in xe.ChildNodes)
					{
						if(x1.Name=="z")
						{
							//여기가 수정 노드 이름 대응 수만은 때문에 위의 받은 노드 연대 속성. . .
							x1.InnerText="update00000";
						}
						
					}
					break;
				}
			}
			xmlDoc.Save(filepath);
			Debug.Log("UpdateXml OK!");
		}
		
	}



}
