using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DateManager : MonoBehaviour {

    [SerializeField]
    private ContentDataManager contentDataManager;
    [SerializeField]
    private ExerciseManager exerciseManager;

    public int currentMonth;
    public int currentWeek;
    public int currentDay;
	void Awake () {
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        Calendar cal = dfi.Calendar;
        DateTime date = System.DateTime.Now;
        currentWeek = cal.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        currentMonth = cal.GetMonth(date);
        currentDay = cal.GetDayOfYear(date);
	}

    public void CheckDate ()
    {
        string[] date = contentDataManager.LoadDate();
        if (date.Length > 0)
        {

            if (int.Parse(GetValueFromString(date[0])) != currentMonth)
            {
                exerciseManager.ClearMonths();
                exerciseManager.SaveData();
            }
            if (int.Parse(GetValueFromString(date[1])) != currentWeek)
            {
                exerciseManager.ClearWeeks();
                exerciseManager.SaveData();
            }
            if (int.Parse(GetValueFromString(date[2])) != currentDay)
            {
                exerciseManager.ClearWeeks();
                exerciseManager.SaveData();
            }
        }

        List<string> dateStrings = new List<string>();
        dateStrings.Add("Month[" + currentMonth + "]");
        dateStrings.Add("Week[" + currentWeek + "]");
        dateStrings.Add("Day[" + currentDay + "]");

        contentDataManager.SaveDate(dateStrings.ToArray());
    }

    private string GetValueFromString (string _contentString)
    {
        int startIndex = _contentString.IndexOf("[");
        int endIndex = _contentString.IndexOf("]");
        return _contentString.Substring(startIndex + 1, endIndex - startIndex - 1);
    }
}
