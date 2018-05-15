using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfoDisplay : MonoBehaviour {

    [SerializeField]
    private ValueSetterDisplay setsValueDisplay,setDurationValueDisplay,breakValueDisplay;

    [SerializeField]
    private Button startButton, editButton, deleteButton;
    private CanvasGroup CG;
    [SerializeField]
    private Text excersiseNameDisplay,equipmentDisplay;
    [SerializeField]
    private Text totalTimesDisplay,totalTimesMonthDisplay;
    [SerializeField]
    private CanvasGroup nameCG;

    [Header("Animation")]
    [SerializeField]
    private RectTransform exerciseLabel;
    [SerializeField]
    private RectTransform workoutGroupLabel,equipmentLabel;
    [SerializeField]
    private CanvasGroup valuesDisplay;
    [SerializeField]
    private RectTransform buttonHolder;
    [SerializeField]
    private RectTransform infoHolder;

    void Awake ()
    {
        CG = GetComponent<CanvasGroup>();
        startButton.onClick.AddListener(StartClicked);
        editButton.onClick.AddListener(EditClicked);
        deleteButton.onClick.AddListener(DeleteClicked);
    }

    private IEnumerator Show ()
    {
        CG.alpha = 1;
        valuesDisplay.alpha = 0;
        //set start position
        exerciseLabel.anchoredPosition = new Vector2(1540, exerciseLabel.anchoredPosition.y);
        workoutGroupLabel.anchoredPosition = new Vector2(1540, workoutGroupLabel.anchoredPosition.y);
        equipmentLabel.anchoredPosition = new Vector2(1540, equipmentLabel.anchoredPosition.y);
        infoHolder.anchoredPosition = new Vector2(1540, infoHolder.anchoredPosition.y);
        buttonHolder.anchoredPosition = new Vector2(1000, buttonHolder.anchoredPosition.y);

        exerciseLabel.DOAnchorPosX(0, 0.5f);
        workoutGroupLabel.DOAnchorPosX(0, 0.5f).SetDelay(0.2f);
        equipmentLabel.DOAnchorPosX(0, 0.5f).SetDelay(0.4f);
        infoHolder.DOAnchorPosX(-124.4f, 0.5f).SetDelay(0.6f);
        valuesDisplay.DOFade(1, 0.5f).SetDelay(0.7f);
        buttonHolder.DOAnchorPosX(-26f, 0.5f).SetDelay(1);

        yield return new WaitForSeconds(1);
    }

    private IEnumerator Hide ()
    {
        buttonHolder.DOAnchorPosX(1000, 0.5f);
        valuesDisplay.DOFade(0, 0.5f);
        exerciseLabel.DOAnchorPosX(1540, 0.5f);
        workoutGroupLabel.DOAnchorPosX(1540, 0.5f);
        equipmentLabel.DOAnchorPosX(1540, 0.5f);
        infoHolder.DOAnchorPosX(1540, 0.5f);
        yield return new WaitForSeconds(0.6f);

        CG.alpha = 0;
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
        StartCoroutine("Show");
        ExerciseUIItem selectedItem = ExerciseSelectionManager.instance.GetSelectedItem();
        Exercise exercise = selectedItem.excercise;
        excersiseNameDisplay.text = exercise.excerciseName;
        totalTimesDisplay.text = exercise.totalTimesDone.ToString();
        totalTimesMonthDisplay.text = exercise.totalTimesDoneMonth.ToString();
        equipmentDisplay.text = exercise.equipmentGroup.ToString();

        setsValueDisplay.Show();
        breakValueDisplay.Show();

        if (exercise.setIsTimed)
        {
            setDurationValueDisplay.Show();
        }
        else
        {
            setDurationValueDisplay.Hide();
        }

    }

    public void HideDirect ()
    {
        CG.alpha = 0;
    }

    public void HideScreen ()
    {
        StartCoroutine("Hide");
    }

    public IEnumerator DisplayData ()
    {
        DOTween.Kill(transform.GetInstanceID());
        nameCG.alpha = 0;
        RectTransform nameRect = nameCG.transform.GetComponent<RectTransform>();
        nameRect.anchoredPosition = new Vector2(nameRect.anchoredPosition.x, 630);
        nameRect.DOAnchorPosY(582, 0.5f).SetId(transform.GetInstanceID());
        nameCG.DOFade(1, 0.5f).SetId(transform.GetInstanceID());
        yield return new WaitForSeconds(0.3f);
 
    }
}
