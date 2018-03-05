using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseListManager : MonoBehaviour {

    [SerializeField]
    private ExerciseManager excerciseManager;
    [SerializeField]private GameObject uiItemPrefab;
    private List<ExerciseUIItem> spawnedItems = new List<ExerciseUIItem>();

    [SerializeField]
    private ExerciseEditPopup editPopup;
    [SerializeField]
    private RectTransform itemHolder;
    [SerializeField]
    private UIEnumSelector currentCategory;

    //Category Select
    [SerializeField]
    private Button newItemButton;

    void Awake ()
    {
        newItemButton.onClick.AddListener(NewItemClicked);
        currentCategory.Initialize(typeof(Exercise.MuscleGroup));
        currentCategory.onUpdated += CurrentCategory_onUpdated;
    }

    private void CurrentCategory_onUpdated ()
    {
        ShowCurrentCategory();
    }

    void NewItemClicked ()
    {
        editPopup.createNewExcercise = true;
        editPopup.Show();
    }

    public void Initialize ()
    {
        currentCategory.Reset();
        ShowCurrentCategory();
    }

	public void DisplayItems ()
    {
        for (int i = 0; i < spawnedItems.Count; i++)
        {
            Destroy(spawnedItems[i].gameObject);
        }
        spawnedItems.Clear();

        for (int i = 0; i < excerciseManager.exercises.Count; i++)
        {
            Exercise currentExercise = excerciseManager.exercises[i];
            GameObject spawnedObject = Instantiate(uiItemPrefab, itemHolder);
            ExerciseUIItem UIItem = spawnedObject.GetComponent<ExerciseUIItem>();
            UIItem.excercise = currentExercise;
            if (currentExercise.totalTimesDoneToday > 0)
                UIItem.SetDoneForToday();
            UIItem.UpdateUI();
            spawnedItems.Add(UIItem);
        }

        ShowCurrentCategory();
    }

    public void ShowCurrentCategory()
    {
        if ((Exercise.MuscleGroup)currentCategory.GetIndex() == Exercise.MuscleGroup.All)
        {
            for (int i = 0; i < spawnedItems.Count; i++)
            {
                spawnedItems[i].gameObject.SetActive(true);
            }
        }
        else
        {

            for (int i = 0; i < spawnedItems.Count; i++)
            {
                spawnedItems[i].gameObject.SetActive(spawnedItems[i].excercise.muscleGroup == (Exercise.MuscleGroup)currentCategory.GetIndex());
            }

        }
       
    }

}
