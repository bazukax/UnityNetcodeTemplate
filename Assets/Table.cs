using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    [SerializeField] private Transform parent;

    [SerializeField] private GameObject cellPrefab;

    [SerializeField] private List<GameObject> cells = new List<GameObject>();

    [SerializeField] private int initialCellCount = 44;

    [SerializeField] private int rowCellCount = 11;
    void Start()
    {
        for (int i = 0; i < initialCellCount; i++)
        {
            AddCell();
        }
    }
    public void AddCell()
    {
        GameObject cell = Instantiate(cellPrefab, transform.position, Quaternion.identity);
        cell.transform.SetParent(parent.transform, false);
        cells.Add(cell);
    }
    public void AddCells(int cellCount = 0)
    {
        for (int i = 0; i < cellCount; i++)
        {
            AddCell();
        }
    }
    public void AddRow()
    {
        for (int i = 0; i < rowCellCount; i++)
        {
            AddCell();
        }
    }
    public void AddRows(int rowsCount)
    {
        for (int j = 0; j < rowsCount;j++)
        {
            for (int i = 0; i < rowCellCount; i++)
            {
                AddCell();
            }
        }
    }
    public List<GameObject> GetCells()
    {
        return cells;
    }
    public int GetRowCellCount()
    {
        return rowCellCount;
    }

}
