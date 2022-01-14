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


public class Handler : TempCodes.IEventHandler
{

	public void OnNotify(TempCodes.IEvent e)
	{
		Debug.Log($"{e.ToString()}");
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



	//���Ŭ���� == ���������� �̺�Ʈ�� ȣ���� Ŭ����
	//: ��� �������̽��� ������ Ŭ����
	public class TempMessageSystem : MonoBehaviour
	{

		private static TempMessageSystem instance = null;

		//�̺�Ʈ�� �ڵ鷯���� �����ϴ� ����
		Dictionary<System.Type, List<IEventHandler>> EventListenerDic = new Dictionary<System.Type, List<IEventHandler>>();

		//Ÿ�԰� �̺�Ʈ�� ��ġ�����ִ� ����
		Dictionary<System.Type, IEvent> EventDic = new Dictionary<System.Type, IEvent>();

		//�̺�Ʈ�� �����ϴ� ����Ʈ
		List<IEvent> eventList = new List<IEvent>();



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
		}


		public static TempMessageSystem Instance
		{
			get
			{
				if (null == instance)
				{
					return null;
				}

				return instance;

			}
		}

		//Ÿ��(T) => �̺�Ʈ��.

		#region �̺�Ʈ����Ʈ ����


		//�̺�Ʈ ����Ʈ �� ��ġ���� ��� �����ִ� �Լ� ����


		//�̺�Ʈ ����Ʈ ����� �����ִ� �Լ�
		public void ShowEventList()
		{

			Debug.Log("--------------����Ʈ�� ��ϵǾ� �ִ� �̺�Ʈ--------------");
			foreach(var item in eventList)
			{
				Debug.Log($"{item.ToString()}");
			}
			Debug.Log("-----------------------------------------------------------------");
		}

		//�̺�Ʈ ��ġ������ ����� �����ִ� �Լ�
		public void ShowEventDic()
		{
			Debug.Log("--------------������ ��ġ�Ǿ� �ִ� �̺�Ʈ--------------");
			
			foreach(KeyValuePair<System.Type,TempCodes.IEvent> item in EventDic)
			{
				Debug.Log($"Type:{item.Key.ToString()} / Event:{item.Value.ToString()}");
			}
			Debug.Log("-----------------------------------------------------------------");

		}
		
		public void AddEvent(TempCodes.IEvent e)  //����Ʈ�� �̺�Ʈ �߰�
		{

			if (eventList.Contains(e))
				Debug.Log($"{e.GetType()}�� ��ϵǾ��ִ� �̺�Ʈ�Դϴ�.");
			else
				eventList.Add(e);
		}

		public void RemoveEvent(TempCodes.IEvent e) //����Ʈ�� �̺�Ʈ ����
		{
			if (eventList.Contains(e))
				eventList.Remove(e);
			else
				Debug.Log($"{e.GetType()}�� ����Ʈ�� ���� �̺�Ʈ�Դϴ�.");
		}



		public void AddEventdic(TempCodes.IEvent e) //������ Ÿ�԰� �̺�Ʈ ��Ī
		{
			if (!EventDic.ContainsKey(e.GetType()))
			{
				EventDic.Add(e.GetType(), e);
				Debug.Log($"EventDic�� {e.ToString()} �߰�");
				Debug.Log($"EventDic.Count:{EventDic.Count}");
			}
			else
			{
				Debug.Log($"{e.GetType()}�� ���� ��Ī�Ǿ� �ִ� �̺�Ʈ�Դϴ�.");
			}
		}

		public void RemoveEventdic(TempCodes.IEvent e) //������ ��Ī���� �����
		{

			if (EventDic.ContainsKey(e.GetType())) 
			{
				EventDic.Remove(e.GetType(), out e);
				Debug.Log($"EventDic�� {e.ToString()} ����");
				Debug.Log($"EventDic.Count:{EventDic.Count}");
			}
			else
			{
				Debug.Log($"{e.GetType()}�� ������ ��ġ�Ǿ����� �ʽ��ϴ�.");
			}
				
			
		}

		#endregion


		#region ����(Subscribe) ����
		//����
		public void Subscribe<T>(IEventHandler handler)
		{
			// Ÿ��(T)�� �ִ��� ã�ƺ��� ������ �ڵ鷯 �߰�.
			if (EventListenerDic.ContainsKey(typeof(T)))
			{
				EventListenerDic[typeof(T)].Add(handler);
			}
			else //������ �ش� Ÿ��(T)�� ���õ� ����� ����� �ڵ鷯(handler) �߰�
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
				//Ÿ��(T)�� �ִ��� ã�ƺ��� ������ �ڵ鷯���� �߰�
				if (EventListenerDic.ContainsKey(typeof(T)))
				{
					EventListenerDic[typeof(T)].Add(element);

				}
				else //������ �ش� Ÿ��(T)�� ���õ� ����� ����� �ڵ鷯��(handlerList)�� �߰�
				{
					EventListenerDic.Add(typeof(T), new List<IEventHandler>());
					EventListenerDic[typeof(T)].Add(element);
				}
			}


		}

