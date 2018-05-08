using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ProgressViewSortUIItem : MonoBehaviour {

    public delegate void OnSortEvent ();
    public event OnSortEvent onSortingChanged = delegate
    { };

    private enum DateSortType
    {
        Year,
        Month,
        Exercise
    }

    [SerializeField]
    private DateSortType dateSortType;

    private int currentIndex;
    private int currentYear;

    [Header("UI")]
    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button rightButton;
    [SerializeField]
    private Text textDisplay;

    private string textPrefix = "";

    public void Awake ()
    {
        switch (dateSortType)
        {
            case DateSortType.Exercise:
            textPrefix = "";
            break;
            case DateSortType.Month:
            textPrefix = "Month: ";
            break;
            case DateSortType.Year:
            textPrefix = "Year: ";
            break;
        }
        currentYear = System.DateTime.Now.Year;
        leftButton.onClick.AddListener(OnPreviousClicked);
        rightButton.onClick.AddListener(OnNextClicked);
        DisplayNewText();
    }

    void OnPreviousClicked ()
    {
        switch (dateSortType)
        {
            case DateSortType.Exercise:
            if (currentIndex == 0)
                currentIndex = System.Enum.GetNames(typeof(Exercise.MuscleGroup)).Length;
            else
                currentIndex--;
            break;
            case DateSortType.Month:
            if (currentIndex == 0)
                currentIndex = 12;
            else
                currentIndex--;
            break;
            case DateSortType.Year:
            if (currentIndex == 0)
                return;
            currentIndex--;
            break;

        }
        DisplayNewText();
        onSortingChanged();
    }

    void OnNextClicked ()
    {
        switch (dateSortType)
        {
            case DateSortType.Exercise:
            if (currentIndex == System.Enum.GetNames(typeof(Exercise.MuscleGroup)).Length)
                currentIndex = 0;
            else
                currentIndex++;
            break;
            case DateSortType.Month:
            if (currentIndex == 12)
                currentIndex = 0;
            else
                currentIndex++;
            break;
            case DateSortType.Year:            
            currentIndex++;
            break;

        }

        DisplayNewText();
        onSortingChanged();
    }

    public void SetIndex(int _index)
    {
        currentIndex = _index;
        DisplayNewText();
        onSortingChanged();
    }

    public int GetIndex ()
    {
        return currentIndex;
    }

    public void DisplayNewText ()
    {
        if (currentIndex == 0)
        {
            textDisplay.text = textPrefix + "Any";
        }
        else
        {
            switch (dateSortType)
            {
                case DateSortType.Exercise:
                Exercise.MuscleGroup group = (Exercise.MuscleGroup)currentIndex;
                textDisplay.text = textPrefix + group.ToString();
                break;

                case DateSortType.Month:
                string monthName = new DateTime(2010, currentIndex, 1).ToString("MMMM", CultureInfo.InvariantCulture);
                if(currentIndex == 0)
                    textDisplay.text = textPrefix + monthName;
                else
                    textDisplay.text = monthName;
                break;

                case DateSortType.Year:
                if (currentIndex == 0)
                    textDisplay.text = textPrefix + (currentYear - currentIndex + 1);
                else
                    textDisplay.text = "" + (currentYear - currentIndex + 1);            
                break;

            }
        }
    }

}
