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

    [Header("")]
    [SerializeField]
    private ProgressViewSortUIItem yearSortUIItem;
    [SerializeField]
    private ProgressViewSortUIItem monthSortUIItem;
    [SerializeField]
    private ProgressViewSortUIItem exerciseSortUIItem;

    private bool sortDescending = true;

    void Awake ()
    {
        sortButton.onSortingChanged += OnSortingChanged;
        yearSortUIItem.onSortingChanged += OnSortingChanged;
        monthSortUIItem.onSortingChanged += OnSortingChanged;
    }

    private void OnSortingChanged ()
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
            bool createItem = true;
            if(yearSortUIItem.GetIndex() != 0)
            {
                Debug.Log("Year index: " + yearSortUIItem.GetIndex());
               int yearVal = System.DateTime.Now.Year - yearSortUIItem.GetIndex() + 1;
                Debug.Log("YearVal index: " + yearVal);
                Debug.Log("Image Val: " + newCarouselProgressImages[i].year);
                if (yearVal != newCarouselProgressImages[i].year)
                    createItem = false;
            }

            if (monthSortUIItem.GetIndex() != 0)
            {
                int monthVal = monthSortUIItem.GetIndex();
                if (monthVal != newCarouselProgressImages[i].month)
                    createItem = false;
            }

            if (createItem)
            {
                GameObject spawnedObject = Instantiate(uiItemPrefab, itemHolder);
                ProgressUIItem UIItem = spawnedObject.GetComponent<ProgressUIItem>();
                UIItem.progressImage = newCarouselProgressImages[i];
                UIItem.UpdateUI();
                UIItem.onRemoveClicked += UIItem_onRemoveClicked;
                spawnedItems.Add(UIItem);
            }
        }
    }

    private void UIItem_onRemoveClicked (ProgressUIItem _uiItem)
    {
        progressImageManager.RemoveProgressImage(_uiItem.progressImage);
        DisplayItems();
    }

}
