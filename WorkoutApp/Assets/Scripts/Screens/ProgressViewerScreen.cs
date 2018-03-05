using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressViewerScreen : BaseState {

    [SerializeField]
    private ContentDataManager contentDataManager;
    [SerializeField]
    private ProgressImageListManager progressImageListManager;

    private List<ProgressImage> loadedImages;
    [SerializeField]
    private Button returnButton,takePictureButton;

    void Awake ()
    {
        returnButton.onClick.AddListener(OnReturnButtonClicked);
        takePictureButton.onClick.AddListener(OnTakePictureButtonClicked);
    }

    void OnTakePictureButtonClicked ()
    {
        StateSelector.Instance.SetState("TakePictureScreen");
    }

    void OnReturnButtonClicked ()
    {
        StateSelector.Instance.SetState("MainScreen");
    }

    public override IEnumerator Enter ()
    {
        progressImageListManager.DisplayItems();
        return base.Enter();
    }

    public override IEnumerator Exit ()
    {
        return base.Exit();
    }
}
