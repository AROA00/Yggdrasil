using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossFSM : MonoBehaviour
{

	private Yggdrasil.BossManager M_BossInfo;

	private float time;
	private int actionPoint;
	//private bool actionCheck=false;
	private bool spCheck = false;


	public enum BossState
	{
		NOMAL,      //�Ϲ�(�����̻� �ɸ��� ���� ���� ����),���(SP�� ȸ���ϴ� ����)
		IDLE,       //�ൿ�� ��ȯ�� ��ٸ��� ����(����,�̵�,��ų)


		//�ൿ�� ��ȭ ����
		BUFF,       //���� ��ų�� ����ϴ� ����
		MOVE,       //�̵�
		BATTLE,     //����


		//���������� 
		DETECTION,   //Ž��
		ATTACK,     //����(��ų�� ����ϴ� ����)
		AGGRO,      //��׷�
		
		//�ܺ��� ����(� ���¿����� �ٷ� �����Ҽ� �ֵ���)
		CC,         //�����̻�(�����������)
		DIE,        //����
		HIT         //�ǰ�
	}

	private BossState bossState;


	void StateNomal()
	{
		//������ SP�� 100%��� ���� �ൿ�� ����.
		if (M_BossInfo.GetTableExcel().MaxTM >= actionPoint)
		{
			//sp�������� ��ä��������.
			spCheck = true;

			bossState = BossState.IDLE;

			Debug.Log("������ SP �������� ������ ���¸� IDLE�� ��ȯ�մϴ�.");
		}
		else
		{
			//�ִϸ��̼� �⺻��� ����.
		}
	}

	void StateIdle()
	{

		int rand = Random.Range(0, 3);

		switch (rand)
		{
			case 0:
				if(actionPoint > 100)        //�̵��� �ʿ��� SP�������� ������. ������ -> �ٽ� �̴´�.
					bossState = BossState.BUFF;
				break;
			case 1:
				if (actionPoint > 200)
					bossState = BossState.MOVE;
				break;
			case 2:
				if (actionPoint > 500)
					bossState = BossState.BATTLE;
				break;
		}
	}


	// Start is called before the first frame update
	void Start()
	{
		M_BossInfo = new Yggdrasil.BossManager(21001);

		bossState = BossState.NOMAL;

		Debug.Log($"BossFSM: ������ ������ �̸��� \"{M_BossInfo.GetTableExcel().Name_KR}\" �Դϴ�.");
	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;

		if (time > 1.0f && !spCheck)
		{
			actionPoint += M_BossInfo.GetTableExcel().Speed;

			if (actionPoint > M_BossInfo.GetTableExcel().MaxTM)
			{
				int a = actionPoint - M_BossInfo.GetTableExcel().MaxTM;

				//MaxTM�� �ѱ� �ʰ�.
				actionPoint -= a;

			}
		}

		switch (bossState)
		{
			case BossState.NOMAL:
				//�� ���¿����� �¾Ƶ� ������ ����(�׷α�? ��������) -> SP�� ä��� �ִ��� == �÷��̾��� ����Ÿ�̹�.
				StateNomal();
				break;
			case BossState.CC:

				break;
			case BossState.DIE:

				break;
			case BossState.IDLE:
				//SP�� �� ä���� �ְų� �����ִ� ����, �ൿ�� ��ȭ�� ����ϴ� ���� -> �̶� �ǰ� �Ǹ� �������·� �ٷ� ����.  
				StateIdle();
				break;

			case BossState.MOVE:
				
				break;
			case BossState.BUFF:
				//�������� ������ ����(��������,ü��ȸ��,���ݷ��������)
				break;
			case BossState.BATTLE:
				//���� ���� ->
				break;

			case BossState.ATTACK:
				//������ ���ݻ���(�⺻ ����,��ų��Ÿ�ӿ� ���� ��ų?)
				break;
			case BossState.AGGRO:

				break;
			case BossState.HIT:
				
				break;
			case BossState.DETECTION:
				//��Ʋ ���¿��� ���� �ִ��� ã�»���(?) -> ����? �޼���� �������°� �´°� ������.
				break;
		}


	}
}
