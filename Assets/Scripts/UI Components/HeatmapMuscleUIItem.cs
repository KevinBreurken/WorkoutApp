using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatmapMuscleUIItem : MonoBehaviour {

    [SerializeField]
    private Text titleText, timesText;
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
        //ExerciseSelectionManager.instance.OnExcerciseSelected(this);
    }

    public void UpdateUI (string _title,string _timesText)
    {
        titleText.text = _title;
        timesText.text = _timesText;
    }
}
