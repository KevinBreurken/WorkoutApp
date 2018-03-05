using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSelector : MonoBehaviour {

    public static StateSelector Instance = null;
    [SerializeField]
    private BaseState[] states;
    [SerializeField]
    private BaseState startState;
    private BaseState currentState;

    void Awake ()
    {
        Instance = this;

        for (int i = 0; i < states.Length; i++)
        {
            states[i].gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
        if (startState == null)
            Debug.LogError("No StartState selected.");
        else
        StartCoroutine(SetState(startState));
	}
	
	public void SetState (string _stateName)
    {
        BaseState foundState = null;

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].GetType().ToString() == _stateName)
                foundState = states[i];
        }

        if (foundState == null)
            Debug.LogError("State[" + _stateName + "] not found");
        else
        StartCoroutine(SetState(foundState));
        
    }

    public IEnumerator SetState(BaseState _state)
    {
        if (currentState != null)
        {
            yield return StartCoroutine(currentState.Exit());
            currentState.gameObject.SetActive(false);
        }

        _state.gameObject.SetActive(true);
        StartCoroutine(_state.Enter());
        currentState = _state;
    }
}
