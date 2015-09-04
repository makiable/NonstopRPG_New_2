using UnityEngine;
using System.Collections;

public class Weapon_Basic_Info : All_Weapon_World {

	public string prefabName;

	public int damage;
	public float viewTime;
	
	//시작 위치..
	public float _positionX = 0;
	public float _positionY = 0;
	public float _positionZ = 0;
	
	// Use this for initialization
	void Start () {
		destroy (); //공격할때만 보여지고..사라짐.
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void destroy(){
		Destroy (gameObject, viewTime);
	}
	
	public void viewWeapon (Transform pos, string PrefabName)
	{
		Transform mPosition = pos;
		
		//여기에서 무기의 포지션을 가져와서 넣으면 변경이 가능함.
		
		string _prefabName = PrefabName;
		
		// Resources 폴더로부터 Monster 프리팹(Prefab)을 로드합니다.
		Object prefab = Resources.Load(_prefabName);
		
		// 참조한 프리팹을 인스턴스화 합니다. (화면에 나타납니다.)
		GameObject viewWeaponObject = Instantiate(prefab, mPosition.position + new Vector3 (_positionX, _positionY, _positionZ) , Quaternion.identity) as GameObject;

		// 생성된 인스턴스에서 MonsterControl 컴포넌트를 불러내어 mMonster 리스트에 Add 시킵니다.
		viewWeaponObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
	}
	
}

