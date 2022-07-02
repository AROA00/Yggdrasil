using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public partial class EMath
{
	public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
	{
		float Func(float x) => 4 * (-height * x * x + height * x);

		var mid = Vector3.Lerp(start, end, t);

		return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
	}

	public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
	{
		float Func(float x) => 4 * (-height * x * x + height * x);

		var mid = Vector2.Lerp(start, end, t);

		return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
	}
}



public class BossSkill : MonoBehaviour
{

	

	public GameObject target;
	public GameObject[] EnemyPrefebs = new GameObject[2];
	public GameObject Laser_Effect;
	public GameObject Lightning_Effect;
	public GameObject Thunderbolt_Effect;

	private Animator boss_anim;

	public float angleRange = 60f;
	public float distance = 5f;
	public bool isCollision = false;

	private float checkTime = 0f;
	private bool checkSkill = false;

	Color _blue = new Color(0f, 0f, 1f, 0.2f);
	Color _red = new Color(1f, 0f, 0f, 0.2f);

	Vector3 direction;

	float dotValue = 0f;

	


	public void Pull()
	{

		

		Debug.Log("2");
		boss_anim.SetInteger("IdleToSkill", 4);
		checkSkill = true;

		dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
		direction = target.transform.position - transform.position;

		if (direction.magnitude < distance)
		{
			Debug.Log("�����ȿ� �ִ�.");
			if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
			{
				Debug.Log("�ڷ�ƾ ����.");
				StartCoroutine(PullAction(target, transform.position,0.5f));
			}
		}

		
	}

	public void Lightning()
	{
		Debug.Log("3");

		boss_anim.SetInteger("IdleToSkill", 1);
		checkSkill = true;
		////������������ ��� �÷��̾ ã�� ��
		Collider[] colls = Physics.OverlapSphere(transform.position, 15f, 1 << 8);  //8��° ���̾� = Player

		//�÷��̾ �������
		if (colls.Length != 0)
		{
			GameObject lightning = Instantiate(Lightning_Effect);
			int rand = Random.Range(0, colls.Length);
			Vector3 targetPos = colls[rand].gameObject.transform.position;

			DOTween.To(setter: value =>
			{
				if(value ==1)
				{
					DestroyObject(lightning);
				}
				Debug.Log(value);
				lightning.transform.position = EMath.Parabola(transform.position, targetPos, 10, value);
			}, startValue: 0, endValue: 1, duration: 1).SetEase(Ease.Linear);

			if (lightning.transform.position == targetPos)
				DestroyObject(lightning);

		}
		else
			Debug.Log("�ֺ��� �÷��̾ �����ϴ�.");
	}

	public void Thumderbolt()
	{
		Debug.Log("4");
		boss_anim.SetInteger("IdleToSkill", 3);
		checkSkill = true;
		StartCoroutine(ThumderboltAction(5.0f, transform.position.x, transform.position.z));

	}

	public void LaserFire()
	{
		Debug.Log("5");
		boss_anim.SetInteger("IdleToSkill", 2);
		checkSkill = true;

		GameObject laser = Instantiate(Laser_Effect);
		laser.transform.position = transform.position;

		laser.transform.LookAt(target.transform);

		

		StartCoroutine(LaserFireAction(laser,target.transform.position,3f));

	}

	public void SpeedDown()
	{
		Debug.Log("6");
		boss_anim.SetInteger("IdleToSkill", 1);
		checkSkill = true;

		//������������ ��� �÷��̾ ã�� ��
		Collider[] colls = Physics.OverlapSphere(transform.position, 5f, 1 << 8);  //8��° ���̾� = Player

		if (colls.Length != 0)
		{
			StartCoroutine(SpeedDownAction(2f, colls));
		}
		else
			Debug.Log("�ֺ��� �÷��̾ �����ϴ�.");
		
	}

	public void MonsterSummon()
	{

		Debug.Log("7");
		boss_anim.SetInteger("IdleToSkill", 3);
		checkSkill = true;

		StartCoroutine(MonsterSummonAction(10,transform.position.x,transform.position.z));
	}

	



	IEnumerator PullAction(GameObject target, Vector3 currentPos,float endtime)
	{
		float time = 0f;
		while(true)
		{
			time += Time.deltaTime;

			if (time >= endtime)
				yield break;


			target.transform.position = Vector3.MoveTowards(target.transform.position, currentPos, 0.05f);

			yield return null;
		}
	}

