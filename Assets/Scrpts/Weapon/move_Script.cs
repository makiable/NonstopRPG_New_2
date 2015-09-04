using UnityEngine;
using System.Collections;

public class move_Script : MonoBehaviour{

	public float rotationSpeedX = 0;
	public float rotationSpeedY = 0;
	public float rotationSpeedZ = 0;

	public float StraightSpeedX = 0;
	public float StraightSpeedY = 0;
	public float StraightSpeedZ = 0;

	public bool isHeHero;
	
	// Update is called once per frame
	void Update () {

		if (isHeHero != true) {
			// 매 프레임에 원하는 방향으로 회전 시킵니다.
			transform.Rotate (new Vector3 (rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
			
			// 매 프레임에 원하는 방향으로 이동 시킨다.
			transform.Translate (new Vector3 (StraightSpeedX, StraightSpeedY, StraightSpeedZ) * Time.deltaTime);

		} else {
			// 매 프레임에 원하는 방향으로 회전 시킵니다.
			transform.Rotate (new Vector3 (rotationSpeedX * -1 , rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
			
			// 매 프레임에 원하는 방향으로 이동 시킨다.
			transform.Translate (new Vector3 (StraightSpeedX * -1 , StraightSpeedY, StraightSpeedZ) * Time.deltaTime);

		}	
	}
}
