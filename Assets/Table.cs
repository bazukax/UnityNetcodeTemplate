using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    [SerializeField] private Transform parent;

    [SerializeField] private GameObject cellPrefab;

    [SerializeField] private List<GameObject> cells= new List<GameObject>();

    [SerializeField] private int cellCount = 44;
    void Start()
    {
       
        for(int i= 0; i < cellCount; i++)
        {
             GameObject cell = Instantiate(cellPrefab,transform.position,Quaternion.identity);
             cell.transform.SetParent (parent.transform, false);
            RectTransform rt = cell.GetComponent<RectTransform>();
          
             cells.Add(cell);
        }
    }

}