	IEnumerator ThumderboltAction(float endTime, float bossX, float bossZ)
	{
		float time = 0f;
		float skillTime = 0f;

		GameObject[] bolt = new GameObject[20];
		Vector3[] pos = new Vector3[20];
		int boltnum = 0;


		for(int i=0; i< bolt.Length; i++)
		{
			bolt[i] = Instantiate(Thunderbolt_Effect);

			float randX = Random.Range(bossX - 10f, bossX + 10f);
			float randZ = Random.Range(bossZ - 10f, bossZ + 10f);

			bolt[i].transform.position = new Vector3(randX, 15f , randZ);
			pos[i] = bolt[i].transform.position;
			pos[i].y = 0f;
			bolt[i].SetActive(false);
		}
	
		while (true)
		{
			time += Time.deltaTime;
			skillTime += Time.deltaTime;

			if (time >= endTime)
			{

				for(int i=0; i< bolt.Length; i++)
				{
					DestroyObject(bolt[i]);
				}

				yield break;
			}

			if(skillTime >= 0.25f && time <=5.0f)
			{
				bolt[boltnum].SetActive(true);  //������ ������ Ȱ��ȭ.
				skillTime = 0f;
				boltnum++;
			}

			for(int i=0;i< bolt.Length; i++)
			{

				if(bolt[i].activeSelf == true)
				{
					bolt[i].transform.position = Vector3.MoveTowards(bolt[i].transform.position, pos[i], 0.2f);
				}
				
				
				if(bolt[i].transform.position.y <= 0f)
				{
					bolt[i].SetActive(false);
				}
				
			}

			yield return null;
		}

		
	}

	IEnumerator LaserFireAction(GameObject laser,Vector3 playerPos,float endTime)
	{
		float time = 0f;

		while(true)
		{
			time += Time.deltaTime;

			if(time> endTime)
			{
				DestroyObject(laser);
				yield break;
			}

			laser.gameObject.transform.position = Vector3.MoveTowards(laser.gameObject.transform.position, playerPos, 0.3f);

			yield return null;
		}

		
	}

	IEnumerator SpeedDownAction(float endtime, Collider[] colls)
	{
		float time = 0f;

		while (true)
		{

			time += Time.deltaTime;

			if (time >= endtime)
			{
				//���ӽð��� ������ �ӵ��� ������� ����.
				for (int i = 0; i < colls.Length; i++)
				{
					colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = 8f;
				}
				yield break;
			}


			for (int i = 0; i < colls.Length; i++)
			{
				//float speed= colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed;
				//speed /= 2;
				//colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = speed;

				colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = 2f;
			}

			yield return null;
		}

	}


	IEnumerator MonsterSummonAction(int summonNum , float bossX, float bossZ)
	{

		GameObject[] monsters = new GameObject[summonNum];

		for (int i = 0; i < summonNum; i++)
		{
			float randX = Random.Range(bossX - 5f, bossX + 5f);
			float randZ = Random.Range(bossZ - 5f, bossZ + 5f);
			
			if(i>4)
				monsters[i] = Instantiate(EnemyPrefebs[0]);
			else
				monsters[i] = Instantiate(EnemyPrefebs[1]);


			monsters[i].transform.position = new Vector3(randX, 0, randZ);
		}

		yield return null;
	}

	// Start is called before the first frame update
	void Start()
    {

		boss_anim = transform.GetChild(0).GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
    {
		//����� Ȯ�ο�.
		dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
		direction = target.transform.position - transform.position;

		if (direction.magnitude < distance)
		{
			if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
				isCollision = true;
			else
				isCollision = false;
		}
		else
			isCollision = false;

		if (Input.GetKeyDown(KeyCode.Keypad2))
			Pull();

		if (Input.GetKeyDown(KeyCode.Keypad3))
			Lightning();

		if (Input.GetKeyDown(KeyCode.Keypad4))
			Thumderbolt();

		if (Input.GetKeyDown(KeyCode.Keypad5))
			LaserFire();

		if (Input.GetKeyDown(KeyCode.Keypad6))
			SpeedDown();

		if (Input.GetKeyDown(KeyCode.Keypad7))
			MonsterSummon();



		if(checkSkill)
		{
			checkTime += Time.deltaTime;
			if(checkTime > 1.2f)
			{
				checkSkill = false;
				checkTime = 0f;
				boss_anim.SetInteger("IdleToSkill", 0);
			}
		}

	}

#if UNITY_EDITOR
	//���信�� Ȯ�ο�
	private void OnDrawGizmos()
	{
		UnityEditor.Handles.color = isCollision ? _red : _blue;
		UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
		UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);
	}
#endif

}
