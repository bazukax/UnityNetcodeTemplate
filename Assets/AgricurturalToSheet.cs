using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class AgricurturalToSheet : MonoBehaviour
{
    string filename = "";
    Table table;
    private List<GameObject> cells = new List<GameObject>();
    void Start()
    {
        table = GetComponent<Table>();
        cells = table.GetCells();
        filename = Application.dataPath + "/test.csv";
       Invoke("WriteCSV",5);

    }
    public void SaveToCSV()
    {
         cells = table.GetCells();
         WriteCSV();
    }

    void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("sep=;");
        tw.WriteLine("Years; Forest; Shrubs; Arable land; Young growth; Forest; Shrubs; Capacity of forest equipment; Quantity of timber; Timber price; Income from agricultural production");
        tw.Close();

        tw = new StreamWriter(filename, true);
        for (int j = 0; j < cells.Count / 11; j++)
        {
            string answerLine = "";
            for (int i = 0; i < 11; i++)
            {
                answerLine += cells[i + j * 11].GetComponent<TMP_InputField>().text + ";";
            }
            tw.WriteLine(answerLine);
        }
        //tw.WriteLine("1; 20; 50; 80; 20; 20; 10; 10; 50; 600; 900");
        tw.Close();

    }
}