		//�ش� �̺�Ʈ�� ������ ������� ����� �����ش�.(1���� Ÿ��(T)��)
		public void ShowSubscribeMember<T>()
		{
			var listners = EventListenerDic[typeof(T)];

			Debug.Log($"--------------{typeof(T)}�� �����ϰ��ִ� �ڵ鷯 ���--------------");

			foreach (var listner in listners)
			{
				Debug.Log($"{listner.ToString()}");

			}
			Debug.Log("-----------------------------------------------------------------");

		}

		//�ش� �̺�Ʈ���� ������ ������� ��ϵ��� �����ش�.(������ Ÿ��(T)����)
		public void ShowSubscribeMember(params IEvent[] eList)
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
			//Ÿ��(T)�� �ش��ϴ� ����Ʈ ����߿� ������ �ڵ鷯(handler)�� ������ �����ش�.
			if (EventListenerDic[typeof(T)].Contains(handler)) EventListenerDic[typeof(T)].Remove(handler);
		}

		//���� - ����
		public void Unsubscribe<T>(params IEventHandler[] handlereList)
		{
			foreach (var element in handlereList)
			{
				//Ÿ��(T)�� �ش��ϴ� ����Ʈ ����߿� ������ �ڵ鷯��(handlereList)�� ������ �����ش�.
				if (EventListenerDic[typeof(T)].Contains(element)) EventListenerDic[typeof(T)].Remove(element);
			}
		}
		#endregion


		#region �˸�(Notify) ����

		//�˸� - �ڵ鷯 ���� ���
		public void Notify<T>(bool Except,params TempCodes.IEventHandler[] handlerList)
		{

			if(Except)  //Except�� True�ϰ�� �ڿ����� �ڵ鷯 ����� �˸����� ����
			{
				//���� Ÿ��(T)�� �ڵ鷯 ����Ʈ�� �޴´�.
				var listners = EventListenerDic[typeof(T)]; //�ش� Ÿ��(T)�� ���õ� ����Ʈ�� �޾ƿ´�. 

				bool check;

				foreach (var listner in listners)  //�� ����Ʈ�� �����ϴ� �������
				{
					check = false;
					foreach (var handler in handlerList)  //�ڵ鷯 ����Ʈ�� �����ִ� �ڵ鷯�� �ִٸ�
					{
						//�ڵ鷯 ����Ʈ�� �����ִ� �ڵ鷯�� �ִٸ�...
						if (string.Compare(handler.ToString(), listner.ToString(), false) == 0)
						{
							check = true;
							Debug.Log($"{typeof(T).ToString()}��{handler.ToString()}�� �˸����� �����մϴ�.");
						}

					}

					//������ �ڵ鷯 ����Ʈ�� ���ٸ� �˸��� �����ش�.
					if (!check)
						listner.OnNotify(EventDic[typeof(T)]);

				}
			}
			else  //False�� ��� �ڿ� ���� �ڵ鷯 ��ϸ� �˸�����
			{

				//���� Ÿ��(T)�� �ڵ鷯 ����Ʈ�� �޴´�.
				var listners = EventListenerDic[typeof(T)]; //�ش� Ÿ��(T)�� ���õ� ����Ʈ�� �޾ƿ´�. 

				bool check;

				foreach (var listner in listners)  //�� ����Ʈ�� �����ϴ� �������
				{
					check = false;
					foreach (var handler in handlerList)  //�ڵ鷯 ����Ʈ�� �����ִ� �ڵ鷯�� �ִٸ�
					{
						//�ڵ鷯 ����Ʈ�� �����ִ� �ڵ鷯�� �ִٸ�...
						if (string.Compare(handler.ToString(), listner.ToString(), false) == 0)
						{
							check = true;
						}

					}

					//������ �ڵ鷯 ����Ʈ�� ���ٸ� �˸��� �����ش�.
					if (check)
						listner.OnNotify(EventDic[typeof(T)]);

				}


			}

			
		}



		//�˸�- ����(�̺�Ʈ ����Ʈ���� ������(����,���� ����) �̺�Ʈ�� �����ϰ��ִ� ��� �ڵ鷯�鿡�� 
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

		

		//�˸� - ��ü(�̺�Ʈ ����Ʈ�� �ִ� �̺�Ʈ�� �����ϰ��ִ� ��� �ڵ鷯�鿡��)
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



		public void ResetData()
		{
			eventList.Clear();
			EventDic.Clear();
			EventListenerDic.Clear();

			Debug.Log($"eventList.Count:{eventList.Count}, " +
				$"EventDic.Count:{EventDic.Count}, " +
				$"EventListenerDic.Count:{EventListenerDic.Count}");

		}


		
	}
}