using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody rbody;

	public float toVel = 2.5f; //преобразует оставшееся расстояние в целевую скорость - если оно слишком мало, твердое тело замедляется раньше
	public float maxVel = 15.0f; //максимальная скорость, которую достигает твердое тело при движении; 
	public float maxForce = 40.0f;
	public float gain = 5f;
	public float speed = 8;
	public static bool start { get; set; }

	public GameObject startScreen;
	Vector3 startPlayerPos;

	// Use this for initialization
	void Start()
	{
		start = false;
		rbody = gameObject.GetComponent<Rigidbody>();
		startPlayerPos = gameObject.transform.position;
	}

	public void SetStart()
    {
		start = true;
		startScreen.SetActive(false);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(start)
        {
			rbody.AddForce(new Vector3(0, 0, speed), ForceMode.Impulse);
		}
		if (Input.GetMouseButton(0) && start)
		{
			PlayerMove();
		}
	}

	private void PlayerMove()
	{
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20); // переменной записываються координаты мыши по иксу и игрику
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши

		Vector3 dist = objPosition - transform.position;
		dist.y = 0;
		Vector3 tgtVel = Vector3.ClampMagnitude(toVel * dist, maxVel);

		Vector3 error = tgtVel - rbody.velocity;

		Vector3 force = Vector3.ClampMagnitude(gain * error, maxForce);
		rbody.AddForce(force);
	}
	public void ResetPosition(bool isEnemy) // Reset position
	{
		start = false;
		startScreen.SetActive(true);

		rbody.isKinematic = true;

		transform.position = startPlayerPos;

		rbody.isKinematic = false;
	}
}