using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExerciseScreen : BaseState {

    [SerializeField]
    private ValueSetterDisplay setsValueDisplay, setDurationValueDisplay, breakValueDisplay;
    [SerializeField]
    private ExerciseManager exerciseManager;
    [SerializeField]
    private ExerciseWebcamSwitch webcamSwitch;
    public Exercise currentExcersise;
    public bool isInBreak;
    private float currentTime;
    private bool timerActive;
    private int currentSet;
    //UI
    [SerializeField]
    private RectTransform circleHolder;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Text excersiseText, timerText, setsText;
    [SerializeField]
    private CanvasGroup continueTextCG, timerTextCG,descriptionTextCG,setsCG;
    [SerializeField]
    private Button actionButton,returnButton;
    [SerializeField]
    private RectTransform webcamButtonTransform;

    private CanvasGroup CG;

    void Awake ()
    {
        CG = GetComponent<CanvasGroup>();
        actionButton.onClick.AddListener(OnActionButtonClicked);
        returnButton.onClick.AddListener(OnReturnClicked);
    }

    void OnReturnClicked ()
    {
        StateSelector.Instance.SetState("MenuScreen");
    }

    void OnActionButtonClicked ()
    {
        if (isInBreak)
        {
            timerActive = !timerActive;
        }
        else
        {
            if (!currentExcersise.setIsTimed)
            {
                StartCoroutine(GoToBreak());
            }
            else
            {
                timerActive = !timerActive;
            }
        }
    }

    public override IEnumerator Enter ()
    {
        webcamSwitch.Initialize();
        isInBreak = false;
        timerActive = false;
        currentSet = 0;
        CG.alpha = 1;
        fillImage.fillAmount = 0;
        timerTextCG.alpha = 0;
        descriptionTextCG.alpha = 0;
        continueTextCG.alpha = 0;
        setsCG.alpha = 0;
        webcamButtonTransform.localScale = Vector2.zero;
        circleHolder.transform.localScale = Vector3.zero;
        returnButton.transform.localScale = Vector2.zero;

        webcamButtonTransform.DOScale(1, 0.5f);
        returnButton.transform.DOScale(1, 0.5f);
        circleHolder.DOScale(1, 0.5f);
        fillImage.DOFillAmount(1, 0.5f).SetDelay(0.5f);
        yield return new WaitForSeconds(1);
        StartCoroutine(GoToExcersise());
        yield return base.Enter();
    }

    void Update ()
    {
        if (!timerActive)
            return;

        currentTime -= Time.deltaTime;
        timerText.text = currentTime.ToString("F2");

        if (currentTime <= 0)
        {
            timerText.text = "0";
            if (isInBreak)
            {
                StartCoroutine(GoToExcersise());
            }
            else
            {
                StartCoroutine(GoToBreak());
            }
            timerActive = false;
        }
    }

    public IEnumerator GoToBreak ()
    {
        yield return null;
        
        if (currentSet == setsValueDisplay.SetterValue)
        {
            currentExcersise.AddTotalTimesCount();
            exerciseManager.SaveData();
            StateSelector.Instance.SetState("MenuScreen");
        }
        else
        {

            currentTime = breakValueDisplay.SetterValue;
            isInBreak = true;
            continueTextCG.DOFade(0, 0.5f);
            timerTextCG.DOFade(0, 0.5f);
            setsCG.DOFade(0, 0.5f);
            descriptionTextCG.DOFade(0, 0.5f);
            yield return new WaitForSeconds(1);
            excersiseText.text = "Break";
            timerTextCG.DOFade(1, 0.5f);
            descriptionTextCG.DOFade(1, 0.5f);
            timerActive = true;
        }
        
    }

    public IEnumerator GoToExcersise ()
    {
        yield return null;
        
        isInBreak = false;
        currentTime = setDurationValueDisplay.SetterValue;
        currentSet++;
        setsText.text = "Set(" + currentSet + "/" + setsValueDisplay.SetterValue +")";
        descriptionTextCG.DOFade(0, 0.5f);
        continueTextCG.DOFade(0, 0.5f);
        timerTextCG.DOFade(0, 0.5f);
        setsCG.DOFade(0, 0.5f);
        yield return new WaitForSeconds(1);
        excersiseText.text = currentExcersise.excerciseName;
        descriptionTextCG.DOFade(1, 0.5f);
        setsCG.DOFade(1, 0.5f);
        if (currentExcersise.setIsTimed)
        {
            timerTextCG.DOFade(1, 0.5f);
            timerActive = true;
        }
        else
        {
            continueTextCG.DOFade(1, 0.5f);
        }
        
    }

    public override IEnumerator Exit ()
    {

        webcamButtonTransform.DOScale(0, 0.5f);
        returnButton.transform.DOScale(0, 0.5f);
        CG.DOFade(0, 0.5f).SetDelay(0.5f);
        yield return new WaitForSeconds(1f);
        yield return base.Exit();
    }
}
