using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
public enum E_DataTableType
{
    None = -1,
    BossStat,
    CharStat,
    Max
}
//�ؾ��Ұ� : �̱��� �����ؼ� �Ŵ����� �̱������� ������Ű��.
[DefaultExecutionOrder(-99)]
public class DataTableManager : Singleton_Ver1.Singleton<DataTableManager>
{
    #region ScriptableObject ����
    /*ScriptableObject �����͸� ����ϱ� ����, ����ȭ �Ͽ� ����Ƽ���� ���� �� ���� ����.
	https://junwe99.tistory.com/13
	https://everyday-devup.tistory.com/53
	*/
//<<<<<<< Updated upstream
    #endregion
    [SerializeField]
    protected List<ScriptableObject> m_DataTableList;
    protected static Dictionary<E_DataTableType, ScriptableObject> m_DataTables;
    #region �ٸ� ��ũ��Ʈ���� ������ ȣ��
    public T GetDataTable<T>() where T : ScriptableObject
    {
        string typeName = typeof(T).ToString().Split('_')[0];
        E_DataTableType type;
        //���ڿ��� ���������� ��ȯ.
        if (Enum.TryParse<E_DataTableType>(typeName, out type))
        {
            return m_DataTables[type] as T;
        }
        return null;
    }
    #endregion

    private void Awake()
    {
        m_DataTables = new Dictionary<E_DataTableType, ScriptableObject>();

        for (E_DataTableType i = E_DataTableType.None + 1; i < E_DataTableType.Max; ++i)
        {
            //datatable list(������ ������ ���)���� Ÿ�԰� ��ġ�ϴ� ���� ������ ��ųʸ��� �߰��Ѵ�.
            #region SingleOrDefault ����
            //SingleOrDefault :���� �̸��� ������ 1�� �̻��� ��ȸ�� �� ���� ����. 1���� ��ȸ�Ҷ� ���.
            //https://im-first-rate.tistory.com/91
            #endregion
            m_DataTables.Add(i, m_DataTableList.Where(item => i.ToString() + "_TableLoader" == item.name).SingleOrDefault());
        }
    }
}

