using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class UpdateData : AssetPostprocessor
{


	void OnPreprocessAsset()
	{
		// ������ �Ľ��ؼ� ������Ʈ �ϴ� �κ�.

		// dll �� ������ 00.Data�� ���������� ����� �� �����ȿ� ���������Ͱ� ����־�����(���ϱ��� �ʼ� = EX )���������� �´���) dll�� ������ cs������ txt�����ȿ�
		// ����� ������ ���� �ش� ������ ���ٸ� ���� ���丮�� �����������.

		// �̹� �ִ� ��ũ���ͺ� ������Ʈ�� ������� ������Ʈ �� �� �ֵ���...



		if (assetImporter.assetPath.Contains("Assets/00.Data"))
		{
			string name = assetImporter.assetBundleName;

			Debug.Log(name + "asd");
			Debug.Log("�ش絥���ʹ� Data�����ȿ� ������");
		}
		else
		{
			Debug.Log("�ش� �����ʹ� Data�����ۿ� ������");
		}


		//������ ������Ʈ�� �Ǹ� dll(ExcelRoaderTest)�� ����Ͽ� scriptable object�� ����� �� �ְԲ�..(�̹� ������ ������ ������� ������ �����Ǵ½�����)

	}




}
