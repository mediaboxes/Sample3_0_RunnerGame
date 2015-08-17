using UnityEngine;
using System.Collections;

public class Player_NonPhysics2D: MonoBehaviour {

	//　宣言-------------------
	public float speed = 15.0f;
	public Sprite[] run;
	public Sprite[] jump;

	float jumpVy;
	int animIndex;
	bool goalCheck;


	// Use this for initialization
	void Start() {
		jumpVy = 0.0f;
		animIndex = 0;
		goalCheck = false;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Stage_Gate") {
			//ゴール
			goalCheck = true;
			return;
		}
		//ゴール以外リトライ
		Application.LoadLevel(Application.loadedLevelName);
	}

	// Update is called once per frame
	void Update() {
		if (goalCheck) {
			return;
		}

		//キャラ高さ
		float height = transform.position.y +jumpVy;
		if (height <= 0.0f) {
			height = 0.0f;
			jumpVy = 0.0f;

			//ジャンプ
			if (Input.GetButtonDown("Fire1")) {
				jumpVy = +1.3f;
				GetComponent<SpriteRenderer>().sprite = jump[0];
			} else {
				//走り
				animIndex++;
				if (animIndex >= run.Length) {
					animIndex = 0;
				}
				GetComponent<SpriteRenderer>().sprite = run[animIndex];
			}
		} else {
			//jumpVy-=0.2f;
			jumpVy -= 6.0f * Time.deltaTime;
		}

		transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, height, 0.0f);
		GameObject goCam = GameObject.Find("Main Camera");
		goCam.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
	}

	void OnGUI() {
		//デバックテキスト
		GUI.TextField(new Rect(10, 10, 300, 60), "[Unity 2D Sample 3-1 A]\nマウスの左ボタンを押すと加速\n離すとジャンプ!");
		if (GUI.Button(new Rect(10, 80, 100, 20), "リセット")) {
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
