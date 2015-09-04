using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO; 

public class In_GameManager : MonoBehaviour {

	//외부 파일 관련.xml 컨트롤러를 하나 만들자.
	public XmlController mXmlController;

	//플레이어 관련 스크립트..
	public PlayerLocal_info mPlayerLocal_info;

	//히어로 컨트롤.
	public HeroControl mHero01;

	//스킬 클타임 버튼 - 히어로에게서 나옴.
	public CoolTimeButton Skill01; //전체 공격 
	public CoolTimeButton Skill02; //연속 공격.

	//노말 공격 컨트롤
	public Skill_normal_Attack mNormalSkill;


	//몬스터 컨트롤 
	[HideInInspector]
	public List<MonsterControl> mMonster01;

	//오토 타겟 몬스터 참조. 1명을 잡을때
	[HideInInspector]
	public MonsterControl TargetMonster;
	public MonsterControl[] AllTargetMonster;
	private int monsterSpwanNumber = 3;
	
	// 몬스터 출연 위치.
	public Transform[] mSpwanPoint;

	//몬스터가 드랍하는 골드 설정..
	public ulong monsterDropGold;

	//EFFECT
	//모든 이팩트 컨트롤.
	public EffectControl_World totalEffectControl;
	
	//UI
	//텍스트 메세지.
	public TextMesh mIngTextMassage;
	public TextMesh killMonster;
	public TextMesh getGold;


	// 던전 인포.
	// 던전을 탐험하는 횟수입니다.
	public int mLoopCount;
	// 화면에 나타난 적의 합
	public int mMonsterCount = 0;
	
	//스테이지 상황.
	public enum StageStatus
	{
		Idle,
		Start,
		BattleIdle,
		Battle,
		Clear,
	}
	//기본 스테이지 상황.
	public StageStatus mStageStatus = StageStatus.Idle;


	// Use this for initialization
	void Start () {
		// 적 몬스터 들이 담길 List
		mMonster01 = new List<MonsterControl>();
		mMonster01.Clear();
		// 던전 탐험 스텝을 만들어서 순서대로 순환시킵니다.
		StartCoroutine ("AutoStep");

		//플레이어 정보 초기화. 나중에 없어져야 함.
		PlayerPrefs.SetInt ("MonsterKillCount", 0 );

		//mXmlController.Monster_Xml_Load (1);
		//mXmlController.Xml_Load ("xml_Test_01");
		//mXmlController.Monster_Xml_Load("Monster_Respwan_Data");
		mXmlController.Test_Monster_ID_Xml_Load ("Monster_Respwan_Data", 1);


	}
	
	// Update is called once per frame
	void Update () {

	}


	IEnumerator AutoStep()
	{
		while (true) 
		{
			if (mStageStatus == StageStatus.Idle) 
			{
				//스타트 TEXT 출현..
				mIngTextMassage.text = "스타트!";

				yield return new WaitForSeconds (0.5f);

				mStageStatus = StageStatus.BattleIdle;

			}

			else if (mStageStatus == StageStatus.BattleIdle) {
				//몬스터 등장...
				mHero01.SetStatus(HeroControl.Status.Idle);
				mMonster01.Clear();
				for (int i = 0; i < monsterSpwanNumber; i++) {
					//X 마리의 몬스터를 소환 합니다.
					SpawnMonster(i);

					//딜레이를 둔다. for 문에 딜레이를 줌.
					yield return new WaitForSeconds(0.5f);
				}

				yield return new WaitForSeconds(2); // 2초 대기..

				mIngTextMassage.text="*배틀 스타트*";

				//배틀 상태로 둔다..
				mStageStatus = StageStatus.Battle;

				//코루틴 실행.
				StartCoroutine("HeroAutoAttack");
				StartCoroutine("MonsterAutoAttack");
				yield break;
			}
		}
	}
	

	private void SpawnMonster(int idx)
	{
		// Resources 폴더로부터 Monster 프리팹(Prefab)을 로드합니다.
		Object prefab = Resources.Load("Monster01");

		// 참조한 프리팹을 인스턴스화 합니다. (화면에 나타납니다.)
		GameObject monster = Instantiate(prefab, mSpwanPoint[idx].position, Quaternion.identity) as GameObject;

		//위치 값이 이상해서, 수동으로 조절 했음.
		monster.transform.parent = mSpwanPoint[idx];

		// 생성된 인스턴스에서 MonsterControl 컴포넌트를 불러내어 mMonster 리스트에 Add 시킵니다.
		mMonster01.Add(monster.GetComponent<MonsterControl>());

		// 생성된 몬스터 만큼 카운팅 됩니다.
		mMonsterCount += 1;
		mMonster01[idx].idx = idx;
		mMonster01[idx].RandomHP();//
		mMonster01[idx].hptext.text = mMonster01[idx].mHP.ToString ();

		mMonster01 [idx].TargetNumber = idx+1;
		monster.name = "Monster01"+idx;
		// 레이어 오더를 단계적으로 주어 몬스터들의 뎁스가 차례대로 겹치도록 한다.
		monster.GetComponent<SpriteRenderer>().sortingOrder = idx+1;
	}

