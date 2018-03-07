using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExerciseWebcamSwitch : MonoBehaviour {

    [SerializeField]
    private RawImage imageOutput;
    [SerializeField]
    private WebCamTexture webcamTexture;
    [SerializeField]
    private CanvasGroup CG,exerciseCG;
    [SerializeField]
    private Button webcamButton;

    private bool isShown;
    // Use this for initialization
    void Awake () {
        webcamTexture = new WebCamTexture();
        imageOutput.texture = webcamTexture;
        imageOutput.material.mainTexture = webcamTexture;

#if !UNITY_EDITOR

        webcamTexture.deviceName = WebCamTexture.devices[1].name;
#endif
        webcamButton.onClick.AddListener(OnSwitchClicked);
    }

    void OnSwitchClicked ()
    {
        
        if (isShown)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Initialize ()
    {
        isShown = false;
        CG.alpha = 0;
        exerciseCG.alpha = 1;
    }
    private void Hide ()
    {
        isShown = false;

        webcamTexture.Pause();
        DOTween.Kill(transform.GetInstanceID());
        CG.DOFade(0, 0.5f).SetId(transform.GetInstanceID());
        exerciseCG.DOFade(1, 0.5f).SetId(transform.GetInstanceID());
    }

    private void Show ()
    {
        webcamTexture.Play();
        isShown = true;
        DOTween.Kill(transform.GetInstanceID());
        CG.DOFade(1, 0.5f).SetId(transform.GetInstanceID());
        exerciseCG.DOFade(0.5f, 0.5f).SetId(transform.GetInstanceID());
    }
}
