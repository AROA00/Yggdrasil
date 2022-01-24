using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yggdrasil
{
    public class BossManager : MonoBehaviour
    {
        [SerializeField]
        private int index; // �� ������ ������ �ִ� �ε�����ȣ

        private BossStat_TableExcel TableExcel;
        DataTableManager M_DataTable => DataTableManager.Instance;

        #region Boss���� ���� ��������
        // BossIndex�� �ش�Ǵ� ���� TableExcel�� �����´�.

        public BossStat_TableExcel GetStatData(int _index)
        {
            BossStat_TableExcelLoader m_BossStat = M_DataTable.GetDataTable<BossStat_TableExcelLoader>();
            BossStat_TableExcel statData = m_BossStat.DataList.Where(item => item.BossIndex == _index).SingleOrDefault();
            return statData;
        }
        #endregion

        public BossManager(int _index)
        {
            TableExcel = GetStatData(_index);
        }

        void Start()
        {
            var obj = new BossManager(21001);

            Debug.Log($"{obj.TableExcel.Name_KR}");
        }
    }
}