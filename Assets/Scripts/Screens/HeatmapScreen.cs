using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeatmapScreen : BaseState {

    public enum HeatmapState
    {
        Today,
        Week,
        Month,
        Year,
        Alltime
    }

    private HeatmapState heatmapState;

    [SerializeField]
    private UIEnumSelector uiEnumSelector;

    [SerializeField]
    private ExerciseManager exerciseManager;

    [SerializeField]
    private Button backButton;

    [Header("Body Parts")]
    [SerializeField]
    private Image bottomLayer;
    [SerializeField]
    private Image shoulderPart, trapsPart, bicepPart,forearmPart,
        chestPart,neckPart,latsPart,middleBackPart,lowerBackPart,
        absPart,calvesPart,glutesPart,hamstringsPart,quadsPart,tricepsPart;

    private int[] countAmountList;
    void Awake ()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        uiEnumSelector.Initialize(typeof(HeatmapState));
        uiEnumSelector.onUpdated += UiEnumSelector_onUpdated;
    }

    private void OnBackButtonClicked ()
    {
        StateSelector.Instance.SetState("MainScreen");
    }

    private void UiEnumSelector_onUpdated ()
    {
        heatmapState = (HeatmapState)uiEnumSelector.GetIndex();
        UpdateHeatmap();
    }

    public override IEnumerator Enter ()
    {
        heatmapState = HeatmapState.Today;
        uiEnumSelector.SetIndex((int)heatmapState);
        uiEnumSelector.UpdateText();
        UpdateHeatmap();
        return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        return base.Exit();
    }

    public void UpdateHeatmap ()
    {
        string[] names = System.Enum.GetNames(typeof(Exercise.MuscleGroup));
        countAmountList = new int[names.Length];
        
        for (int i = 0; i < countAmountList.Length; i++)
        {
            for (int x = 0; x < exerciseManager.exercises.Count; x++)
            {
                Exercise exercise = exerciseManager.exercises[x];
                if (exercise.muscleGroup.ToString() == names[i])
                {
                    switch (heatmapState)
                    {
                        case HeatmapState.Today:
                        countAmountList[i] += exercise.totalTimesDoneToday;
                        break;
                        case HeatmapState.Week:
                        countAmountList[i] += exercise.totalTimesDoneWeek;
                        break;
                        case HeatmapState.Month:
                        countAmountList[i] += exercise.totalTimesDoneMonth;
                        break;
                        case HeatmapState.Year:
                        countAmountList[i] += exercise.totalTimesDoneYear;
                        break;
                        case HeatmapState.Alltime:
                        countAmountList[i] += exercise.totalTimesDone;
                        break;

                    }
                }
                   
            }
        }

        AnimateHeatmap();

    }

    public void AnimateHeatmap ()
    {
        int highestInt = GetHighestCountFromList();
        // count/highestInt
        Debug.Log((float)countAmountList[1] + " : " + highestInt);
        Debug.Log(((float)countAmountList[1]) / ((float)highestInt));
        float hueVal = 0.4f;
        float valueVal = 1;
        float divisionfix = 0.0001f;
        neckPart.DOColor(Color.HSVToRGB(hueVal, (float)countAmountList[1] / (divisionfix + (float)highestInt), valueVal),1);
        trapsPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[2] / (divisionfix + (float)highestInt), valueVal), 1);
        shoulderPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[3] / (divisionfix + (float)highestInt), valueVal), 1);
        chestPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[4] / (divisionfix + (float)highestInt), valueVal), 1);
        bicepPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[5] / (divisionfix + (float)highestInt), valueVal), 1);
        forearmPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[6] / (divisionfix + (float)highestInt), valueVal), 1);
        absPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[7] / (divisionfix + (float)highestInt), valueVal), 1);
        quadsPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[8] / (divisionfix + (float)highestInt), valueVal), 1);
        calvesPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[9] / (divisionfix + (float)highestInt), valueVal), 1);
        tricepsPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[10] / (divisionfix + (float)highestInt), valueVal), 1);
        latsPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[11] / (divisionfix + (float)highestInt), valueVal), 1);
        middleBackPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[12] / (divisionfix + (float)highestInt), valueVal), 1);
        lowerBackPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[13] / (divisionfix + (float)highestInt), valueVal), 1);
        glutesPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[14] / (divisionfix + (float)highestInt), valueVal), 1);
        hamstringsPart.DOColor(Color.HSVToRGB(hueVal, countAmountList[15] / (divisionfix + (float)highestInt), valueVal), 1);


    }

    public int GetHighestCountFromList ()
    {
        int highestInt = 0;
        for (int i = 0; i < countAmountList.Length; i++)
        {
            if (countAmountList[i] > highestInt)
                highestInt = countAmountList[i];
        }

        return highestInt;
    }
}
