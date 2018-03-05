using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exercise {

    public const int MUSCLEGROUPCOUNT = 16;
    public enum MuscleGroup
    {
        All,
        Neck,
        Trapezius,
        Shoulders,
        Chest,
        Biceps,
        ForeArm,
        Abs,
        Quads,
        Calves,
        Triceps,
        Lats,
        MiddleBack,
        LowerBack,
        Glutes,
        Hamstring,
        Other
    }

    public enum EquipmentGroup
    {
        None,
        Dumbell
    }

    public string excerciseName;
    public MuscleGroup muscleGroup;
    public EquipmentGroup equipmentGroup;
    public bool setIsTimed;
    public int repDuration;
    public int breakDuration;
    public int repetitionAmount;
    public int setAmount;

    //stats
    public int totalTimesDone;
    public int totalTimesDoneToday;
    public int totalTimesDoneWeek;
    public int totalTimesDoneMonth;

    public void AddTotalTimesCount ()
    {
        totalTimesDoneToday++;
        totalTimesDone++;
        totalTimesDoneWeek++;
        totalTimesDoneMonth++;
    }

    public string ToSaveString ()
    {
        //savestring example
        //Plank_Chest_True_30_30_5_etc

        string saveString = "";

        saveString += excerciseName  + "_";
        saveString += muscleGroup.ToString() + "_";
        saveString += equipmentGroup.ToString() + "_";
        saveString += setIsTimed + "_";
        saveString += repDuration + "_";
        saveString += breakDuration + "_";
        saveString += setAmount + "_";
        saveString += repetitionAmount + "_";
        saveString += totalTimesDone + "_";
        saveString += totalTimesDoneToday + "_";
        saveString += totalTimesDoneWeek + "_";
        saveString += totalTimesDoneMonth + "_";

        return saveString;
    }

    public void LoadDataByString (string _dataString)
    {
        string[] splittedString = _dataString.Split('_');
        if (splittedString.Length == 1)
            return;
        excerciseName = splittedString[0];
        Debug.Log(splittedString[2]);
        muscleGroup = (MuscleGroup)System.Enum.Parse(typeof(MuscleGroup), splittedString[1]);
        equipmentGroup = (EquipmentGroup)System.Enum.Parse(typeof(EquipmentGroup), splittedString[2]);
        setIsTimed = (splittedString[3] == "True");
        repDuration = int.Parse(splittedString[4]);
        breakDuration = int.Parse(splittedString[5]);
        setAmount = int.Parse(splittedString[6]);
        repetitionAmount = int.Parse(splittedString[7]);
        Debug.Log(splittedString[8]);
        totalTimesDone = int.Parse(splittedString[8]);
        totalTimesDoneToday = int.Parse(splittedString[9]);
        totalTimesDoneWeek = int.Parse(splittedString[10]);
        totalTimesDoneMonth = int.Parse(splittedString[11]);
    }

    public string GetFileNameString ()
    {
        return excerciseName + "_" + muscleGroup.ToString();
    }

    public string[] GetExcerciseContentAsString ()
    {
        List<string> ExcersiseString = new List<string>();
        ExcersiseString.Add("Name[" + excerciseName + "]");
        ExcersiseString.Add("Muscle Group[" + muscleGroup.ToString() + "]");
        ExcersiseString.Add("Equipment Group[" + equipmentGroup.ToString() + "]");
        ExcersiseString.Add("Uses a timer[" + setIsTimed + "]");
        ExcersiseString.Add("Set Amount[" + setAmount + "]");
        ExcersiseString.Add("Rep Amount[" + repetitionAmount + "]");
        ExcersiseString.Add("Rep Duration[" + repDuration + "]");
        ExcersiseString.Add("Break Duration[" + breakDuration + "]");
        ExcersiseString.Add("Total times done[" + totalTimesDone + "]");
        ExcersiseString.Add("Total times done today[" + totalTimesDoneToday + "]");
        ExcersiseString.Add("Total times done this week[" + totalTimesDoneWeek + "]");
        ExcersiseString.Add("Break times done this month[" + totalTimesDoneMonth + "]");

        return ExcersiseString.ToArray();
    }
}
