using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yggdrasil.PlayerSkillSet  //�÷��̾� ��ų ���� ����
{
	

	//�������̽�
	public interface ISkill
	{
		//�������ִ� Ÿ�Կ� ���� �׼ǿ��� ������ ��ų�� �޶�������
		public void SkillAction(AbilityType type);
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
		public static ISkill SkillTypeSet(SkillType type)
		{
			ISkill SkillSet = null;

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
	public class Attack : ISkill
	{
		

		public void SkillAction(AbilityType type)
		{
			switch (type)
			{
				case AbilityType.Damage:
					Action.Instance.AttackDamage();
					Debug.Log("����-������ ��ų");
					break;

				case AbilityType.Distance:
					Action.Instance.AttackDistance();
					Debug.Log("����-�Ÿ��� ��ų");
					break;

				case AbilityType.Speed:
					Action.Instance.AttackSpeed();
					Debug.Log("����-������ ��ų");
					break;

			}
		}
	}

	////////////////////////////////////////����/////////////////////////////////////
	public class Defense :ISkill
	{
		

		public void SkillAction(AbilityType type)
		{
			switch (type)
			{
				case AbilityType.Damage:
					Action.Instance.DefenseDamage();
					Debug.Log("���-������ ��ų");
					break;

				case AbilityType.Distance:
					Action.Instance.DefenseDistance();
					Debug.Log("���-�Ÿ��� ��ų");
					break;

				case AbilityType.Speed:
					Action.Instance.DefenseSpeed();
					Debug.Log("���-������ ��ų");
					break;
			}
		}
	}

	////////////////////////////////////////����/////////////////////////////////////
	public class Support : ISkill
	{
		

		public void SkillAction(AbilityType type)
		{
			switch (type)
			{
				case AbilityType.Damage:
					//����� ��ų ���೻�븸(������ ��ų ������ �׼��� �Լ�)
					Action.Instance.SupportDamage();
					//�����
					Debug.Log("����-������ ��ų");
					break;

				case AbilityType.Distance:
					Action.Instance.SupportDistance();
					Debug.Log("����-�Ÿ��� ��ų");
					break;

				case AbilityType.Speed:
					Action.Instance.SupportSpeed();
					Debug.Log("����-������ ��ų");
					break;
			}
		}
	}



}

