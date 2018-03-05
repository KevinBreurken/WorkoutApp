using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUIItem : MonoBehaviour {

    public delegate void OnProgressUIItemClickedEvent (ProgressUIItem _uiItem);
    public event OnProgressUIItemClickedEvent onRemoveClicked = delegate
    { };

    public ProgressImage progressImage;
    [SerializeField]
    private Button removeButton;
    [SerializeField]
    private RawImage imageOutput;
    [SerializeField]
    private Text dateText;

    void Awake ()
    {
        removeButton.onClick.AddListener(OnRemoveClicked);
    }

    private void OnRemoveClicked ()
    {
        onRemoveClicked(this);
    }

	public void UpdateUI ()
    {
        imageOutput.texture = progressImage.texture;
        dateText.text = progressImage.date;

    }


}
