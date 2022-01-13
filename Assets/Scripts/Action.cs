using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Action : MonoBehaviour
{

	//�ý���
	TempCodes.TempMessageSystem eSystem;


	//�̺�Ʈ ���
	AttackEvent ae;
	DamageEvent de;
	MoveEvent me;


	//�ڵ鷯 ���
	AttackHandler atkHandler;
	DamageHandler dmgHandler;
	MoveHandler moveHandler;


	// Start is called before the first frame update
	void Start()
	{

		//���� 

		// 1.�ý��� ����
		eSystem = new TempCodes.TempMessageSystem();

		// 2.�̺�Ʈ ����
		ae = new AttackEvent();
		de = new DamageEvent();
		me = new MoveEvent();


		// 3.�ڵ鷯 ����
		atkHandler = new AttackHandler();
		dmgHandler = new DamageHandler();
		moveHandler = new MoveHandler();


		// 4.�̺�Ʈ ����Ʈ�� �߰�
		eSystem.AddEvent(ae);
		eSystem.AddEvent(de);
		eSystem.AddEvent(me);

		// 5.Ÿ�԰� �̺�Ʈ ��Ī
		eSystem.AddEventdic(ae);
		eSystem.AddEventdic(de);
		eSystem.AddEventdic(me);


		// 6.���ϴ� �̺�Ʈ�� ���� �ڵ鷯�� �߰�(����) �� ����
		eSystem.Subscribe<AttackEvent>(atkHandler);
		eSystem.Subscribe<DamageEvent>(dmgHandler);
		eSystem.Subscribe<MoveEvent>(moveHandler);




	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//���� ����� ����Ÿ �ʱ�ȭ.
			eSystem.ResetData();
		}


		// ������
		if (Input.GetKeyDown(KeyCode.Space))
		{
			eSystem.Notify();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			eSystem.Subscribe<AttackEvent>(moveHandler);
			eSystem.Notify(ae);
			eSystem.Unsubscribe<AttackEvent>(moveHandler);
		}


	}
}
