using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private int index; // �� ������ ������ �ִ� �ε�����ȣ

    private BossStat_TableExcel TableExcel;

    #region Boss���� ���� ��������
    // BossIndex�� �ش�Ǵ� ���� TableExcel�� �����´�.
    public BossStat_TableExcel GetStatData(int _index)
    {
        BossStat_TableExcel stat = new BossStat_TableExcel();
        var list = DataTableManager.Instance.GetDataTable<BossStat_TableExcelLoader>().DataList;

        foreach (var it in list)
        {
            if (_index == it.BossIndex)
            {
                stat = it;
                return stat;
            }
        }
        return stat;
    }
    #endregion

    void Start()
    {
        // �ش纸�� TableExcel ��������
        TableExcel = GetStatData(index);

        Debug.Log(TableExcel.Name_KR);
    }
}