	IEnumerator HeroAutoAttack(){

		//타겟을 잡고..
		GetSingleAutoTarget ();

		while (mStageStatus == StageStatus.Battle) {
			//공격 애니메이션 추가.

			if (TargetMonster.mStatus != MonsterControl.Status.Dead) {
				//노말 공격 -> 애니메이션 까지  끝냄. ㅋㅋㅋ
				mNormalSkill.normalHit(mHero01, GetRandomDamage(mHero01.mAttackPower), TargetMonster);
			}
			//한번 공격하고 공격 속도 만큼 멈춘다.
			yield return new WaitForSeconds(mHero01.mAttackSpeed);
		}
	}


	//사용자 공격 관련

	public int GetRandomDamage(int damage){
		return damage + Random.Range(-10, 10);
	}

	public void TapAttack(){ //모든 적을 공격하는 광역 공격.
		
		//StopCoroutine("HeroAutoAttack");

		Debug.Log ("사용 전 현재 몇마리 남음? = "+mMonsterCount);
		
		while (mStageStatus == StageStatus.Battle) {
			GetSingleAutoTarget ();

			if (mHero01.CriticalRate < Random.Range(0,100)) {
				mNormalSkill.normalHit(mHero01, GetRandomDamage(mHero01.mAttackPower * 2), TargetMonster);
				Debug.Log("it's Critical");
			}
			else {
				mNormalSkill.normalHit(mHero01, GetRandomDamage(mHero01.mAttackPower), TargetMonster);
			}

			Debug.Log("TapAttack");

			break;
		}
		//Invoke ("WaitAndStartCoroutine", 0.5f);
	}

	public void HeroSkillAttack01(){ //모든 적을 공격하는 광역 공격.

		//StopCoroutine("HeroAutoAttack");
		Debug.Log ("사용 전 현재 몇마리 남음? = "+mMonsterCount);

		while (mStageStatus == StageStatus.Battle) {
			for (int i = 0; i < monsterSpwanNumber; ++i) {
				if (mMonster01[i].mStatus != MonsterControl.Status.Dead) {
					mNormalSkill.normalHit (mHero01, GetRandomDamage (mHero01.mAttackPower), mMonster01 [i]);
					Debug.Log ("몬스터 " + i + "에게 " + mMonster01 [i].saveDamageTextForShow + "를 주었다. 남은 HP = " + mMonster01 [i].mHP);
				}
			}
			Debug.Log (" 스킬 사용 후 현재 몇마리 남음? = "+mMonsterCount);
			break;
		}
		//Invoke ("WaitAndStartCoroutine", 1.2f);
	}

	public void HeroSkillAttack02(){ //자동으로 연속 공격 하는 스킬.
		if (mStageStatus == StageStatus.Battle) {
			//StopCoroutine("HeroAutoAttack");
			StartCoroutine("HeroSerialAttack");
			}
		float a = Skill02.runtime;
		Invoke("StopSkill2",a);
	}

	IEnumerator HeroSerialAttack(){
		while (mStageStatus == StageStatus.Battle) {
			GetSingleAutoTarget ();
			
			if (mHero01.CriticalRate < Random.Range(0,100)) {
				mNormalSkill.normalHit(mHero01, GetRandomDamage(mHero01.mAttackPower * 2), TargetMonster);
				Debug.Log("it's Critical");
			}
			else {
				mNormalSkill.normalHit(mHero01, GetRandomDamage(mHero01.mAttackPower), TargetMonster);
			}
			Debug.Log("Skill2 Serise Attack");
			yield return new WaitForSeconds (0.2f);
		}
	}
	public void StopSkill2(){
		StopCoroutine ("HeroSerialAttack");
	}
	

	void WaitAndStartCoroutine(){
		StartCoroutine("HeroAutoAttack");
	}

	private void GetAllAutoTarget(){
	//	TargetMonster.SetAllTarget ();
	}
	
