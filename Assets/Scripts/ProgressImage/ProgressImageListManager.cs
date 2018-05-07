using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressImageListManager : MonoBehaviour {

    [SerializeField]
    private ProgressImageManager progressImageManager;
    [SerializeField]
    private ProgressImageListSortButton sortButton;
    [SerializeField]
    private GameObject uiItemPrefab;

    private List<ProgressUIItem> spawnedItems = new List<ProgressUIItem>();
    [SerializeField]
    private RectTransform itemHolder;

    private bool sortDescending = true;

    void Awake ()
    {
        sortButton.onSortingChanged += SortButton_onSortingChanged;
    }

    private void SortButton_onSortingChanged (bool _state)
    {
        DisplayItems();
    }

    public void DisplayItems ()
    {
        progressImageManager.SortImages();
        for (int i = 0; i < spawnedItems.Count; i++)
        {
            spawnedItems[i].onRemoveClicked -= UIItem_onRemoveClicked;
            Destroy(spawnedItems[i].gameObject);
        }
        spawnedItems.Clear();

        List<ProgressImage> newCarouselProgressImages = progressImageManager.GetAllProgressImages();
        newCarouselProgressImages.Sort((p1, p2) => p1.dateCompareValue.CompareTo(p2.dateCompareValue));
        if (sortButton.GetState())
        {
            newCarouselProgressImages.Reverse();
        }

        for (int i = 0; i < newCarouselProgressImages.Count; i++)
        {
            GameObject spawnedObject = Instantiate(uiItemPrefab, itemHolder);
            ProgressUIItem UIItem = spawnedObject.GetComponent<ProgressUIItem>();
            UIItem.progressImage = newCarouselProgressImages[i];
            UIItem.UpdateUI();
            UIItem.onRemoveClicked += UIItem_onRemoveClicked;
            spawnedItems.Add(UIItem);
        }
    }

    private void UIItem_onRemoveClicked (ProgressUIItem _uiItem)
    {
        progressImageManager.RemoveProgressImage(_uiItem.progressImage);
        DisplayItems();
    }

}
