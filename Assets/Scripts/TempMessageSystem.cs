using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TempCodes
{
	public interface IEvent : System.IDisposable
	{

	}

	public class DamageEvent : IEvent
	{
		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}

	public class MoveEvent : IEvent
	{
		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}


	public class temp
	{


	}



	public interface IEventHandler
	{
		public abstract void OnNotify(IEvent e);

	}

	//������ ����Ŭ����1(�̺�Ʈ ����1)
	public class EventHandler1 : IEventHandler
	{
		//���Ÿ���� Ŭ�������� �� �޼ҵ带 �����Ŵ
		public virtual void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} �̺�Ʈ1 ȣ��");
		}

	}

	//������ ����Ŭ����2(�̺�Ʈ ����2)
	public class EventHandler2 : IEventHandler
	{
		public void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} �̺�Ʈ2 ȣ��");
		}
	}



	//���Ŭ���� == ���������� �̺�Ʈ�� ȣ���� Ŭ����
	//: ��� �������̽��� ������ Ŭ����
	public class TempMessageSystem : MonoBehaviour
	{

		//�̺�Ʈ���� ����Ʈ�� ����
		Dictionary<System.Type, List<IEventHandler>> EventListenerDic = new Dictionary<System.Type, List<IEventHandler>>();
		List<IEvent> eventList = new List<IEvent>();


		//���(�������̽�) ���  == �ݵ�� �����ؾ� �Ǵ� ��
		public void Subscribe<T>(IEventHandler e)
		{
			//EventListenerDic.Add(typeof(T),  new List<IEventHandler>());  // �����Ҷ����� ���ο� ����Ʈ�� ��� ���ܳ�
			//EventListenerDic[typeof(T)].Add(e);


			//Ÿ���� �ִ��� ã�ƺ��� ������ ���ο� ����Ʈ�� �����ؼ� �߰�.
			if (EventListenerDic.ContainsKey(typeof(T)))
			{
				EventListenerDic[typeof(T)].Add(e);


			}
			else
			{
				//eventList.Add((TempCodes.IEvent)typeof(T));  //�����Ҷ� �̺�Ʈ����Ʈ�� �߰����ֱ�
				EventListenerDic.Add(typeof(T), new List<IEventHandler>());
				EventListenerDic[typeof(T)].Add(e);
			}


		}
		public void Unsubscribe<T>(IEventHandler e)
		{

			if (EventListenerDic[typeof(T)].Contains(e)) EventListenerDic[typeof(T)].Remove(e);  //���������� �����ش�.



		}
		public void Notify()
		{
			//for �̿�
			//for(int i=0;i<EventList.Count; i++)
			//{
			//	EventList[i].Notify();
			//}

			//�����
			Debug.Log(eventList.Count);

			//foreach �̿�
			foreach (var e in eventList)
			{

				var listners = EventListenerDic[e.GetType()];

				foreach (var listner in listners)
				{

					listner.OnNotify(e);
				}
			}

			//eventList.Clear();

			//foreach(var e in eventList)
			//{
			//	e.Dispose();//Dispose ���
			//}


		}

		// Start is called before the first frame update
		void Start()
		{

			eventList.Add(new MoveEvent());
			eventList.Add(new DamageEvent());

			IEventHandler eve1 = new EventHandler1();
			IEventHandler eve2 = new EventHandler2();




			Subscribe<MoveEvent>(eve1);
			Subscribe<DamageEvent>(eve2);


			Notify();

			Unsubscribe<DamageEvent>(eve2);

			Notify();



			eventList.Clear();


		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}