using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSkillSet;  //SkillFactory.cs -> PlayerSkillSet(namespace)




public class Action : MonoBehaviour
{
	public PlayerManager pm = new PlayerManager();
	public GameObject target;
	public GameObject player;

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

	#region Support Skill 
	public void Support_Damage()
	{
		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)
		if(skillMgr.Support_Damage_Mgr(target))
		{
			//���⼭    target.transform.position = pm.transform.position; ������.....(����?)
			target.transform.position = player.transform.position;
		}

		

		//�������� ������ �ް� ��ų����� �����ϴٸ�
	}
	
	public void Support_Distance()
	{

	}

	public void Support_Speed()
	{

	}
	#endregion

	#region Attack Skill
	public void Attack_Damage()
	{
		//Trigger(����üũ) -> ������ �����͸� ������ Ȯ�ι���(�켱 �Ŵ����θ�)

		// ���� �������� Ȯ�ι޴� ����

		//�������� ������ �ް� ��ų����� �����ϴٸ�
	}

	public void Attack_Distance()
	{

	}

	public void Attack_Speed()
	{

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
