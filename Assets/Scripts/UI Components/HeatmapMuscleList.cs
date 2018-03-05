using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapMuscleList : MonoBehaviour {

    [SerializeField]
    private HeatmapScreen heatmapScreen;
    [SerializeField]
    private GameObject uiItemPrefab;
    private List<HeatmapMuscleUIItem> spawnedItems = new List<HeatmapMuscleUIItem>();

    [SerializeField]
    private RectTransform itemHolder;
    [SerializeField]
    private UIEnumSelector currentCategory;

    void Awake ()
    {
        CreateUIItems();
    }

    private void CreateUIItems ()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Exercise.MuscleGroup)).Length; i++)
        {
            //ignore first
            if(i != 0)
            {
                GameObject spawnedObject = Instantiate(uiItemPrefab, itemHolder);
                HeatmapMuscleUIItem UIItem = spawnedObject.GetComponent<HeatmapMuscleUIItem>();

                spawnedItems.Add(UIItem);
            }
        }
        
    }

    private void CurrentCategory_onUpdated ()
    {
        DisplayItems();
    }

    public void DisplayItems ()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Exercise.MuscleGroup)).Length; i++)
        {
            if (i != 0)
            {
                Debug.Log(spawnedItems[i - 1]);
                Debug.Log(((Exercise.MuscleGroup)i).ToString());
                Debug.Log(heatmapScreen.countAmountList[i]);

                spawnedItems[i - 1].UpdateUI(((Exercise.MuscleGroup)i).ToString(),"Amount: " + heatmapScreen.countAmountList[i]);
            }
        }
    }
}
