using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSkillSet;  //SkillFactory.cs -> PlayerSkillSet(namespace)




public class Action : MonoBehaviour
{
	
	public GameObject Spirit_Prefab;  //�ӽ�


	private SkillManager skillMgr;
	private static Action instance = null;
	

	public static Action Instance
	{
		get
		{
			if(null== instance)
			{
				return null;
			}

			return instance;
		}
	}


	GameObject FindEnemy(GameObject findStartObject,float distance) //��ó�� �ִ� �� ã�� �Լ�
	{
		float Dist = 0f;
		float near = 0f;
		GameObject nearEnemy = null;

		//���� ���� ���� ã�´�.
		Collider[] colls = Physics.OverlapSphere(findStartObject.transform.position, 10f, 1 << 9);  //9��° ���̾� = Enemy

		if (colls.Length == 0)
		{
			Debug.Log("������ ���� �����ϴ�.");
			DestroyObject(findStartObject);
			return null;
		}
		else
		{
			//���� �ִٸ� �� ���� �߿�
			for (int i = 0; i < colls.Length; i++)
			{

				//���ɰ��� �Ÿ��� �����
				Dist = Vector3.Distance(findStartObject.transform.position, colls[i].transform.position);

				if (i == 0)
				{
					near = Dist;
					nearEnemy = colls[i].gameObject;
				}

				//�� �Ÿ��� �۴ٸ� �Ÿ��� �����ϰ� �ش� ������Ʈ�� ����
				if (Dist < near)
				{
					near = Dist;
					nearEnemy = colls[i].gameObject;
				}
			}

			return nearEnemy;
		}

	}



	/**************�������� ��ų�� �ڷ�ƾ****************/

	#region Support Skill 

	public void Support_Damage()  //����-����
	{
		GameObject SupDmg_Spirit = Instantiate(Spirit_Prefab);

		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if (skillMgr.Support_Damage_Mgr(SupDmg_Spirit))
		{
			//������ �÷��̾� ��ġ�� �̵�.
			SupDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;

			//�ڷ�ƾ���� ����.
			StartCoroutine(Support_Damage_Skill(SupDmg_Spirit));
		}
		else
			Debug.Log("��ų�� ����� �� �����ϴ�.");
	}


