using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoolTimeButton : MonoBehaviour {

	public Image img;

	//버튼 오브젝트 연결 할 꺼..
	public UnityEngine.UI.Button btn;
	public float cooltime = 60.0f;
	public bool disableOnStart = true;
	float leftTime = 60.0f;
	public Text lefttimetext;

	//실행하게 되면 해당 초 동안(Runtime)동안 작동..
	public bool notUseRuntime;
	public float runtime = 10.0f;
	public float setRuntime;
	public bool isRuntime = false;
	public Text runtimeText;

	
	// Use this for initialization
	void Start () {
		if (img == null) {
			img = gameObject.GetComponent<Image>();
		}

		if (btn == null) {
			btn = gameObject.GetComponent<UnityEngine.UI.Button>();
		}
		if (disableOnStart) {
			ResetCooltime();
		}

		if (notUseRuntime == true) {
			runtimeText.text = "";
		} else {
			runtimeText.text = "Ready";
			setRuntime = runtime;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (leftTime > 0 ) {
			leftTime -=Time.deltaTime;

			lefttimetext.text = ((int)leftTime).ToString(); //초만 출력 

			if (leftTime < 0) {
				leftTime = 0;
				if (btn) {
					btn.enabled = true;
					lefttimetext.text = "READY";
				}
			}
			float ratio = 1.0f - (leftTime / cooltime);
			if (img) {
				img.fillAmount = ratio;
			}
		}

		if (notUseRuntime != true) {
			if (isRuntime == true) {
				runtime -=Time.deltaTime;
				runtimeText.text = ((int)runtime).ToString(); //초만 출력 
				if (runtime <= 0) {
					isRuntime = false;
					runtimeText.text = "Ready";
					runtime = setRuntime;
				}
			}
		}
	}

	public void On_Runtime(){
		isRuntime = true;
	}

	public bool CheckCooltime(){
		if (leftTime > 0) {
			return false;
		} else
			return true;
	}

	public void ResetCooltime(){
		leftTime = cooltime;
		if (btn) {
			btn.enabled = false;
		}
	}


}







































