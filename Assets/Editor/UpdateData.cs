using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class UpdateData : AssetPostprocessor
{

	void OnPreprocessAsset()
	{
		//������ �Ľ��ؼ� ������Ʈ �ϴ� �κ�.

		//dll�� ��������  ��ȹ�ںе��� �����͸� ����Ƽ �ȿ� ����־����� ������Ʈ�� �����ϰԲ�(������Ʈ�� ������ �����ؾ��� ������� �������ϸ� ������Ʈ �Ѵٴ���.)


		if(assetImporter.assetPath.Contains("Assets/00.Data"))
		{
			Debug.Log("�ش� �����ʹ� Data�����ȿ� ������");
		}
		else
		{
			Debug.Log("�ش� �����ʹ� Data�����ۿ� ������");
		}


		//������ ������Ʈ�� �Ǹ� dll(ExcelRoaderTest)�� ����Ͽ� scriptable object�� ����� �� �ְԲ�..(�̹� ������ ������ ������� ������ �����Ǵ½�����)

	}



}
