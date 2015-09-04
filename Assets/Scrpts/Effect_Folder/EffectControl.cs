using UnityEngine;
using System.Collections;

public class EffectControl : EffectControl_World {

	//프라이빗 게임메니저에서 이팩트를 컨트롤 한다.
	public In_GameManager mIn_GameManager;

	public Animator mEffectAnimator;

	public enum EffectStatus{
		On
	}
	// Use this for initialization
	void Start () {

		// 참조해야할 객체나 스크립트들을 여기서 설정하게 될 것입니다.
		mIn_GameManager = In_GameManager.FindObjectOfType<In_GameManager>();

		mEffectAnimator = gameObject.GetComponent<Animator> ();

		SetStatus ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetStatus(){
		mEffectAnimator.SetTrigger ("On");
	}

	public void destroy(){
		Destroy (gameObject);
	}
}
