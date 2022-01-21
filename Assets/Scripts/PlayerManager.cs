using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerSkillSet;

public class PlayerManager : MonoBehaviour
{
	//�÷��̾� ������Ʈ
	public static GameObject p_Object;

	//����
	public Yggdrasil.CharacterStats p_Status;
	

	//��ų
	private Skill skill;


	public Text SkillType_txt;
	private int skillType_num;
	// �̺�Ʈ ����
	//private TempCodes.TempMessageSystem eSystem;
	//private MoveEvent me;
	//private Handler player;



	private void InputCheck()
	{

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			
			skillType_num++;
			skillType_num %= 3;

			var type_num = (SkillType)skillType_num;

			switch (type_num)
			{
				case SkillType.Attack:
					skill = SkillFactory.SkillTypeSet(type_num);
					SkillType_txt.text = "Attack";
					break;
				case SkillType.Defense:
					skill = SkillFactory.SkillTypeSet(type_num);
					SkillType_txt.text = "Defense";
					break;
				case SkillType.Support:
					skill = SkillFactory.SkillTypeSet(type_num);
					SkillType_txt.text = "Support";
					break;
			}

		}


		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//��ų�� ����Ҽ� �ִ��� 1�� ����(����,��Ÿ�� �� üũ)

			//1�����˿��� ����Ǹ� ��ų�ߵ�
			skill.SkillAction(AbilityType.Damage);
			Debug.Log("1");
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			//��ų�� ����Ҽ� �ִ��� 1�� ����(����,��Ÿ�� �� üũ)

			skill.SkillAction(AbilityType.Distance);
			Debug.Log("2");
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			//��ų�� ����Ҽ� �ִ��� 1�� ����(����,��Ÿ�� �� üũ)

			skill.SkillAction(AbilityType.Speed);
			Debug.Log("3");
		}


	}



	private void Move()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(h, 0, v) * p_Status.MoveSpeed * Time.deltaTime);
	}
	

	// Start is called before the first frame update
	void Start()
    {
		p_Object = this.gameObject;
		p_Status = new Yggdrasil.CharacterStats();

		

		//ĳ���� �ʱ����
		p_Status.MoveSpeed = 7f; //�⺻���ǵ�

		skillType_num = 0;
		skill = SkillFactory.SkillTypeSet(SkillType.Attack);
	}

    // Update is called once per frame
    void Update()
    {
		//�̵�
		Move();

		//Ű���� �Է� üũ �Լ�.
		InputCheck();

	}
}
