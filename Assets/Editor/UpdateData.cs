using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;


public class UpdateData : AssetPostprocessor
{


	void OnPreprocessAsset()
	{
		// ������ �Ľ��ؼ� ������Ʈ �ϴ� �κ�.

		// dll �� ������ 00.Data�� ���������� ����� �� �����ȿ� ���������Ͱ� ����־�����(���ϱ��� �ʼ� = EX )���������� �´���) dll�� ������ cs������ txt�����ȿ�
		// ����� ������ ���� �ش� ������ ���ٸ� ���� ���丮�� �����������.

		// �̹� �ִ� ��ũ���ͺ� ������Ʈ�� ������� ������Ʈ �� �� �ֵ���...


		DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(assetImporter.assetPath));
		DirectoryInfo[] diarr = di.GetDirectories();


		foreach (DirectoryInfo element in diarr)
		{
			string f_name = Path.GetFileName(element.Name);

			if (f_name == "txt")
			{
				Debug.Log("txt���� ����");
			}
			else if(f_name == "script")
			{
				Debug.Log("script���� ����");
			}
		}



		if (assetImporter.assetPath.Contains("Assets/00.Data/Excel"))
		{

			string a = Application.dataPath.Replace("/", @"\");
			string b = assetImporter.assetPath.Replace("Assets", "");  //

			string f_path = a + b.Replace("/", @"\");            // path��� ����

			//int idx = f_path.LastIndexOf("\\");                  // path���� ���� �̸� ��ġ ã��.
			//string f_name = f_path.Substring(idx + 1);           // path���� ���� �̸� ����.

			string f_name = Path.GetFileName(f_path);            // PathŬ������ ����ؼ� �̸� ����

			string f_extension = Path.GetExtension(f_path);      // PathŬ������ ����ؼ� Ȯ���� ����

			//dll ����ְ� path�־ ���.
			

			//file�� Ȯ���ڰ� Excel�������� Ȯ��.
			// if(f_extension.Contains(".xlsx"))   { // Tooo  }

			Debug.Log(f_path);
			Debug.Log(f_name);
			Debug.Log(f_extension);
		}
		else
			return;
	
		//������ ������Ʈ�� �Ǹ� dll(ExcelRoaderTest)�� ����Ͽ� scriptable object�� ����� �� �ְԲ�..(�̹� ������ ������ ������� ������ �����Ǵ½�����)

	}




}
