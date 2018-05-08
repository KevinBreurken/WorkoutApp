using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressImageManager : MonoBehaviour {

    [SerializeField]
    private ContentDataManager contentDataManager;

    private List<ProgressImage> loadedImages;

    void Start ()
    {
        loadedImages = contentDataManager.GetAllProgressImages();
    }

    public void SortImages ()
    {
        for (int i = 0; i < loadedImages.Count; i++)
        {
            loadedImages[i].CalculateDateValue();
        }
        loadedImages.Sort( delegate (ProgressImage x, ProgressImage y)
         {
             return x.dateCompareValue.CompareTo(y.dateCompareValue);
         });
        loadedImages.Reverse();
         
    }
    public List<ProgressImage> GetAllProgressImages ()
    {
        
        return loadedImages;
    }

    public void AddProgressImage (ProgressImage _progressImage)
    {
        loadedImages.Add(_progressImage);
        contentDataManager.SaveProgressImage(_progressImage);
    }

    public void RemoveProgressImage(ProgressImage _progressImage)
    {
        contentDataManager.RemoveProgressImage(_progressImage);
        loadedImages.Remove(_progressImage);
    }

    public ProgressImage GetOldestProgressImage ()
    {
        ProgressImage oldestImage = loadedImages[0];

        for (int i = 0; i < loadedImages.Count; i++)
        {
            if (loadedImages[i].dateCompareValue < oldestImage.dateCompareValue)
                oldestImage = loadedImages[i];
        }

        return oldestImage;
    }

}
