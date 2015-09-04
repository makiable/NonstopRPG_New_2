using UnityEngine;
using System.Collections;

public class HeroControl : Character {

	//mAnimator를 선언.
	private Animator mAnimator;
	
	//공격 데미지 (현제는 걍 데미지만.. 나중에 공격력이랑 수정 필요)
	public int mOrinAttackPower;
	[HideInInspector]
	public int mAttackPower;

	public float CriticalRate;

	//공속 
	public float mAttackSpeed;

	//스킬 쿨타임.
	public float mSkillSpeed;

	//히어로의 터겟 넘버.
	public int mHeroTargetNumber;

	public bool mHeroSingleTargeted;


	void Start () {

		//1.HP 넣고, 2. 백그라운드 컴퍼넌트 넣고, 3. 활이 발사될 장소를 넣고. 스타트
		mHP = mOrinHP;

		//무기 데미지 + 본인 데미지.
		mAttackPower = mOrinAttackPower + mWeapon_Prefab.damage;

		hptext.text = mHP.ToString ();

		mHeroTargetNumber = 1;
		mHeroSingleTargeted = true;

		mAnimator = gameObject.GetComponent<Animator> ();

	
	}
	//상테와 파라메터를 통해 아처의 상태를 컨트롤 합니다.
	public void SetStatus(Status status)
	{
		//animator 에서 만든 상태 간 전이를 상황에 맞게 호출 한다.
		switch (status) {
		case Status.Idle:
			mAnimator.SetTrigger("Idle");
			Debug.Log("idle---");
			break;
			
		case Status.Attack:
			mAnimator.SetTrigger("Basic_Attack");
			Debug.Log("Attack---");
			break;
			
		case Status.Dead:
			mAnimator.SetTrigger("Dead");
			Debug.Log("Die---");
			break;
			
		case Status.Damaged:
			mAnimator.SetTrigger("Damaged");
			Debug.Log("Damage---");
			break;

		case Status.UseSkill:
			mAnimator.SetTrigger("Skill01");
			Debug.Log("Skill---");
			break;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AttackTest(){
		mAnimator.SetTrigger("Attack");
		mWeapon_Prefab.viewWeapon (weapon_View_tranform.transform, mWeapon_Prefab.prefabName);

	}

	public void DamagedTest(int damage){
		mAnimator.SetTrigger("Damaged");
		
		//Debug.Log ("hitted --> "+gameObject.name);
		saveDamageTextForShow = damage;
		mIn_GameManager.mIngTextMassage.text = "적에게 데미지:" + saveDamageTextForShow + "를 주었다.";
		
		hptext.text = mHP.ToString ();
		//Debug.Log ("last hp ="+mHP);
		
	}

	public int GetRandomDamage(){

		return mAttackPower + Random.Range(-10, 10);
	}

	public void DeadTest(){
		
		if (mStatus !=Status.Dead) {
			mStatus = Status.Dead;
			mHP = 0;
			hptext.text = mHP.ToString ();
			mIn_GameManager.mIngTextMassage.text = "Player Dead.";
			mAnimator.SetTrigger("Dead");
			
			
		}
		else if (mStatus == Status.Dead) {
			Debug.Log("Already Die");
		}
	}
}






































