using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InsertAgricultural : MonoBehaviour
{
    string URL = "http://localhost/classroomdb/InsertToAgricultural2.php";

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddRow();
        }
    }
    void Start()
    {
        Invoke("AddRow", 10f);
    }
    public void AddRow()
    {
        Debug.Log("Uploading");
        StartCoroutine(Upload(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 69));
    }
    IEnumerator Upload(int year, int forest, int shrubs, int arable_land, int young_growth, int forest_cut, int shrubs_cut, int capacity_of_forest_equipment, int quantity_of_timber, int timber_price, int income_from_agricultural_production)
    {
        Debug.Log("Uploading");
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        form.AddField("year", year);
        form.AddField("forest", forest);
        form.AddField("shrubs", shrubs);
        form.AddField("arable_land", arable_land);
        form.AddField("young_growth", young_growth);
        form.AddField("forest_cut", forest_cut);
        form.AddField("shrubs_cut", shrubs_cut);
        form.AddField("capacity_of_forest_equipment", capacity_of_forest_equipment);
        form.AddField("quantity_of_timber", quantity_of_timber);
        form.AddField("timber_price", timber_price);
        form.AddField("income_from_agricultural_production", income_from_agricultural_production);
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

    }

}
