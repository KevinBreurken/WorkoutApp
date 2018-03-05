﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TakePictureScreen : BaseState {

    [SerializeField]
    private ProgressImageManager progressImageManager;

    [SerializeField]
    private RawImage imageOutput;
    [SerializeField]
    private WebCamTexture webcamTexture;

    [SerializeField]
    private Button useButton, retryButton,cancelButton;

    [SerializeField]
    private CanvasGroup textCG;
    [SerializeField]
    private Text timerText;

    //timer
    private float timer;
    private bool isTicking;

    void Awake ()
    {
        webcamTexture = new WebCamTexture();
        imageOutput.texture = webcamTexture;
        imageOutput.material.mainTexture = webcamTexture;

        webcamTexture.deviceName = WebCamTexture.devices[0].name;
#if !UNITY_EDITOR

        webcamTexture.deviceName = WebCamTexture.devices[1].name;
#endif

        useButton.onClick.AddListener(OnUseButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);

    }

    void OnUseButtonClicked ()
    {
        ProgressImage createdImage = new ProgressImage();
        createdImage.date = System.DateTime.Now.ToShortDateString();
        Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height);
        snap.SetPixels(webcamTexture.GetPixels());
        snap.Apply();
        createdImage.texture = snap;
        progressImageManager.AddProgressImage(createdImage);
        StateSelector.Instance.SetState("ProgressViewerScreen");
    }

    void OnCancelButtonClicked ()
    {
        StateSelector.Instance.SetState("ProgressViewerScreen");
    }

    void OnRetryButtonClicked ()
    {
        timer = 5;
        isTicking = true;
        textCG.DOFade(1, 0.5f);
        imageOutput.transform.DOScale(new Vector3(-1,1,1), 0.5f);
        webcamTexture.Play();
    }

    void Update ()
    {
        if (!isTicking)
            return;

        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F0");

        if(timer <= 0)
        {
            timerText.text = "0";

            isTicking = false;
            StopCamera();
        }
    }

    
    private void StopCamera ()
    {
        webcamTexture.Pause();
        textCG.DOFade(0, 0.5f);
        imageOutput.transform.DOScale(0.5f, 0.5f);
    }

    public override IEnumerator Enter ()
    {
        timer = 5;
        imageOutput.transform.localScale = new Vector3(-1,1,1);
        textCG.alpha = 0;
        textCG.DOFade(1, 0.5f);
        webcamTexture.Play();
        isTicking = true;
        return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        return base.Exit();
    }
}
