using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseState {

    public ExerciseManager excerciseManager;
    public ExerciseListManager excerciseListManager;

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
      
        excerciseListManager.Initialize();
        excerciseListManager.DisplayItems();
        ExerciseSelectionManager.instance.OnDeselect();
        return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        return base.Exit();
    }
}
