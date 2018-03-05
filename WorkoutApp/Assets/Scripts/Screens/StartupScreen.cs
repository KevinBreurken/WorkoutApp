using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupScreen : BaseState {

    [SerializeField]
    private ExerciseManager exerciseManager;
    [SerializeField]
    private DateManager dateManager;

    public override IEnumerator Enter ()
    {
        
        exerciseManager.LoadData();

        yield return new WaitForSeconds(1);
        dateManager.CheckDate();
        StateSelector.Instance.SetState("MainScreen");
        yield return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        return base.Exit();
    }
}
