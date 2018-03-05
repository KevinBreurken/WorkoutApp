using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExerciseSelectionManager : MonoBehaviour {

    public static ExerciseSelectionManager instance = null;
    private ExerciseUIItem selectedItem;

    [SerializeField]
    private SelectedInfoDisplay selectedInfoDisplay;
    [SerializeField]
    private ExerciseScreen excersiseScreen;
    [SerializeField]
    private ExerciseEditPopup excersiseEdit;
    [SerializeField]
    private ExerciseManager excersiseManager;
    [SerializeField]
    private ExerciseListManager listManager;


    void Awake ()
    {
        instance = this;
    }
	
    public void StartSelection ()
    {
        excersiseScreen.currentExcersise = selectedItem.excercise;
        StateSelector.Instance.SetState("ExerciseScreen");
    }

    public void EditSelection ()
    {
        excersiseEdit.currentExcercise = selectedItem.excercise;
        excersiseEdit.createNewExcercise = false;
        excersiseEdit.Show();
    }

    public void DeleteSelected ()
    {
        excersiseManager.DeleteExcersise(selectedItem.excercise);
        OnDeselect();
        listManager.DisplayItems();
    }

    public void OnExcerciseSelected(ExerciseUIItem _item)
    {
        if (selectedItem != null)
            selectedItem.Deselect();

        selectedItem = _item;
        selectedItem.Select();

        selectedInfoDisplay.ShowScreen(_item);
    }

    public void OnDeselect ()
    {
        if (selectedItem != null)
            selectedItem.Deselect();
        selectedInfoDisplay.HideScreen();
    }

    public ExerciseUIItem GetSelectedItem ()
    {
        return selectedItem;
    }
}
