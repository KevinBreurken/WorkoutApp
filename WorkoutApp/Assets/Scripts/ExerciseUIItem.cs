using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseUIItem : MonoBehaviour {

    public Exercise excercise;

    [SerializeField]
    private Color doneTodayColor;
    //UI
    [SerializeField]
    private Text excerciseNameText,workoutGroupNameText;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Button button;


    void Awake ()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick ()
    {
        ExerciseSelectionManager.instance.OnExcerciseSelected(this);
    }

    public void Select ()
    {
        GetComponent<Image>().color = Color.green;
    }

    public void Deselect ()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void UpdateUI ()
    {
        excerciseNameText.text = excercise.excerciseName;
        workoutGroupNameText.text = excercise.muscleGroup.ToString();

    }

    public void SetDoneForToday ()
    {
        backgroundImage.color = doneTodayColor;
    }

}
