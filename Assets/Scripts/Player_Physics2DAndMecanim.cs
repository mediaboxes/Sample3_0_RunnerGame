using UnityEngine;
using System.Collections;

public class Player_Physics2DAndMecanim : MonoBehaviour {

	//　宣言-------------------
	public float speed = 12.0f;
	public float jumpPower = 1600.0f;

	bool goalCheck;
	bool grounded;
	float goalTime;

	// Use this for initialization
	void Start () {
		goalCheck = false;
		grounded = false;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Stage_Gate") {
			//ゴール
			goalCheck = true;
			goalTime = Time.timeSinceLevelLoad;
		}
	}

	// Update is called once per frame
	void Update () {
		Transform groundCheck = transform.Find("GroundCheck");
		grounded = (Physics2D.OverlapPoint(groundCheck.position) != null) ? true : false;

		if (grounded) {
			if (Input.GetButtonDown("Fire1")) {
				//ジャンプ
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpPower));
			}
			//走り
			GetComponent<Animator>().SetTrigger("Run");
		} else {
			Debug.Log("Jump");
			GetComponent<Animator>().SetTrigger("Jump");
		}
		if (transform.position.y < -10.0f) {
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	void FixedUpdate() {
		//移動計算
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

		//カメラ
		GameObject goCam = GameObject.Find("Main Camera");
		goCam.transform.position = new Vector3(transform.position.x + 0.5f, transform.transform.position.y + 2.0f, goCam.transform.position.z);
	}

	void OnGUI() {
		//デバックテキスト
		GUI.TextField(new Rect(10, 10, 300, 60), "[Unity 2D Sample 3-1 C]\nマウスの左ボタンを押すと加速\n離すとジャンプ!");
		if (goalCheck) {
			GUI.TextField(new Rect(10, 100, 330, 60), string.Format("**GOAL**\nTime{0}", goalTime));
		}

		if (GUI.Button(new Rect(10, 80, 100, 20), "リセット")) {
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
