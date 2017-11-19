﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour {

	public enum CAMERA_MODE {
		FIXEDPOINT,
		LOCKUP,
		POSE
	}

	[SerializeField] GameObject player;
	[SerializeField] GameObject center;
	[SerializeField] List<GameObject> cameras = new List<GameObject>();
	[SerializeField] List<GameObject> pointCameras = new List<GameObject>();

	[SerializeField] List<GameObject> pointEffect = new List<GameObject>();
	CAMERA_MODE cameraMode;
	
	private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0, 0, 0);

	float time;

	void Awake() {
		// foreach(var obj in cameras) {
		// 	obj.SetActive(false);
		// }
		// foreach(var obj in pointEffect) {
		// 	obj.SetActive(false);
		// }

		for(int i = 0; i > cameras.Count; i++) {
			cameras[i].SetActive(false);
			pointEffect[i].SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		cameraMode = CAMERA_MODE.LOCKUP;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("w")) {
			cameraMode = CAMERA_MODE.LOCKUP;
			Time.timeScale = 1;
		} else if(Input.GetKeyDown("q")) {
			cameraMode = CAMERA_MODE.FIXEDPOINT;
			Time.timeScale = 1;
		} else if(Input.GetKeyDown("e")){
			cameraMode = CAMERA_MODE.POSE;
		}

		if(cameraMode == CAMERA_MODE.FIXEDPOINT) {
			UpdateForFixedPoint();
		} else if(cameraMode == CAMERA_MODE.LOCKUP) {
			UpdateForLockUp();
		} else if(cameraMode == CAMERA_MODE.POSE) {
			UpdateForPose();
		}
	}

	void UpdateForFixedPoint() {
		// 定点カメラ移動、揺れる
		// エフェクト降らす、
		cameras[1].SetActive(true);
		cameras[2].SetActive(false);
		cameras[0].SetActive(false);
		

		// for(int i = 0; i > cameras.Count; i++) {
		// 	if(i == 0 || i == 2) {
		// 		Debug.Log("切り替え1ON");
		// 		cameras[i].SetActive(false);
		// 	} else if (i == 1) {
		// 		Debug.Log("切り替え1ONONONON");
		// 		cameras[i].SetActive(true);
		// 	}
		// }
		
		time += Time.deltaTime;

		if(time > 4f) {
			// point2に移動
			ChangeTransform(1);
			pointEffect[1].SetActive(true);
			pointEffect[0].SetActive(false);
			pointEffect[2].SetActive(false);
		} 
		if(time > 8f) {
			// point3に移動
			ChangeTransform(2);
			pointEffect[2].SetActive(true);
			pointEffect[0].SetActive(false);
			pointEffect[1].SetActive(false);
		} 
		if (time > 12f) {
			// point1に移動
			ChangeTransform(0);
			pointEffect[0].SetActive(true);
			pointEffect[1].SetActive(false);
			pointEffect[2].SetActive(false);
			time = 0f;
		}
		
	}

	void UpdateForLockUp() {
		// for(int i = 0; i > cameras.Count; i++) {
		// 	if(i == 0 || i == 1) {
		// 		Debug.Log("切り替え2ON");
		// 		cameras[i].SetActive(false);
		// 	} else if (i == 2) {
		// 		cameras[i].SetActive(true);
		// 	}
		// }

		cameras[2].SetActive(true);
		cameras[0].SetActive(false);
		cameras[1].SetActive(false);
		pointEffect[0].SetActive(false);
		pointEffect[1].SetActive(false);
		pointEffect[2].SetActive(false);
		
		// 回転カメラ
		if (Input.GetMouseButtonDown(0))
        {
            // マウスクリック開始(マウスダウン)時にカメラの角度を保持(Z軸には回転させないため).
            newAngle = center.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // マウスの移動量分カメラを回転させる.
            newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
            center.gameObject.transform.localEulerAngles = newAngle;

            lastMousePosition = Input.mousePosition;
        }
	}

	void UpdateForPose() {
		// スローモーション

		// for(int i = 0; i > cameras.Count; i++) {
		// 	if(i == 1 || i == 2) {
		// 		Debug.Log("切り替え3ON");
		// 		cameras[i].SetActive(false);
		// 	} else if (i == 0) {
		// 		Debug.Log("切り替え3ONONONO");
		// 		cameras[i].SetActive(true);
		// 	}

		cameras[0].SetActive(true);
		cameras[1].SetActive(false);
		cameras[2].SetActive(false);
		pointEffect[0].SetActive(false);
		pointEffect[1].SetActive(false);
		pointEffect[2].SetActive(false);
		
		
		Time.timeScale = 0.2f;
	}

	void ChangeTransform(int num) {
		cameras[1].transform.position = pointCameras[num].transform.position;
		cameras[1].transform.rotation = pointCameras[num].transform.rotation;
		

		for(int i = 0; i > pointEffect.Count; i++) {
			if(i == num) {
				pointEffect[i].SetActive(true);
			} else {
				pointEffect[i].SetActive(false);
			}
		}
	}
}
