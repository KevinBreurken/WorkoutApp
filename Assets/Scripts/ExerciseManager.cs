using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExerciseManager : MonoBehaviour {

    [SerializeField]
    private ContentDataManager contentDataManager;
    public List<Exercise> exercises = new List<Exercise>();
    public bool ClearData;
    private bool isloaded = false;

    private string saveLocation;

	public void LoadData ()
    {
        if (isloaded)
            return;
        isloaded = true;

        exercises = contentDataManager.LoadAllExcercise();
      
    }

    public void AddNewExcercise (Exercise _excercise)
    {
        exercises.Add(_excercise);
        contentDataManager.SaveExcercise(_excercise);
    }

    public void ReplaceExcersise(Exercise _toReplace,Exercise _toAdd)
    {
        contentDataManager.DeleteExcercise(_toReplace);
        contentDataManager.SaveExcercise(_toAdd);

        exercises.Add(_toAdd);
        exercises.Remove(_toReplace);
    }

    public void DeleteExcersise(Exercise _toRemove)
    {
        contentDataManager.DeleteExcercise(_toRemove);
        exercises.Remove(_toRemove);
    }

    public void ClearWeeks ()
    {
        for (int i = 0; i < exercises.Count; i++)
        {
            exercises[i].totalTimesDoneWeek = 0;
        }
    }

    public void ClearMonths ()
    {
        for (int i = 0; i < exercises.Count; i++)
        {
            exercises[i].totalTimesDoneMonth = 0;
        }
    }

    public void SaveData ()
    {
        for (int i = 0; i < exercises.Count; i++)
        {
            contentDataManager.SaveExcercise(exercises[i]);
        }
       
    }

}
