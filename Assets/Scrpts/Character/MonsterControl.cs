using UnityEngine;
using System.Collections;

public class MonsterControl : Character {

	public Animator mAnimator;

	//몬스터 숫자.
	public int idx;

	//public TextMesh hptext;
	
	//공격 데미지 (현제는 걍 데미지만.. 나중에 공격력이랑 수정 필요)
	public int mOrinAttack;
	[HideInInspector]
	public int mAttack;
	
	//공속 
	public float mAttackSpeed;

	//싱글 타겟이 된 것인지. 확인한다..
	public bool SingleTargeted;

	//모든 타겟이 된 것인지..
	public bool AllTargeted;

	[HideInInspector]
	public int TargetNumber;

	void Start () {

		// 참조해야할 객체나 스크립트들을 여기서 설정하게 될 것입니다.
		mIn_GameManager = In_GameManager.FindObjectOfType<In_GameManager>();

		mMP = mOrinMP;
		mAttack = mOrinAttack;

		SingleTargeted = false;
		AllTargeted = false;
		
		//Archer의 Animator 컴포넌트 레퍼런스를 가져옵니다.
		//이 script가 붙은 gameObject에 Animator를 가져옴.
		mAnimator = gameObject.GetComponent<Animator> ();
	}

	//상테와 파라메터를 통해 아처의 상태를 컨트롤 합니다.
	public void SetStatus(Status status)
	{
		//animator 에서 만든 상태 간 전이를 상황에 맞게 호출 한다.
		switch (status) {
		case Status.Idle:
			mAnimator.SetTrigger("Idle");
			Debug.Log("MM idle---");
			break;
			
		case Status.Attack:
			mAnimator.SetTrigger("Attack");
			Debug.Log("MM Attack---");
			break;
			
		case Status.Dead:
			mAnimator.SetTrigger("Dead");
			Debug.Log("MM Die---");
			break;
			
		case Status.Damaged:
			mAnimator.SetTrigger("Damaged");
			Debug.Log("MM Damage---");
			break;
			
		case Status.UseSkill:
			mAnimator.SetTrigger("Skill");
			Debug.Log("MMSkill---");
			break;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void RandomHP()
	{
		//1.HP 넣고, 2. 백그라운드 컴퍼넌트 넣고, 3. 활이 발사될 장소를 넣고. 스타트
		mHP = mOrinHP;
		//Debug.Log ("mOrinHP= " + mOrinHP);

		mHP += Random.Range(-10, 10);    

		//Debug.Log ("hp is = "+ mHP);
	}

	public void SetSingleTarget(){
		SingleTargeted = true;
	}

	public void SetAllTarget(){
		AllTargeted = true;
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

	public void DeadTest(){

		if (mStatus !=Status.Dead) {
			mStatus = Status.Dead;
			mHP = 0;
			hptext.text = mHP.ToString ();
			mIn_GameManager.mIngTextMassage.text = "적을 물리쳤다.";
			SingleTargeted = false;
			AllTargeted = false;
			mAnimator.SetTrigger("Dead");
			TargetNumber = -1; //더이상 타겟이 아님요...
			//Debug.Log("Dead ->"+gameObject.name);
			mIn_GameManager.ReAutoTarget();


			int tempkill = PlayerPrefs.GetInt ("MonsterKillCount");
			PlayerPrefs.SetInt ("MonsterKillCount", tempkill + 1 );
			//Debug.Log("현재 킬카운트 = "+PlayerPrefs.GetInt("MonsterKillCount"));

			mIn_GameManager.killMonster.text = PlayerPrefs.GetInt("MonsterKillCount").ToString();


		}
		else if (mStatus == Status.Dead) {
			//Debug.Log("Already Die");
		}
	}

	void destroy(){
		Destroy (gameObject);
	}
}