	public void Support_Distance() //����-�Ÿ�
	{ 
		GameObject nearEnemy = null;
		GameObject SupDis_Spirit = Instantiate(Spirit_Prefab);
		//������ �÷��̾� ��ġ�� �̵�
		SupDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if (skillMgr.Support_Distance_Mgr(SupDis_Spirit))
		{
			//��ó�� ���� ã�´�
			nearEnemy = FindEnemy(SupDis_Spirit,15f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Support_Distance_Skill(SupDis_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(SupDis_Spirit);
			Debug.Log("��ų�� ����� �� �����ϴ�.");
		}
			
	}

	public void Support_Speed()  //����-�̵�
	{
		GameObject nearEnemy = null;
		GameObject SupSpeed_Spirit = Instantiate(Spirit_Prefab);
		//������ �÷��̾� ��ġ�� �̵�
		SupSpeed_Spirit.transform.position = PlayerManager.p_Object.transform.position;


		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if (skillMgr.Support_Speed_Mgr(SupSpeed_Spirit))
		{
			//��ó�� ���� ã�´�
			nearEnemy = FindEnemy(SupSpeed_Spirit,15f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Support_Speed_Skill(SupSpeed_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(SupSpeed_Spirit);
			Debug.Log("��ų�� ����� �� �����ϴ�.");
		}

	}
	#endregion

	#region Support_Skill_Coroutine 
	IEnumerator Support_Damage_Skill(GameObject target)        //����-���� ��ų
	{
		//2�ʵ��� ���� ���ӽ�ų ��ų���� 
		float sprit_range = 5f;  //����(��ų)�� �����ų ����
		float spirit_time = 0f;   //���� ���� �ð� ����

		while (true)
		{
			spirit_time += Time.deltaTime;

			//�������� ���� ã�Ƽ� 
			Collider[] colls = Physics.OverlapSphere(target.transform.position, sprit_range, 1 << 9);  //9��° ���̾� = Enemy

			foreach (var rangeCollider in colls)
			{
				//���������� �����ų ����� ����

				//�����
				Debug.Log(rangeCollider.ToString());


			}

			//���� ���ӽð��� ����� 
			if (spirit_time > 2f)
			{
				//������Ʈ �ı��� �ڷ�ƾ ����
				DestroyObject(target);
				yield break;
			}


			yield return null;
		}
	}

	IEnumerator Support_Distance_Skill(GameObject Spirit,GameObject target) //����-�Ÿ� ��ų
	{
		float pull_range = 5f;  //���������
		float pull_time = 0f; //������ �ð� üũ ����.

		//���� ����� ������ �̵� ��
		while (true)
		{
			Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position, 0.1f);

			if(Spirit.transform.position == target.transform.position)
				break;

			yield return null;
		}

		//������������ ��� ���ʹ�(���鸸)�� �ڽ��� ��ġ�� ��� ��
		Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, pull_range, 1 << 9);  //9��° ���̾� = Enemy


		while (true)
		{
			pull_time += Time.deltaTime;

			foreach (var rangeCollider in colls)
			{
				//������ �ƴ϶��
				if(rangeCollider.gameObject.name != "Boss")
				{
					rangeCollider.gameObject.transform.position = Vector3.MoveTowards(rangeCollider.gameObject.transform.position, Spirit.transform.position, 0.05f);
				}
			}

			if (pull_time >= 0.75f)
				break;


			yield return null;
		}

		//�ش� ���ʹ��� �̵��ӵ��� 0(���� ȿ��)�� �����.

		//foreach (var rangeCollider in colls)
		//{
			//rangeCollider.gameObject  -> ���� ������Ʈ�� ���ͼ� ���Ⱥκ��� ������ �̵��ӵ��� �����ð�(0.75)���� 0���� �����.
		//}

		DestroyObject(Spirit);
		yield return null;

	}

	IEnumerator Support_Speed_Skill(GameObject Spirit, GameObject target) //����-�̵� ��ų
	{
		float sprit_range = 15f;  //����(��ų)�� �����ų ����
		float spirit_time = 0f;   //���� ���� �ð� ����

		//���� ����� ������ �̵� ��
		while (true)
		{
			Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position, 0.1f);

			if (Spirit.transform.position == target.transform.position)
				break;

			yield return null;
		}


		while (true)
		{
			spirit_time += Time.deltaTime;

			if (spirit_time >= 2f)
			{
				DestroyObject(Spirit);
				yield break;
			}
				

			//�������� ���� ã�´�.
			Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, sprit_range, 1 << 9);  //9��° ���̾� = Enemy

			foreach (var rangeCollider in colls)
			{
				//���⼭ �̵��ӵ� ���� �ϴ� ���� �߰�.

				//�����
				Debug.Log($"{rangeCollider.name}�� �̵��ӵ��� -50% �����մϴ�.");
			}


			yield return null;
		}

	}

	#endregion

	/**********************************************/



