using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ExerciseEditPopup : MonoBehaviour {

    [SerializeField]
    private ExerciseManager excerciseManager;
    [SerializeField]
    private ExerciseListManager listManager;
    public bool createNewExcercise;
    public Exercise currentExcercise;

    //UI
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Button cancelButton,saveButton;
    [SerializeField]
    private UIEnumSelector muscleGroup, equipmentGroup;
    //Input fields
    [SerializeField]
    private InputField nameInput,setDurationInput,timeBetweenSetsInput,setAmountInput,repetitionInput;
    [SerializeField]private Toggle isTimedToggle;

    void Awake ()
    {
        isTimedToggle.onValueChanged.AddListener(delegate {
            OnValueChange(isTimedToggle);
        });
        cancelButton.onClick.AddListener(OnCancelClick);
        saveButton.onClick.AddListener(OnSaveClick);
        muscleGroup.Initialize(typeof(Exercise.MuscleGroup));
        equipmentGroup.Initialize(typeof(Exercise.EquipmentGroup));
        canvasGroup = GetComponent<CanvasGroup>();
        DirectHide();
    }

    #region UI_Event

    void OnValueChange(Toggle _toggle)
    {
        setDurationInput.transform.parent.gameObject.SetActive(_toggle.isOn);
    }

    void OnCancelClick ()
    {
        Hide();
    }

    void OnSaveClick ()
    {
        if (createNewExcercise)
        {
            excerciseManager.AddNewExcercise(GetMadeExcercise());
        }
        else
        {
            excerciseManager.ReplaceExcersise(currentExcercise, GetMadeExcercise());
        }

        listManager.DisplayItems();
        Hide();
    }
    #endregion



    void DirectHide ()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    void Hide ()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 0.5f);
        canvasGroup.interactable = false;
    }

    public void Show ()
    {
        ExerciseSelectionManager.instance.OnDeselect();
        if (createNewExcercise)
        {
            muscleGroup.Reset();
            equipmentGroup.Reset();
            isTimedToggle.isOn = false;
            nameInput.text = "New Excercise";
            setDurationInput.text = "30";
            timeBetweenSetsInput.text = "30";
            setAmountInput.text = "5";
            repetitionInput.text = "10";
        }
        else
        {
            nameInput.text = currentExcercise.excerciseName;
            setDurationInput.text = currentExcercise.repDuration.ToString();
            setAmountInput.text = currentExcercise.setAmount.ToString();
            timeBetweenSetsInput.text = currentExcercise.breakDuration.ToString();
            muscleGroup.SetIndex((int)currentExcercise.muscleGroup);
            equipmentGroup.SetIndex((int)currentExcercise.equipmentGroup);
            isTimedToggle.isOn = currentExcercise.setIsTimed;
            repetitionInput.text = currentExcercise.repetitionAmount.ToString();
        }
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 0.5f);
        canvasGroup.interactable = true;
        muscleGroup.UpdateText();
    }

    private Exercise GetMadeExcercise ()
    {
        Exercise newExcercise = new Exercise();
        newExcercise.excerciseName = nameInput.text;
        
        newExcercise.setAmount = int.Parse(setAmountInput.text);
        newExcercise.repetitionAmount = int.Parse(repetitionInput.text);
        newExcercise.breakDuration = int.Parse(timeBetweenSetsInput.text);
        if (isTimedToggle.isOn)
        {
            newExcercise.repDuration = int.Parse(setDurationInput.text);
        }
        else
        {
            newExcercise.repDuration = 0;
        }

        newExcercise.muscleGroup = (Exercise.MuscleGroup)muscleGroup.GetIndex();
        newExcercise.equipmentGroup = (Exercise.EquipmentGroup)equipmentGroup.GetIndex();
        newExcercise.setIsTimed = isTimedToggle.isOn;
        return newExcercise;
    }

}
