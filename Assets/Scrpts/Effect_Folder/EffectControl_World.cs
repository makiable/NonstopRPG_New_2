using UnityEngine;
using System.Collections;

public class EffectControl_World : MonoBehaviour {


	public EffectControl mEffectControl;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void On_Effect(Transform pos, string PrefabName)
	{
		Transform mPosition = pos;

		string _prefabName = PrefabName;
		
		// Resources 폴더로부터 Monster 프리팹(Prefab)을 로드합니다.
		Object prefab = Resources.Load(_prefabName);
		
		//위치를 받아야함.
		
		// 참조한 프리팹을 인스턴스화 합니다. (화면에 나타납니다.)
		GameObject effect = Instantiate(prefab, mPosition.position, Quaternion.identity) as GameObject;
		
		// 생성된 인스턴스에서 MonsterControl 컴포넌트를 불러내어 mMonster 리스트에 Add 시킵니다.
		effect.GetComponent<SpriteRenderer>().sortingOrder = 1;
	}


}