	private void GetSingleAutoTarget(){
		//1. HP로 소팅할 경우 이거 참조..
		//TargetMonster = mMonster01.Where(m=>m.mHP > 0).OrderBy(m=>m.mHP).First();
		//2. 타겟 넘버를 지정 맨 앞에 를 타겟으로 잡는다.
		if (mStageStatus == StageStatus.Battle) {
			TargetMonster = mMonster01.Where (m => m.TargetNumber > 0).OrderBy (m => m.TargetNumber).First ();
			TargetMonster.SetSingleTarget ();
		} else{
			//Debug.Log ("not in battle");
		}
	}

	public void CoolTimeReset(){
	
		Skill01.ResetCooltime ();
	}

	public void CoolTimeReset2 (){
		Skill02.ResetCooltime ();

	}

	public void ReAutoTarget(){

		mMonsterCount -= 1;

		if (mMonsterCount == 0) {
			//한 스테이지 클리어 
			StopCoroutine ("HeroAutoAttack");
			StopCoroutine ("MonsterAutoAttack");

			mLoopCount -= 1;
			//mIngTextMassage.text = "모든 적을 격파";



			if (mLoopCount == 0) {
				//모든 스테이지 클리어. -> 승리 결과창.

				mIngTextMassage.text = "스테이지 클리어!";

				mStageStatus = StageStatus.Clear;
				GameOver();

				return;
			}

			mStageStatus = StageStatus.Idle;
			StartCoroutine ("AutoStep");
			return;
		}
		//타겟 재 탐색.
		GetSingleAutoTarget();
	}

	// 몬스터 자동 공격 입니다잉~~~
	IEnumerator MonsterAutoAttack(){
		//타겟을 찾습니다..
		GetMonsterSingleAutoTarget ();

		while (mStageStatus == StageStatus.Battle) {
			//공격에 들어갑니다. 몬스터는 여러마리..(위에나 밑에나 같다..)
			//for (int i = 0; i < monsterSpwanNumber; i++) {
			//	if (mMonster01[i].mStatus != MonsterControl.Status.Dead) {
			//		//노말 공격 -> 애니메이션 까지  끝냄. ㅋㅋㅋ
			//		mNormalSkill.normalHit(mMonster01[i], GetRandomDamage(mMonster01[i].mAttack), mHero01);
			//		yield return new WaitForSeconds(mMonster01[i].mAttackSpeed + Random.Range(0, 0.5f));
			//	}
			//}
			//break;

			foreach (MonsterControl monster in mMonster01) { //모든 몬스터를 하나씩 돌린다..
				if (monster.mStatus == MonsterControl.Status.Dead) continue;
				//노말 공격 -> 애니메이션 까지  끝냄. ㅋㅋㅋ
				mNormalSkill.normalHit(monster, GetRandomDamage(monster.mAttack), mHero01);
				yield return new WaitForSeconds(monster.mAttackSpeed + Random.Range(0, 0.5f));
			}
		}
	}
	
	private void GetMonsterSingleAutoTarget(){

		//지금은 한명이니, 1인에가 타겟임..
		mHero01.mHeroSingleTargeted = true;
	}
	

	// 버튼의 명령을 받았을때, 처리 하는 곳.
	void OnButtonDown(string trigger){
		if (trigger == "Back") {
			//mCameraControl.SetStatus(CameraControl.Status.Start);
			Invoke("StartButton",0.5f);
		}
		//캐릭터 상태 동작.
		if (trigger == "ChangeStatus") {
			mHero01.SetStatus(HeroControl.Status.Attack);
		}

		if (trigger == "StopStatus") {
			mHero01.SetStatus(HeroControl.Status.Idle);
		}
		if (trigger == "UseSkill") {
			mHero01.SetStatus(HeroControl.Status.UseSkill);
		}
		
		if (trigger == "Damaged") {
			mHero01.SetStatus(HeroControl.Status.Damaged);
		}
		if (trigger == "Dead") {
			mHero01.SetStatus(HeroControl.Status.Dead);
		}
		if (trigger == "SkillAllDamage") {
			HeroSkillAttack01();
		}
		if (trigger == "TapAttack") {
			TapAttack();
		}
		
	}
	// 버튼의 명령 처리 함수 
	void StartButton(){
		Application.LoadLevel("Menu_Default_Scene");
	}

	public void GameOver(){
		//임시
		Debug.Log("GameOver");    
		StopCoroutine ("HeroAutoAttack");
		StopCoroutine ("MonsterAutoAttack");
		StopCoroutine ("AutoStep");
		//Application.LoadLevel("Menu_Default_Scene");

	}


}
