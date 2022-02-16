using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yggdrasil.PlayerSkillSet;  //SkillFactory.cs -> PlayerSkillSet(namespace)





public class Action : MonoBehaviour
{

	public enum SkillList
	{
		AttackDmg = 11,
		AttackDis,
		AttackSpd,

		DefenseDmg = 21,
		DefenseDis,
		DefenseSpd,

		SupportDmg = 31,
		SupportDis,
		SupportSpd

	}


	private static Action instance = null;




	public GameObject SpiritPrefab;  //�ӽ�
	//private SkillManager skillMgr;
	private Vector3 direction;




	public static Action Instance
	{
		get
		{
			if (null == instance)
			{
				return null;
			}

			return instance;
		}
	}

	GameObject FindNearbyEnemy(GameObject findStartObject, float distance)
	{
		float Dist = 0f;
		float near = 0f;
		GameObject nearEnemy = null;

		//���� ���� ���� ã�´�.
		Collider[] colls = Physics.OverlapSphere(findStartObject.transform.position, distance, 1 << 9);  //9��° ���̾� = Enemy

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


	IEnumerator InstallSprit(GameObject spirit, float duration, float range, SkillList skill)
	{
		float spirit_time = 0f;

		//Defense-dis
		float attack_time = 0f;

		//Defen-spd
		bool range_check = true;

		//All 
		Collider[] colls = null;

		while (true)
		{
			//���ӽð� üũ
			spirit_time += Time.deltaTime;

			//���� ���ӽð��� ����� 
			if (spirit_time >= duration)
			{
				//���� ���� �ڵ�

				//���� �ı��� �ڷ�ƾ ����
				DestroyObject(spirit);
				yield break;
			}

			switch (skill)
			{
				case SkillList.DefenseDmg:
					{
						//�������� �÷��̾ ã�Ƽ� 
						colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 8);  //8��° ���̾� = Player
						foreach (var rangeCollider in colls)
						{
							//���������� �����ų ���� ����

							//�����
							Debug.Log($"{rangeCollider.name}�� �Դ� ���ذ� 70% �����մϴ�.");
						}
					}
					break;

				case SkillList.DefenseDis:
					{
						attack_time += Time.deltaTime;

						if (attack_time >= 0.8f && attack_time < 1.0f)
						{
							attack_time = 0f;

							//�������� ���� ã�Ƽ� 
							colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy
							foreach (var rangeCollider in colls)
							{
								//�и��� ��ü�� �и��� ������ ������ �Ÿ�(��)�� ���ϰ�

								//�÷��̾�� �и��� ��ü������ ������ ���ؼ�
								var heading = rangeCollider.transform.position - spirit.transform.position;
								heading.y = 0f;
								heading *= range;

								//������ �ƴ϶��
								if (rangeCollider.gameObject.name != "Boss")
								{
									rangeCollider.gameObject.transform.position = Vector3.MoveTowards(rangeCollider.gameObject.transform.position, heading, 1.5f);
									Debug.Log($"{rangeCollider.ToString()}�� ���� ������ �о���ϴ�.");
								}
							}

						}
					}
					break;

				case SkillList.DefenseSpd:
					{
						//�������� �÷��̾ ã�Ƽ� 
						if (range_check)
						{
							colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy
							range_check = false;
						}


						foreach (var rangeCollider in colls)
						{
							//���������� �����ų ���� ����

							//�����
							Debug.Log($"{rangeCollider.ToString()}�� �̵��ӵ��� 0����(����ȿ��)�� �ݴϴ�.");
						}
					}
					break;

				case SkillList.SupportDmg:
					{
						//�������� ���� ã�Ƽ� 
						colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy
						foreach (var rangeCollider in colls)
						{
							//���������� �����ų ����� ����

							//�����
							Debug.Log($"{rangeCollider.name}�� �Դ� ���ذ� 20% �����մϴ�.");
						}
					}
					break;

			}

			yield return null;
		}

	}

