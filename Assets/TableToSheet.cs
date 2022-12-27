using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class TableToSheet : MonoBehaviour
{   
    [SerializeField] string fileName = "table.csv";
    [SerializeField] string header = "Years; Forest; Shrubs; Arable land; Young growth; Forest; Shrubs; Capacity of forest equipment; Quantity of timber; Timber price; Income from agricultural production";
    string filepath = "";
    Table table;
    private List<GameObject> cells = new List<GameObject>();
    void Start()
    {
        table = GetComponent<Table>();
        cells = table.GetCells();
        filepath = Application.dataPath + "/" + fileName;
        Invoke("WriteCSV",5);
    }
    public void SaveToCSV()
    {
         cells = table.GetCells();
         WriteCSV();
    }

    void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filepath, false);
        tw.WriteLine("sep=;");
        tw.WriteLine(header);
        tw.Close();

        tw = new StreamWriter(filepath, true);
        for (int j = 0; j < cells.Count / table.GetRowCellCount(); j++)
        {
            string answerLine = "";
            for (int i = 0; i < table.GetRowCellCount(); i++)
            {
                answerLine += cells[i + j *  table.GetRowCellCount()].GetComponent<TMP_InputField>().text + ";";
            }
            tw.WriteLine(answerLine);
        }
        tw.Close();

    }
}
