using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueSetterDisplay : MonoBehaviour {

    private int setterValue;
    public int SetterValue
    {
        get
        {
            return setterValue;
        }
        set
        {
            if (SetterValue + value <= -1)
                return;
            setterValue = value;
            UpdateDisplay();

        }
    }

    [SerializeField]
    private int defaultValue;
    [SerializeField]
    private int valueIncreasePerPress = 1;
    [SerializeField]
    private Button increaseButton,decreaseButton;
    [SerializeField]
    private Text displayText;

    private CanvasGroup CG;

    void Awake ()
    {

        increaseButton.onClick.AddListener(OnIncreaseClicked);
        decreaseButton.onClick.AddListener(OnDecreaseClicked);
        CG = GetComponent<CanvasGroup>();

    }
	
    void OnIncreaseClicked ()
    {
        SetterValue += valueIncreasePerPress;
    }

    void OnDecreaseClicked ()
    {
        SetterValue -= valueIncreasePerPress;
    }

    private void UpdateDisplay ()
    {
        displayText.text = "" + SetterValue;
    }

    private void Reset ()
    {
        SetterValue = defaultValue;
    }

    public void Hide ()
    {
        gameObject.SetActive(false);

    }

    public void Show ()
    {
        Reset();
        gameObject.SetActive(true);

    }

}
