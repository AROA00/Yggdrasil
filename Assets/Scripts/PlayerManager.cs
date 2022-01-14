using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	
	public float Speed = 7f;
	private float h, v;

	public Yggdrasil.CharacterStats p_status; 

	private TempCodes.TempMessageSystem eSystem;
	private MoveEvent me;
	private Handler player;
    // Start is called before the first frame update
    void Start()
    {
		

		// 1.�ý��� ����
		eSystem = TempCodes.TempMessageSystem.Instance;

		// 2.������ �̺�Ʈ ����
		me = new MoveEvent();

		// 3.�ڵ鷯 ���(����)
		player = new Handler();

		// 4.�̺�Ʈ �߰�
		eSystem.AddEvent(me);

		// 5.Ÿ�� ��ġ
		eSystem.AddEventdic(me);

		// 6.���� �� ����
		eSystem.Subscribe<MoveEvent>(player);

		eSystem.ShowSubscribeMember<MoveEvent>();



		p_status = new Yggdrasil.CharacterStats();
		p_status.Attack = 5;
		

	}

    // Update is called once per frame
    void Update()
    {


		if(Input.GetKeyDown(KeyCode.Space))
		{
			playerBuf.Buf b = new playerBuf.Buf(ref p_status);

			//���� ����� ���ݷ�
			Debug.Log("Attack:" + p_status.Attack);


			//�Ŵ������� ������ ����� Ȯ�ι���
			b.BufTriggerManager();

			//���� ����� ���ݷ�
			Debug.Log("Attack:" + p_status.Attack);
		}

		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(h, 0, v) * Speed * Time.deltaTime);



	}
}