	IEnumerator TrackingSpirit(GameObject spirit, GameObject nearbyEnemy, float duration, float range, SkillList skill)
	{

		float spirit_time = 0f;

		//Attack-dis,Attack-spd
		float attack_time = 0f;


		if (skill != SkillList.AttackDis)
		{
			//������ �̵�.
			while (true)
			{
				spirit.transform.position = Vector3.MoveTowards(spirit.transform.position, nearbyEnemy.transform.position, 0.1f);

				if (spirit.transform.position == nearbyEnemy.transform.position)
					break;

				yield return null;
			}
		}



		while (true)
		{
			spirit_time += Time.deltaTime;

			if (spirit_time >= duration)
			{
				DestroyObject(spirit);
				yield break;
			}

			switch (skill)
			{
				case SkillList.AttackDmg:
					{
						int randvalue = 0;

						//������������ ��� ���ʹ�(���鸸)�� ã�� ��
						Collider[] colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy

						for (int i = 0; i < 10; i++)
						{
							randvalue = Random.Range(0, colls.Length);

							//���� ��󿡰� ���ظ� ������ 20% ���ҵ� ���ظ� �ִ� �ڵ� �߰�.
							Debug.Log($"{colls[randvalue].name}���� ������� �ݴϴ�");

						}
						DestroyObject(spirit);
						yield break;

					}
				case SkillList.AttackDis:
					{
						attack_time += Time.deltaTime;

						if (attack_time >= 0.5f)
						{
							attack_time = 0f;

							spirit.transform.position = Vector3.MoveTowards(spirit.transform.position, nearbyEnemy.transform.position + direction, 1.5f);

							Collider[] colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy

							foreach (var rangeCollider in colls)
							{
								//����� �ڵ�
								Debug.Log($"{rangeCollider.name}���� ���ظ� �ݴϴ�.");
							}

						}


					}
					break;
				case SkillList.AttackSpd:
					{
						attack_time += Time.deltaTime;

						if (attack_time >= 0.33f)
						{
							attack_time = 0f;
							//������������ ��� ���ʹ̸� ã�� ��
							Collider[] colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy

							foreach (var rangeCollider in colls)
							{
								//����� �ڵ�
								Debug.Log($"{rangeCollider.name}���� ���� ���ظ� �ݴϴ�.");
							}

						}
					}
					break;
				case SkillList.SupportDis:
					{
						if (spirit_time < 0.75f)
						{
							Collider[] colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy
							foreach (var rangeCollider in colls)
							{
								//������ �ƴ϶�� + ��ġ�� ���� �ʴٸ� �� ����� ����´�.
								if (rangeCollider.gameObject.name != "Boss" && rangeCollider.transform.position != spirit.transform.position)
								{
									rangeCollider.gameObject.transform.position = Vector3.MoveTowards(rangeCollider.gameObject.transform.position, spirit.transform.position, 0.05f);
								}
							}
						}


						if (spirit_time >= 0.75f)
						{
							//������ �ð����� �̵��ӵ� 0���� ����� �ڵ�.
							Collider[] colls = Physics.OverlapSphere(spirit.transform.position, 0.5f, 1 << 9);  //9��° ���̾� = Enemy
							foreach (var rangeCollider in colls)
							{
								Debug.Log($"{rangeCollider.name}�� ���Ͽ� �ɷȽ��ϴ�.");
							}
						}
					}

					break;
				case SkillList.SupportSpd:
					{
						//�������� ���� ã�´�.
						Collider[] colls = Physics.OverlapSphere(spirit.transform.position, range, 1 << 9);  //9��° ���̾� = Enemy
						foreach (var rangeCollider in colls)
						{
							//���⼭ �̵��ӵ� ���� �ϴ� ���� �߰�.

							//�����
							Debug.Log($"{rangeCollider.name}�� �̵��ӵ��� -50% �����մϴ�.");
						}

					}
					break;
			}

			yield return null;
		}


	}


	#region SupportSkill 
	public void SupportDamage()  //����-����
	{
		GameObject SupDmg_Spirit = Instantiate(SpiritPrefab);

		//������ �÷��̾� ��ġ�� �̵�.  -> ���߿� �����ִ� Ÿ�� �߾ӿ� ��ġ���Ѿ���(�̹� Ÿ���߾ӿ� ������ ������� ��ġ�Ұ�)
		SupDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		StartCoroutine(InstallSprit(SupDmg_Spirit, 2f, 5f, SkillList.SupportDmg));
	}


	public void SupportDistance() //����-�Ÿ�
	{
		GameObject nearEnemy = null;
		GameObject SupDis_Spirit = Instantiate(SpiritPrefab);
		//������ �÷��̾� ��ġ�� �̵�
		SupDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//��ó�� ���� ã�´�
		nearEnemy = FindNearbyEnemy(SupDis_Spirit, 15f);

		if (nearEnemy == null)
			return;
		else
			StartCoroutine(TrackingSpirit(SupDis_Spirit, nearEnemy, 1.5f, 5f, SkillList.SupportDis));
	}

