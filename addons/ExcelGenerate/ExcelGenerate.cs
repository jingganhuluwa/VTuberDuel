#if TOOLS
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using Godot;
using Newtonsoft.Json;
using TinyFramework;
using FileAccess = System.IO.FileAccess;

[Tool]
public partial class ExcelGenerate : EditorPlugin
{
	private Button GenerateButton;

	//excel表字典
	private static Dictionary<string, DataTable> _dataTableDict = new Dictionary<string, DataTable>();
	
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
		GenerateButton = new Button();
		GenerateButton.Name = "Excel生成配置";
		GenerateButton.Text="Excel生成配置";
		AddControlToDock(DockSlot.RightBl,GenerateButton);
		GenerateButton.Pressed += Generate;
	}

	private void Generate()
	{
		_dataTableDict.Clear();
		GD.Print("开始生成");
		
		//1.读取表格数据存入字典
		ReadExcel();
		//2.生成字段类
		GenerateFieldClass();
		//3.生成json配置数据
		GenerateJsonFile();
	}

	private void GenerateJsonFile()
	{
		if (!Directory.Exists(PathDefine.ConfigPath))
		{
			Directory.CreateDirectory(PathDefine.ConfigPath);
		}
		
		foreach ((string name, DataTable table) in _dataTableDict)
		{

			List<Dictionary<string, object>> dataTable = new List<Dictionary<string, object>>();


			DataRow rowName = table.Rows[0];
			DataRow rowType = table.Rows[1];
			for (int i = 4; i < table.Rows.Count; i++)
			{
				if (string.IsNullOrEmpty(table.Rows[i][0].ToString()))
				{
					//id行没数据,直接跳过
					continue;
				}
                
				Dictionary<string, object> row = new Dictionary<string, object>();

				for (int j = 0; j < table.Columns.Count; j++)
				{
					//读取第一行数据作为表头字段
					string field = rowName[j].ToString();
					// key value 格式的数据

					string type = rowType[j].ToString();
					string value = table.Rows[i][j].ToString();
					if (string.IsNullOrEmpty(value))
					{
						continue;
						//throw new Exception("表格实际数据存在未配置项");
					}

					if (field != null)
					{
						row[field] = Convert(type, value);
					}
				}

				dataTable.Add(row);
			}

			//生成Json字符串
			string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
			json = json.Replace("\"[", "[").Replace("]\"", "]");

			using (StreamWriter writer = new StreamWriter(PathDefine.ConfigPath + table.TableName + ".json"))
			{
				writer.WriteLine(json);
			}
		}
	}
	
	private static string Convert(string type, string value)
	{
		switch (type)
		{
			case "int":
			case "float":
			case "double":
			case "bool":
			case "string":
			case "long":
				return value;
			case "int[]":
			case "float[]":
			case "double[]":
			case "bool[]":
			case "string[]":
			case "long[]":

				return ArrayParse(value);
			default:
				return value;
		}
	}
	
	/// <summary>
	/// 将数组转换成对应的字符串
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	private static string ArrayParse(string value)
	{
		//切分字符串得到数组
		var res = value.Split("|");
		StringBuilder sb = new StringBuilder();
		sb.Append('[');
		for (int i = 0; i < res.Length; i++)
		{
			sb.Append(res[i]);
			//不是数组最后一个加，
			if (i != res.Length - 1)
			{
				sb.Append(',');
			}
		}

		sb.Append(']');
		return sb.ToString();
	}



	private void GenerateFieldClass()
	{
		if (!Directory.Exists(PathDefine.ExcelFieldClassPath))
		{
			Directory.CreateDirectory(PathDefine.ExcelFieldClassPath);
		}

		foreach ((string name, DataTable table) in _dataTableDict)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("using TinyFramework;");
			sb.AppendLine($"public class {table.TableName} : BaseConfig");
			sb.AppendLine("{");
			DataRow rowName = table.Rows[0];
			DataRow rowType = table.Rows[1];
			DataRow rowDesc = table.Rows[2];
			//忽略第一列Id
			for (int i = 1; i < table.Columns.Count; i++)
			{
				sb.AppendLine($"\t/// <summary> {rowDesc[i]} </summary>");
				sb.AppendLine($"\tpublic {rowType[i]} {rowName[i]};");
			}

			sb.Append('}');
			File.WriteAllText($"{PathDefine.ExcelFieldClassPath}{table.TableName}.cs", sb.ToString());
		}
	}

	private void ReadExcel()
	{
		//获取目录下所有文件
		DirectoryInfo directory = Directory.CreateDirectory(PathDefine.ExcelFilePath);
		FileInfo[] files = directory.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			if (fileInfo.Extension != ".xls" && fileInfo.Extension != ".xlsx")
			{
				continue;
			}

			//是Excel文件
			using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read))
			{
				IExcelDataReader excelDataReader;
				if (fileInfo.Extension==".xlsx")
				{
					excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
				}
				else
				{
					excelDataReader =ExcelReaderFactory.CreateBinaryReader(stream);
				}
				
				DataTableCollection dataTableCollection = excelDataReader.AsDataSet().Tables;
				foreach (DataTable dataTable in dataTableCollection)
				{
					//判断表名是否重复
					if (!_dataTableDict.TryAdd(dataTable.TableName, dataTable))
					{
						GD.PrintErr($"表名重复请修改!:{dataTable.TableName}");
					}
				}
			}
		}
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		GenerateButton.Pressed -= Generate;
	}
}
#endif
