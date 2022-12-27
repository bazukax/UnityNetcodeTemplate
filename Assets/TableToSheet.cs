using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

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
        Invoke("SaveToGoogleSheets",3);
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
    void SaveToGoogleSheets()
    {
      //  PUT https://sheets.googleapis.com/v4/spreadsheets/spreadsheetId/values/Sheet1!A1:D5?valueInputOption=USER_ENTERED
      //  PUT https://sheets.googleapis.com/v4/spreadsheets/1nvF0ZXtj76Hkky5EIYMTS1kuoO5k32SpvwgAYKPYPXE/values/Sheet1!A1:D5?valueInputOption=USER_ENTERED

      //https://docs.google.com/spreadsheets/d/1nvF0ZXtj76Hkky5EIYMTS1kuoO5k32SpvwgAYKPYPXE/edit#gid=0
      Upload();
    }
    IEnumerator Upload()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        using (UnityWebRequest www = UnityWebRequest.Put("https://sheets.googleapis.com/v4/spreadsheets/1nvF0ZXtj76Hkky5EIYMTS1kuoO5k32SpvwgAYKPYPXE/values/Sheet1!A1:D5?valueInputOption=USER_ENTERED", myData))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }
}
