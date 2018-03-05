using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainScreen : BaseState {

    public RectTransform title;
    [SerializeField]
    private Button excerciseButton,progressButton;
    [SerializeField]
    private RectTransform buttonGroup;

    [SerializeField]
    private CanvasGroup logoCG;

    void Awake ()
    {
       
        excerciseButton.onClick.AddListener(OnExcerciseButtonClicked);
        progressButton.onClick.AddListener(OnProgressButtonClicked);
    }

    void OnExcerciseButtonClicked ()
    {
        StateSelector.Instance.SetState("MenuScreen");
    }

    void OnProgressButtonClicked ()
    {
        StateSelector.Instance.SetState("ProgressViewerScreen");
    }

    public override IEnumerator Enter ()
    {
        title.localScale = Vector3.zero;
        buttonGroup.transform.localScale = Vector3.zero;
        logoCG.transform.localScale = Vector3.zero;
        logoCG.alpha = 0;

        logoCG.transform.DOScale(1, 1f);
        logoCG.DOFade(1, 1f);
        yield return new WaitForSeconds(0.6f);

        title.DOScale(1, 0.5f);
        buttonGroup.transform.DOScale(1, 0.5f).SetDelay(1);
        yield return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        logoCG.DOFade(0, 0.5f);
        logoCG.transform.DOScale(0, 0.5f);
        title.DOScale(0, 0.5f);
        buttonGroup.transform.DOScale(0, 0.5f);

        yield return new WaitForSeconds(0.6f);
        yield return base.Exit();
    }
}