	public void SupportSpeed()  //����-�̵�
	{
		GameObject nearEnemy = null;
		GameObject SupSpeed_Spirit = Instantiate(SpiritPrefab);
		//������ �÷��̾� ��ġ�� �̵�
		SupSpeed_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//��ó�� ���� ã�´�
		nearEnemy = FindNearbyEnemy(SupSpeed_Spirit, 15f);

		if (nearEnemy == null)
			return;
		else
			StartCoroutine(TrackingSpirit(SupSpeed_Spirit, nearEnemy, 2f, 15f, SkillList.SupportSpd));

	}
	#endregion

	#region AttackSkill
	public void AttackDamage()
	{
		GameObject nearEnemy = null;
		GameObject AtkDis_Spirit = Instantiate(SpiritPrefab);
		//������ �÷��̾� ��ġ�� �̵�
		AtkDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//��ó�� ���� ã�´�
		nearEnemy = FindNearbyEnemy(AtkDis_Spirit, 15f);

		if (nearEnemy == null)
			return;
		else
			StartCoroutine(TrackingSpirit(AtkDis_Spirit, nearEnemy, 0.1f, 5f, SkillList.AttackDmg));

	}

	public void AttackDistance()
	{
		GameObject nearEnemy = null;
		GameObject AtkDmg_Spirit = Instantiate(SpiritPrefab);
		//������ �÷��̾� ��ġ�� �̵�
		AtkDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//��ó�� ���� ã�´�
		nearEnemy = FindNearbyEnemy(AtkDmg_Spirit, 10f);

		if (nearEnemy == null)
			return;
		else
		{
			var heading = nearEnemy.transform.position - AtkDmg_Spirit.transform.position;
			var distance = heading.magnitude;

			direction = heading / distance;
			direction.y = 0;
			direction *= 6f;

			StartCoroutine(TrackingSpirit(AtkDmg_Spirit, nearEnemy, 2.2f, 5f, SkillList.AttackDis));
		}

	}


	public void AttackSpeed()
	{
		GameObject nearEnemy = null;
		GameObject AtkSpeed_Spirit = Instantiate(SpiritPrefab);
		//������ �÷��̾� ��ġ�� �̵�
		AtkSpeed_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//��ó�� ���� ã�´�
		nearEnemy = FindNearbyEnemy(AtkSpeed_Spirit, 10f);

		AtkSpeed_Spirit.transform.position = nearEnemy.transform.position;

		if (nearEnemy == null)
			return;
		else
			StartCoroutine(TrackingSpirit(AtkSpeed_Spirit, nearEnemy, 3.2f, 3, SkillList.AttackSpd));

	}

	#endregion

	#region DefenseSkill
	public void DefenseDamage()
	{
		GameObject DefDmg_Spirit = Instantiate(SpiritPrefab);

		//������ �÷��̾� ��ġ�� �̵�.  -> ���߿� �����ִ� Ÿ�� �߾ӿ� ��ġ���Ѿ���(�̹� Ÿ���߾ӿ� ������ ������� ��ġ�Ұ�)
		DefDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		StartCoroutine(InstallSprit(DefDmg_Spirit, 1.5f, 3f, SkillList.DefenseDmg));
	}

	public void DefenseDistance()
	{
		GameObject DefDis_Spirit = Instantiate(SpiritPrefab);

		//������ �÷��̾� ��ġ�� �̵�.  -> ���߿� �����ִ� Ÿ�� �߾ӿ� ��ġ���Ѿ���(�̹� Ÿ���߾ӿ� ������ ������� ��ġ�Ұ�)
		DefDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		StartCoroutine(InstallSprit(DefDis_Spirit, 3.2f, 5f, SkillList.DefenseDis));
	}

	public void DefenseSpeed()
	{
		GameObject DefSpd_Spirit = Instantiate(SpiritPrefab);

		//������ �÷��̾� ��ġ�� �̵�.  -> ���߿� �����ִ� Ÿ�� �߾ӿ� ��ġ���Ѿ���(�̹� Ÿ���߾ӿ� ������ ������� ��ġ�Ұ�)
		DefSpd_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		StartCoroutine(InstallSprit(DefSpd_Spirit, 2.0f, 5f, SkillList.DefenseSpd));
	}
	#endregion


	private void Awake()
	{
		if (null == instance)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		//skillMgr = new SkillManager();
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
