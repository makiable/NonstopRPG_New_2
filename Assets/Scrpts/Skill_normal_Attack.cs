using UnityEngine;
using System.Collections;

public class Skill_normal_Attack : MonoBehaviour {

	public Character mAttackCharacter;

	public Character mDamagedCharacter;

	public EffectControl_World totalEffectControl;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void normalHit(Character attacker, int damage, Character damagedC){

		totalEffectControl.On_Effect(damagedC.transform, "hit_Effect");

		attacker.SendMessage ("AttackTest");

		damagedC.mHP -= damage;
		
		if (damagedC.mHP > 0) {
			damagedC.SendMessage("DamagedTest", damage);
			Debug.Log("Damage = "+damage);
		}
		
		if (damagedC.mHP <= 0) {
			damagedC.SendMessage("DeadTest");
		}
	}
}
