using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : BaseState {

    public ExerciseManager excerciseManager;
    public ExerciseListManager excerciseListManager;

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
