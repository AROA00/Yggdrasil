using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Yggdrasil
{

	public enum BossSkillType { WIDE=1,TARGET,LINE,DIFFUSION, SUMMONS  }

	public class BossSkillData
	{

		public string Name_KR { get; set; }  //�ѱ��̸� 
		public string Name_EN { get; set; }     //�����̸�
		public int BossIndex { get; set; }    //��ų �ε��� �� �������� ��� ������� �ָ���.
		public int TargetType { get; set; }        // �����̳� �Ʊ��̳� (�Ʊ��ϰ�� ���� Ȯ�������� ������ų), ���( �������� �ִ� ��� ������Ʈ) 
		public float Power { get; set; }  //�Ŀ�
		public float CoolTime { get; set; }  //
		public float SkillDistance { get; set; }
		public float SkillRange { get; set; }
		public BossSkillType SkillType { get; set; }       // ��ų�� ���� (�����̳� ,����̳� ���)   => ���Ƿ� float���� BossSkillType������ �ٲ�.

		//public float direction { get; set; }   //���� ����� ����������� (6��������)

		public bool move { get; set; }  //�̵�

		public float lifeTime { get; set; }  //����

		public float DoT { get; set; }   // ��ų �ֱ�
		public int SkillAdded { get; set; }      // �߰���ų
		public int BuffADDED { get; set; }        //�߰����� -> ���� ���̺��� ���� ����.

		public int SkillAnimation { get; set; }     //��ų �ִϸ��̼�



		public int AreaPrefab { get; set; }    //���� ������.

		public int LunchPrefab { get; set; }  //�߻� ������.

		public int FirePrefab { get; set; }  //����ü ������

		public int DamPrefab { get; set; }  //�ǰ� ������.

	}
}
public class BossSkillData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
