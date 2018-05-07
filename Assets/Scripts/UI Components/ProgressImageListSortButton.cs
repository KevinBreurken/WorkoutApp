using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressImageListSortButton : MonoBehaviour {

    public delegate void OnSortingClickedEvent (bool _state);
    public event OnSortingClickedEvent onSortingChanged = delegate
    { };

    private Button button;
    private Image image;

    public bool isDescending = true;

    [SerializeField]
    private Sprite ascendingSprite;
    [SerializeField]
    private Sprite descendingSprite;

    void Awake ()
    {
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OnToggleClicked);
        image = GetComponent<Image>();
        UpdateGraphic();
    }

    void OnToggleClicked ()
    {
        isDescending = !isDescending;
        onSortingChanged(isDescending);
        UpdateGraphic();
    }

    void UpdateGraphic ()
    {
        if (isDescending)
            image.sprite = ascendingSprite;
        else
            image.sprite = descendingSprite;

    }

    public bool GetState ()
    {
        return isDescending;
    }
}
