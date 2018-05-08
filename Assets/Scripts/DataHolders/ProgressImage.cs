using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressImage {

    public Texture2D texture;
    public string date;
    public int dateCompareValue;

    public int year;
    public int month;

    public void CalculateDateValue ()
    {
        string[] splittedString = date.Split('_');
        for (int i = 0; i < splittedString.Length; i++)
        {
            Debug.Log(splittedString[i]);
        }
      
        dateCompareValue = 0;
        dateCompareValue += int.Parse(splittedString[0]);
        dateCompareValue += int.Parse(splittedString[1]) * 100;
        month = int.Parse(splittedString[1]);
        dateCompareValue += int.Parse(splittedString[2]) * 10000;
        year = int.Parse(splittedString[2]);
    }

    /// <summary>
    /// Changes the DateTime string (1/2/2018) to the appropriate string (1_2_2018)
    /// </summary>
    /// <param name="_dateString"></param>
    public void SetDateString(string _dateString)
    {      
        date = _dateString;
        date = date.Replace('/', '_');
        Debug.Log("After:" + date);
    }

}
