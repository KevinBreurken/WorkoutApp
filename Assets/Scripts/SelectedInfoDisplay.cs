using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfoDisplay : MonoBehaviour {
    [SerializeField]
    private Button startButton, editButton, deleteButton;
    private CanvasGroup CG;
    [SerializeField]
    private Text excersiseNameDisplay, groupDisplay, setAmountDisplay, repetitionAmountDisplay, repetitionLengthDuration, breakDisplay;
    [SerializeField]
    private Text totalTimesDisplay,totalTimesWeekDisplay,totalTimesMonthDisplay;
    [SerializeField]
    private CanvasGroup nameCG, groupCG;

    void Awake ()
    {
        CG = GetComponent<CanvasGroup>();
        startButton.onClick.AddListener(StartClicked);
        editButton.onClick.AddListener(EditClicked);
        deleteButton.onClick.AddListener(DeleteClicked);
    }

    void StartClicked ()
    {
        ExerciseSelectionManager.instance.StartSelection();
    }

    void EditClicked ()
    {
        ExerciseSelectionManager.instance.EditSelection();
    }

    void DeleteClicked ()
    {
        ExerciseSelectionManager.instance.DeleteSelected();
    }

    public void ShowScreen(ExerciseUIItem _item)
    {
        CG.alpha = 1;

        ExerciseUIItem selectedItem = ExerciseSelectionManager.instance.GetSelectedItem();
        Exercise exercise = selectedItem.excercise;
        excersiseNameDisplay.text = exercise.excerciseName;
        groupDisplay.text = exercise.muscleGroup.ToString();
        setAmountDisplay.text = exercise.setAmount.ToString();
        repetitionAmountDisplay.text = exercise.repetitionAmount.ToString();
        repetitionLengthDuration.gameObject.SetActive(exercise.setIsTimed);
        repetitionLengthDuration.text = exercise.repDuration.ToString() + "second duration";
        breakDisplay.text = exercise.breakDuration.ToString() + "second break";
        totalTimesDisplay.text = exercise.totalTimesDone.ToString();
        totalTimesWeekDisplay.text = exercise.totalTimesDoneWeek.ToString();
        totalTimesMonthDisplay.text = exercise.totalTimesDoneMonth.ToString();

    }

    public void HideScreen ()
    {
        CG.alpha = 0;
    }

    public IEnumerator DisplayData ()
    {
        DOTween.Kill(transform.GetInstanceID());
        nameCG.alpha = 0;
        groupCG.alpha = 0;
        setAmountDisplay.transform.localScale = Vector3.zero;
        repetitionAmountDisplay.transform.localScale = Vector3.zero;

        RectTransform nameRect = nameCG.transform.GetComponent<RectTransform>();
        nameRect.anchoredPosition = new Vector2(nameRect.anchoredPosition.x, 630);
        nameRect.DOAnchorPosY(582, 0.5f).SetId(transform.GetInstanceID());
        nameCG.DOFade(1, 0.5f).SetId(transform.GetInstanceID());
        yield return new WaitForSeconds(0.5f);

        RectTransform groupRect = groupCG.transform.GetComponent<RectTransform>();
        groupRect.anchoredPosition = new Vector2(-600, groupRect.anchoredPosition.y);
        groupRect.DOAnchorPosX(-890, 0.5f).SetId(transform.GetInstanceID());
        groupCG.DOFade(1, 0.5f).SetId(transform.GetInstanceID());
        yield return new WaitForSeconds(0.3f);
        setAmountDisplay.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetId(transform.GetInstanceID());
        repetitionAmountDisplay.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetId(transform.GetInstanceID());

    }
}
