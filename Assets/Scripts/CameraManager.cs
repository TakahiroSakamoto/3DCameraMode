using System.Collections;
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
	CAMERA_MODE cameraMode;
	
	private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0, 0, 0);

	float time;

	void Awake() {
		foreach(var obj in cameras) {
			obj.SetActive(false);
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
		} else if(Input.GetKeyDown("q")) {
			cameraMode = CAMERA_MODE.FIXEDPOINT;
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
		// cameras[1].transform.DOLocalPath (new Vector3[]{ new Vector3 (0.4f, 1.6f, 1.5f), new Vector3(2f, 2f), new Vector3 (1f, 1f) }, 3f, PathType.CatmullRom);
		// cameras[1].transform.rotation = Quaternion.Euler(22,197,2);

		
		time += Time.deltaTime;

		if(time > 4f) {
			// point2に移動
			
		} else if(time > 8f) {
			// point3に移動
			
		}  else if (time > 12f) {
			// point1に移動
			time = 0f;
		}
	}

	void UpdateForLockUp() {
		cameras[2].SetActive(true);
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
		cameras[0].SetActive(true);
		Time.timeScale = 0.2f;
	}
}
