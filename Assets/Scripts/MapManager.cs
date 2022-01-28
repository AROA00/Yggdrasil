using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �ؾ��� ���
 * 1. �޸�Ǯ ����� �޸�Ǯ���� Ÿ�� �����ϵ��� �ϱ�.
 * (
 * + �߰������� Ÿ�Ͽ� ���� Ÿ���� ������ ���̶�� ��ȹ���� ���� �ϼ����ϱ� Ÿ�Ժ��� �޸�Ǯ�� ����
 *    �� Ÿ�Ͽ� ���� ������ ���� �� ��.
 * )
 * 2.Ÿ�� Ŭ���� ���� ��� ¥��( ex-���ɼ�ȯ�� Ȱ��ȭ �� ��� Ÿ�� Ŭ�� ����, Ŭ���� Ÿ�� ������ ��û�� ������ ����ϵ���) 
 * 3.Tile ��ũ��Ʈ �����Ͽ� Ÿ�ϸ��� ������ �־�� �� ������ �����صα�.(��ġ,Ÿ�� Ÿ�� �� �ڼ��Ѱ� ��ȹ�� ������)
 */
public class MapManager : MonoBehaviour
{
    [SerializeField]
    Vector3 m_StartPos;
    [SerializeField]
    int m_mapWidth;
    [SerializeField]
    int m_mapHeight;
    [SerializeField]
    bool flag;
    #region TileInfo
    public GameObject HexTilePrefab;
    float m_CToSVertex_Length; //������ ���� �ٱ��� ������������ ����
    float m_CToSLine_Length;  //������ �ٱ� �������� ����
    float m_SideLine_Length; //�ٱ� �� ����
    #endregion
    #region GroundInfo
    public GameObject GroundPrefab;
    //Ÿ�� 1�� �߰��� �ٴ� ũ�� �þ ��ġ
    float GroundNomalWidth = 48.3f;
    float GroundNomalHeight = 44f;
    GameObject Ground;
    #endregion
    void Start()
    {
        InitTileInfo();
        CreateHexTileMap();
    }

    void InitTileInfo()
    {
        Ground = null;
        float CosValue = Mathf.Cos(36 * Mathf.Deg2Rad);
        float SinValue = Mathf.Sin(36 * Mathf.Deg2Rad);

        m_CToSVertex_Length = 21.4f;
        m_CToSLine_Length = m_CToSVertex_Length * CosValue;
        m_SideLine_Length = m_CToSVertex_Length * SinValue * 2;

        if (!flag)
        {
            m_mapWidth = 6;
            m_mapHeight = 5;
        }
    }
    void CreateHexTileMap()
    {
        Vector3 m_StartPoint = m_StartPos;
        Vector3 m_CurrentPos = m_StartPos;
        Ground = Instantiate<GameObject>(GroundPrefab);
        Ground.transform.localScale = new Vector3(GroundNomalWidth * m_mapWidth, 0, GroundNomalHeight * m_mapHeight);
        Vector3 result = new Vector3(m_StartPos.x + m_CToSVertex_Length * (m_mapWidth - 0.5f), 0, m_StartPos.z + m_CToSLine_Length * (m_mapHeight - 1));
        Ground.transform.position = result;
        for (int y = 0; y < m_mapHeight; ++y)
        {
            if (y % 2 != 0)
            {
                m_CurrentPos.x = m_StartPos.x + m_CToSVertex_Length;
            }
            else m_CurrentPos.x = m_StartPos.x;
            for (int x = 0; x < m_mapWidth; ++x)
            {
                GameObject obj = Instantiate<GameObject>(HexTilePrefab);
                obj.transform.position = m_CurrentPos;
                m_CurrentPos.x += m_CToSVertex_Length * 2;
            }
            m_CurrentPos.z += m_CToSLine_Length * 2;
        }
    }
}
