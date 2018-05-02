using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressImageGallery : MonoBehaviour {

    public static ProgressImageGallery instance = null;
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private Button returnButton;

    private CanvasGroup CG;

    void Awake ()
    {
        instance = this;
        CG = GetComponent<CanvasGroup>();
        CG.alpha = 0;
        CG.blocksRaycasts = false;
        CG.interactable = false;

        returnButton.onClick.AddListener(Hide);
    }

    public void SetImage (Texture _tex)
    {
        image.texture = _tex;
    }

    public void Show ()
    {
        CG.DOFade(1, 0.5f);
        CG.blocksRaycasts = true;
        CG.interactable = true;
    }

    public void Hide ()
    {
        CG.DOFade(0, 0.5f);
        CG.blocksRaycasts = false;
        CG.interactable = false;
    }

}
