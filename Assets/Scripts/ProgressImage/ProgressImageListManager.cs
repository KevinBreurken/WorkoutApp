using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressImageListManager : MonoBehaviour {

    [SerializeField]
    private ProgressImageManager progressImageManager;

    [SerializeField]
    private GameObject uiItemPrefab;

    private List<ProgressUIItem> spawnedItems = new List<ProgressUIItem>();
    [SerializeField]
    private RectTransform itemHolder;

    public void DisplayItems ()
    {
        progressImageManager.SortImages();
        for (int i = 0; i < spawnedItems.Count; i++)
        {
            spawnedItems[i].onRemoveClicked -= UIItem_onRemoveClicked;
            Destroy(spawnedItems[i].gameObject);
        }
        spawnedItems.Clear();

        for (int i = 0; i < progressImageManager.GetAllProgressImages().Count; i++)
        {
            GameObject spawnedObject = Instantiate(uiItemPrefab, itemHolder);
            ProgressUIItem UIItem = spawnedObject.GetComponent<ProgressUIItem>();
            UIItem.progressImage = progressImageManager.GetAllProgressImages()[i];
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
