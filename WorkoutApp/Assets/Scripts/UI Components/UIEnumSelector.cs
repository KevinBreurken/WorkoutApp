using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnumSelector : MonoBehaviour {

    public delegate void OnSelectorClickedEvent ();
    public event OnSelectorClickedEvent onNextClicked = delegate
    { };
    public event OnSelectorClickedEvent onPreviousClicked = delegate
    { };
    public event OnSelectorClickedEvent onUpdated = delegate
    { };

    [Header("UI")]
    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button rightButton;
    [SerializeField]
    private Text textDisplay;

    
    private int enumIndex;
    private int maxEnumIndex;
    private string[] enumStrings;

    void Awake ()
    {
        leftButton.onClick.AddListener(OnPreviousClicked);
        rightButton.onClick.AddListener(OnNextClicked);
    }

    public void Initialize (System.Type _enumType)
    {
        string[] names = System.Enum.GetNames(_enumType);
        maxEnumIndex = names.Length;
        enumStrings = names;
    }

    public void SetIndex(int _val)
    {
        enumIndex = _val;
    }
    public int GetIndex ()
    {
        return enumIndex;
    }


    protected virtual void OnPreviousClicked () {
        enumIndex--;
        if (enumIndex < 0)
            enumIndex = maxEnumIndex;
        onPreviousClicked();
        UpdateText();
        onUpdated();
    }

    protected virtual void OnNextClicked ()
    {
        enumIndex++;
        if (enumIndex > maxEnumIndex)
            enumIndex = 0;
        onNextClicked();
        UpdateText();
        onUpdated();
    }

    public virtual void Reset ()
    {
        enumIndex = 0;
        UpdateText();
        onUpdated();
    }

    public void UpdateText()
    {
        textDisplay.text = enumStrings[enumIndex];
    }
}
