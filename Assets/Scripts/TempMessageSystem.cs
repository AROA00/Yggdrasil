using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace TempCodes
{
	public interface IEvent : System.IDisposable
	{

	}


	public interface IEventHandler
	{
		//���⼭ Ư���̺�Ʈ�� ����.
		public abstract void OnNotify(IEvent e);

	}

	//������ ����Ŭ����1(�̺�Ʈ ����1)
	public class MoveEventHandler : IEventHandler
	{
		//���Ÿ���� Ŭ�������� �� �޼ҵ带 �����Ŵ
		public virtual void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} ���� �̺�Ʈ ȣ��");
		}

	}

	//������ ����Ŭ����2(�̺�Ʈ ����2)
	public class DamageEventHandler : IEventHandler
	{
		public void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} ����� �̺�Ʈ ȣ��");
		}
	}



	//���Ŭ���� == ���������� �̺�Ʈ�� ȣ���� Ŭ����
	//: ��� �������̽��� ������ Ŭ����
	public class TempMessageSystem : MonoBehaviour
	{

		//�̺�Ʈ���� ����Ʈ�� ����
		Dictionary<System.Type, List<IEventHandler>> EventListenerDic = new Dictionary<System.Type, List<IEventHandler>>();
		List<IEvent> eventList = new List<IEvent>();

		#region �̺�Ʈ����Ʈ ����
		public void AddEvent(TempCodes.IEvent e)
		{
			eventList.Add(e);
		}

		public void RemoveEvent(TempCodes.IEvent e)
		{
			eventList.Remove(e);
		}
		#endregion



		#region ����(Subscribe) ����
		//����
		public void Subscribe<T>(IEventHandler handler)
		{
			//Ÿ��(T)�� �ִ��� ã�ƺ��� ������ �̺�Ʈ(e) �߰�.
			if (EventListenerDic.ContainsKey(typeof(T)))
			{
				EventListenerDic[typeof(T)].Add(handler);
			}
			else //������ �ش� Ÿ��(T)�� ���õ� ����� ����� �̺�Ʈ(e) �߰�
			{
				EventListenerDic.Add(typeof(T), new List<IEventHandler>());
				EventListenerDic[typeof(T)].Add(handler);
			}
		}
		//���� - ����
		public void Subscribe<T>(params IEventHandler[] handlerList)
		{
			foreach (var element in handlerList)
			{
				if (EventListenerDic.ContainsKey(typeof(T)))
				{
					EventListenerDic[typeof(T)].Add(element);

				}
				else
				{
					EventListenerDic.Add(typeof(T), new List<IEventHandler>());
					EventListenerDic[typeof(T)].Add(element);
				}
			}


		}

		//�ش� �̺�Ʈ�� ������ ������� ����� �����ش�.(1���� �̺�Ʈ��)
		public void SubscribeMember<T>()
		{
			var listners = EventListenerDic[typeof(T)];

			Debug.Log($"--------------{typeof(T)}�� �����ϰ��ִ� �ڵ鷯 ���--------------");

			foreach (var listner in listners)
			{
				Debug.Log($"{listner.ToString()}");

			}
			Debug.Log("-----------------------------------------------------------------");

		}

		//�ش� �̺�Ʈ���� ������ ������� ��ϵ��� �����ش�.(������ �̺�Ʈ ����)
		public void SubscribeMember(params IEvent[] eList)
		{
			foreach (var element in eList)
			{
				var listners = EventListenerDic[element.GetType()];

				Debug.Log($"--------------{element.GetType()}�� �����ϰ��ִ� �ڵ鷯 ���--------------");

				foreach (var listner in listners)
				{
					Debug.Log($"{listner.ToString()}");
				}
				Debug.Log("-----------------------------------------------------------------");
			}

		}

		#endregion


		#region ����(Unsubscribe) ����
		//����
		public void Unsubscribe<T>(IEventHandler handler)
		{
			if (EventListenerDic[typeof(T)].Contains(handler)) EventListenerDic[typeof(T)].Remove(handler);  //�������� �����ش�.
		}

		//���� - ����
		public void Unsubscribe<T>(params IEventHandler[] handlereList)
		{
			foreach (var element in handlereList)
			{
				if (EventListenerDic[typeof(T)].Contains(element)) EventListenerDic[typeof(T)].Remove(element);
			}
		}
		#endregion


		#region �˸�(Notify) ����

		public void Notify<T>(params TempCodes.IEventHandler[] handlerList)
		{

			var listners = EventListenerDic[typeof(T)];



			//���� �˸��� ���� �ι��� ȣ��� �ѹ����� ȣ��ǵ��� �����ϱ�.
			foreach (var listner in listners)
			{
				foreach (var handler in handlerList)
				{
					if (!listner.Equals(handler))
					{
						Debug.Log($"{handler.ToString()}�� �˸����� �����մϴ�.");
					}

				}
			}

		}


		//��������(������(T)�̺�Ʈ(������ ����)�� ������)�˸�
		public void Notify(params TempCodes.IEvent[] eList)
		{

			foreach (var element in eList)
			{

				if (eventList.Contains(element))
				{
					var listners = EventListenerDic[element.GetType()];

					foreach (var lister in listners)
					{
						lister.OnNotify(element);
					}
				}
				else
				{
					Debug.Log($"{element.ToString()}�� �̺�Ʈ ����Ʈ�� �������� �ʽ��ϴ�...<!���� �̺�Ʈ ����Ʈ�� �߰����ּ���!>");
				}


			}

		}


		//��ü(�̺�Ʈ ����Ʈ�� �ִ� �� �̺�Ʈ�� ������)���� �˸�
		public void Notify()
		{

			//foreach �̿�
			foreach (var e in eventList)
			{

				var listners = EventListenerDic[e.GetType()];

				foreach (var listner in listners)
				{
					listner.OnNotify(e);
				}
			}

		}
		#endregion


		// Start is called before the first frame update
		void Start()
		{


		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}