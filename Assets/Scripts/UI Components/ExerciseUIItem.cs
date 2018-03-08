using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseUIItem : MonoBehaviour {

    public Exercise excercise;

    [SerializeField]
    private Sprite normalSprite, activeSprite,selectedSprite;

    //UI
    [SerializeField]
    private Text excerciseNameText,workoutGroupNameText;
    [SerializeField]
    private Button button;
    private Image backgroundImage;

    private bool isDoneForToday;

    void Awake ()
    {
        backgroundImage = GetComponent<Image>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick ()
    {
        ExerciseSelectionManager.instance.OnExcerciseSelected(this);
    }

    public void Select ()
    {
        backgroundImage.sprite = selectedSprite;       
    }

    public void Deselect ()
    {
        if (isDoneForToday)
            backgroundImage.sprite = activeSprite;
        else
            backgroundImage.sprite = normalSprite;
       
    }

    public void UpdateUI ()
    {
        excerciseNameText.text = excercise.excerciseName;
        workoutGroupNameText.text = excercise.muscleGroup.ToString();
    }

    public void SetDoneForToday ()
    {
        isDoneForToday = true;
        backgroundImage.sprite = activeSprite;
    }

}
