using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamageEvent : TempCodes.IEvent
{
	public void Dispose()
	{
		throw new System.NotImplementedException();
	}
}

public class MoveEvent : TempCodes.IEvent
{
	public void Dispose()
	{
		throw new System.NotImplementedException();
	}
}

public class AttackEvent : TempCodes.IEvent
{

	public void Dispose()
	{
		throw new System.NotImplementedException();
	}
}


public class AttackHandler : TempCodes.IEventHandler
{


	public void OnNotify(TempCodes.IEvent e)
	{

		Debug.Log($"{e.ToString()} ����");

		//���ݿ����� ó��
	}
}


public class MoveHandler : TempCodes.IEventHandler
{

	public void OnNotify(TempCodes.IEvent e)
	{

		Debug.Log($"{e.ToString()} �̵�");

		//�̵������� ó��
	}

}

public class DamageHandler : TempCodes.IEventHandler
{

	public void OnNotify(TempCodes.IEvent e)
	{

		Debug.Log($"{e.ToString()} �����");

		//����������� ó��
	}

}



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
		//�ý��� ����
		eSystem = new TempCodes.TempMessageSystem();

		//�̺�Ʈ ����
		ae = new AttackEvent();
		de = new DamageEvent();
		me = new MoveEvent();




		//�ڵ鷯 ����
		atkHandler = new AttackHandler();
		dmgHandler = new DamageHandler();
		moveHandler = new MoveHandler();


		//�̺�Ʈ ����Ʈ�� �̺�Ʈ ���
		eSystem.AddEvent(ae);
		eSystem.AddEvent(de);
		eSystem.AddEvent(me);


		//�̺�Ʈ�� ���� �ڵ鷯 ���.
		eSystem.Subscribe<AttackEvent>(atkHandler);
		eSystem.Subscribe<DamageEvent>(dmgHandler);
		eSystem.Subscribe<MoveEvent>(moveHandler);


		//SubscribeMember ������
		//eSystem.Subscribe<AttackEvent>(moveHandler, dmgHandler);
		//eSystem.Subscribe<DamageEvent>(atkHandler, moveHandler);
		//eSystem.Subscribe<MoveEvent>(atkHandler, dmgHandler);

		//eSystem.SubscribeMember(ae,me,de);

		//eSystem.Unsubscribe<AttackEvent>(moveHandler, dmgHandler);
		//eSystem.Unsubscribe<DamageEvent>(atkHandler, moveHandler);
		//eSystem.Unsubscribe<MoveEvent>(atkHandler, dmgHandler);


		//���� üũ
		eSystem.Subscribe<AttackEvent>(moveHandler, dmgHandler);

		eSystem.Notify<AttackEvent>(moveHandler, dmgHandler);



	}

	// Update is called once per frame
	void Update()
	{

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
