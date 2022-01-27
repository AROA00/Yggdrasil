using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yggdrasil.PlayerSkillSet  //�÷��̾� ��ų ���� ����
{
	


	//�߻� ���丮 ����
	public abstract class Skill
	{
		//�������ִ� Ÿ�Կ� ���� �׼ǿ��� ������ ��ų�� �޶�������
		public abstract void SkillAction(AbilityType type);
	}

	//�ߵ��� ��ų�� �ɷ�
	public enum AbilityType
	{
		Distance,     //�Ÿ�����
		Speed,        //�̵�����
		Damage        //���ذ���
	}

	public enum SkillType
	{
		Attack,     //������ Ÿ��
		Defense,    //����� Ÿ��
		Support     //������ Ÿ��
	}

	public class SkillFactory
	{
		public static PlayerSkillSet.Skill SkillTypeSet(SkillType type)
		{
			PlayerSkillSet.Skill SkillSet = null;

			switch (type)
			{
				case SkillType.Attack:
					SkillSet = new Attack();
					break;
				case SkillType.Defense:
					SkillSet = new Defense();
					break;
				case SkillType.Support:
					SkillSet = new Support();
					break;
					//����ó��
			}
			return SkillSet;

		}

	}

	////////////////////////////////////////����/////////////////////////////////////
	public class Attack : PlayerSkillSet.Skill
	{
		

		public override void SkillAction(PlayerSkillSet.AbilityType type)
		{
			switch (type)
			{
				case PlayerSkillSet.AbilityType.Damage:
					Action.Instance.AttackDamage();
					Debug.Log("����-������ ��ų");
					break;

				case PlayerSkillSet.AbilityType.Distance:
					Action.Instance.AttackDistance();
					Debug.Log("����-�Ÿ��� ��ų");
					break;

				case PlayerSkillSet.AbilityType.Speed:
					Action.Instance.AttackSpeed();
					Debug.Log("����-������ ��ų");
					break;

			}
		}
	}

	////////////////////////////////////////����/////////////////////////////////////
	public class Defense : PlayerSkillSet.Skill
	{
		

		public override void SkillAction(PlayerSkillSet.AbilityType type)
		{
			switch (type)
			{
				case PlayerSkillSet.AbilityType.Damage:
					Action.Instance.DefenseDamage();
					Debug.Log("���-������ ��ų");
					break;

				case PlayerSkillSet.AbilityType.Distance:
					Action.Instance.DefenseDistance();
					Debug.Log("���-�Ÿ��� ��ų");
					break;

				case PlayerSkillSet.AbilityType.Speed:
					Action.Instance.DefenseSpeed();
					Debug.Log("���-������ ��ų");
					break;
			}
		}
	}

	////////////////////////////////////////����/////////////////////////////////////
	public class Support : PlayerSkillSet.Skill
	{
		

		public override void SkillAction(PlayerSkillSet.AbilityType type)
		{
			switch (type)
			{
				case PlayerSkillSet.AbilityType.Damage:
					//����� ��ų ���೻�븸(������ ��ų ������ �׼��� �Լ�)
					Action.Instance.SupportDamage();
					//�����
					Debug.Log("����-������ ��ų");
					break;

				case PlayerSkillSet.AbilityType.Distance:
					Action.Instance.SupportDistance();
					Debug.Log("����-�Ÿ��� ��ų");
					break;

				case PlayerSkillSet.AbilityType.Speed:
					Action.Instance.SupportSpeed();
					Debug.Log("����-������ ��ų");
					break;
			}
		}
	}



}