	#region Attack Skill
	public void Attack_Damage()
	{
		GameObject nearEnemy = null;
		GameObject AtkDis_Spirit = Instantiate(Spirit_Prefab);
		//������ �÷��̾� ��ġ�� �̵�
		AtkDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if (skillMgr.Attack_Damage_Mgr(AtkDis_Spirit))
		{
			//��ó�� ���� ã�´�
			nearEnemy = FindEnemy(AtkDis_Spirit,15f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Attack_Damage_Skill(AtkDis_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(AtkDis_Spirit);
			Debug.Log("��ų�� ����� �� �����ϴ�.");
		}


		// ���� �������� Ȯ�ι޴� ����

		//�������� ������ �ް� ��ų����� �����ϴٸ�
	}

	public void Attack_Distance()
	{
		GameObject nearEnemy = null;
		GameObject AtkDmg_Spirit = Instantiate(Spirit_Prefab);
		//������ �÷��̾� ��ġ�� �̵�
		AtkDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;


		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if (skillMgr.Attack_Distance_Mgr(AtkDmg_Spirit))
		{
			//��ó�� ���� ã�´�
			nearEnemy = FindEnemy(AtkDmg_Spirit,10f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Attack_Distance_Skill(AtkDmg_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(AtkDmg_Spirit);
			Debug.Log("��ų�� ����� �� �����ϴ�.");
		}

	}

	public void Attack_Speed()
	{
		GameObject nearEnemy = null;
		GameObject AtkSpeed_Spirit = Instantiate(Spirit_Prefab);
		//������ �÷��̾� ��ġ�� �̵�
		AtkSpeed_Spirit.transform.position = PlayerManager.p_Object.transform.position;


		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if (skillMgr.Attack_Speed_Mgr(AtkSpeed_Spirit))
		{
			//��ó�� ���� ã�´�
			nearEnemy = FindEnemy(AtkSpeed_Spirit, 10f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Attack_Speed_Skill(AtkSpeed_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(AtkSpeed_Spirit);
			Debug.Log("��ų�� ����� �� �����ϴ�.");
		}

	}


	#endregion



	#region Attack_Skill_Coroutine

	IEnumerator Attack_Damage_Skill(GameObject Spirit, GameObject target)
	{
		float attack_range = 5f;  //���ݹ���
		//float attack_time = 0f; //���� �ð�.

		int randvalue = 0;  //������
		

		//���� ����� ������ �̵� ��
		while (true)
		{
			Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position, 0.1f);

			if (Spirit.transform.position == target.transform.position)
				break;

			yield return null;
		}

		//������������ ��� ���ʹ�(���鸸)�� ã�� ��
		Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, attack_range, 1 << 9);  //9��° ���̾� = Enemy

		

		for(int i=0; i<10; i++)
		{
			randvalue = Random.Range(0, colls.Length);

			//���� ��󿡰� ���ظ� ������ 20% ���ҵ� ���ظ� �ݴϴ�.
			Debug.Log($"{colls[randvalue].name}���� ������� �ݴϴ�");

			//yield return null;
		}

		DestroyObject(Spirit);

		yield return null;
	}


	IEnumerator Attack_Distance_Skill(GameObject Spirit, GameObject target)
	{
		float attack_range = 5f;  //���ݹ���
		float attack_time = 0f;  //���� �ð�
		float check_time = 0f;  //���� ���� �ð�

		//�÷��̾ ���ֺ��� ������ ������ ��ġ��(6f) ���Ѵ�.
		var heading = target.transform.position - Spirit.transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		direction.y = 0;
		direction *= 6f;




		//���� ����� ������ �̵� ��
		while (true)
		{
			//���ݽð�üũ
			attack_time += Time.deltaTime;

			//�Ҹ�ð�üũ
			check_time += Time.deltaTime;


			if (check_time >= 2.2f)
			{
				//�Ҹ� �ð�
				yield return new WaitForSeconds(0.3f);
				DestroyObject(Spirit);
				yield break;
			}
				
			if (attack_time >= 0.5f)
			{
				attack_time = 0f;

				Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position+direction, 1.5f);

				//������������ ��� ���ʹ�(���鸸)�� �ڽ��� ��ġ�� ��� ��
				Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, attack_range, 1 << 9);  //9��° ���̾� = Enemy

				foreach(var rangeCollider in colls)
				{
					//����� �ڵ�
					Debug.Log($"{rangeCollider.name}���� ���ظ� �ݴϴ�.");
				}

			}

			yield return null;
		}

	}


	IEnumerator Attack_Speed_Skill(GameObject Spirit, GameObject target)
	{

		float attack_range = 3f;  //���ݹ���
		float attack_time = 0f;  //���� �ð�
		float check_time = 0f;  //���� ���� �ð�

		//���� ����� ������ �����̵���
		Spirit.transform.position = target.transform.position;



		// 0.33�ʿ� �ѹ��� 3�ʵ��� �����Ǵ� ������ �����Ѵ�.

		while(true)
		{

			attack_time += Time.deltaTime;
			check_time += Time.deltaTime;


			if (check_time >= 3.2f)
			{
				//�Ҹ� �ð�
				yield return new WaitForSeconds(0.3f);
				DestroyObject(Spirit);
				yield break;
			}



			if (attack_time >= 0.33f)
			{
				attack_time = 0f;


				//������������ ��� ���ʹ�(���鸸)�� �ڽ��� ��ġ�� ��� ��
				Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, attack_range, 1 << 9);  //9��° ���̾� = Enemy



				foreach (var rangeCollider in colls)
				{
					//����� �ڵ�
					Debug.Log($"{rangeCollider.name}���� ���� ���ظ� �ݴϴ�.");
				}

			}


			yield return null;
		}

		
	}


	#endregion

	#region Defense Skill
	public void Defense_Damage()
	{
		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)

		// ���� �������� Ȯ�ι޴� ����

		//�������� ������ �ް� ��ų����� �����ϴٸ�
	}

	public void Defense_Distance()
	{

	}

	public void Defense_Speed()
	{

	}
	#endregion


	private void Awake()
	{
		if(null == instance)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		skillMgr = new SkillManager();
	}


	// Start is called before the first frame update
	void Start()
	{
	
	}


	// Update is called once per frame
	void Update()
	{

	}
}
