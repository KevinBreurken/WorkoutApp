using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuScreen : BaseState {

    public ExerciseManager excerciseManager;
    public ExerciseListManager excerciseListManager;
    public SelectedInfoDisplay selectedInfoDisplay;
    [SerializeField]
    private Button returnButton;
    
    void Awake ()
    {
        returnButton.onClick.AddListener(OnReturnClicked);
    }

    void OnReturnClicked ()
    {
        StateSelector.Instance.SetState("MainScreen");
    }

    public override IEnumerator Enter ()
    {
        returnButton.transform.localScale = Vector3.zero;
        returnButton.transform.DOScale(1, 0.5f);
        excerciseListManager.Show();
        excerciseListManager.Initialize();
        excerciseListManager.DisplayItems();
        selectedInfoDisplay.HideDirect();
        return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        selectedInfoDisplay.HideScreen();
        excerciseListManager.Hide();
        returnButton.transform.DOScale(0, 0.3f);
        yield return new WaitForSeconds(0.6f);
        yield return base.Exit();
    }
}
