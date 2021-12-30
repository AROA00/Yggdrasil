using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �߻�Ŭ����  == �̺�Ʈ �߻� Ŭ����
//: ���������� �����ؾ� �� �������̽� �޼���  == �̺�Ʈ�� ������ �������� �ż���
public abstract class Event
{

	public abstract void Notify();

}

//������ ����Ŭ����1(�̺�Ʈ ����1)
public class EventHandler1 : Event
{
	//���Ÿ���� Ŭ�������� �� �޼ҵ带 �����Ŵ
	public override void Notify()
	{
		Debug.Log("�̺�Ʈ1 ȣ��");
	}

}

//������ ����Ŭ����2(�̺�Ʈ ����2)
public class EventHandler2 : Event
{
	public override void Notify()
	{
		Debug.Log("�̺�Ʈ2 ȣ��");
	}
}


//��� �������̽� == ������ ���Ŭ�������� �ݵ�� �־�� �ϴ� ���
//: ������ ����, Ȱ�뿡 ���� Ÿ�� ���� == �̺�Ʈ ���� �� Ȱ�� 
public interface ISubject
{

	public void Subscribe(Event e);  //����
	public void Unsubscribe(Event e);  //���� ����
	public void Notify();  //�����ڿ��� ���� �˸�


}


//���Ŭ���� == ���������� �̺�Ʈ�� ȣ���� Ŭ����
//: ��� �������̽��� ������ Ŭ����
public class Observer : MonoBehaviour,ISubject
{

	//�̺�Ʈ���� ����Ʈ�� ����
	List<Event> EventList = new List<Event>();


 
	//���(�������̽�) ���  == �ݵ�� �����ؾ� �Ǵ� ��
	public void Subscribe(Event e)  
	{
		EventList.Add(e);
	}
	public void Unsubscribe(Event e)
	{

		if (EventList.IndexOf(e) > 0) EventList.Remove(e);
	}
	public void Notify()
	{
		//for �̿�
		//for(int i=0;i<EventList.Count; i++)
		//{
		//	EventList[i].Notify();
		//}

		//foreach �̿�
		foreach(Event e in EventList)
		{
			e.Notify();
		}



	}

	// Start is called before the first frame update
	void Start()
    {
		Event eve1 = new EventHandler1();
		Event eve2 = new EventHandler2();


		Subscribe(eve1);
		Subscribe(eve2);

		Notify();

		Unsubscribe(eve2);

		Notify();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
