using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ContentDataManager : MonoBehaviour {

    private string saveLocation;
    private const string EXCERSISEFOLDER = "/Excersises";
    private const string IMAGESFOLDER = "/ProgressViewer";
    private const string DATEFOLDER = "/Date";

    void Awake ()
    {
        saveLocation = Application.persistentDataPath;
        CheckForAvailableFolders();
    }

    /// <summary>
    /// Creates the required directories if needed.
    /// </summary>
    private void CheckForAvailableFolders ()
    {
        if (Directory.Exists(saveLocation + EXCERSISEFOLDER) == false)
            Directory.CreateDirectory(saveLocation + EXCERSISEFOLDER);

        if (Directory.Exists(saveLocation + IMAGESFOLDER) == false)
            Directory.CreateDirectory(saveLocation + IMAGESFOLDER);

        if (Directory.Exists(saveLocation + DATEFOLDER) == false)
        {
            Directory.CreateDirectory(saveLocation + DATEFOLDER);
            
            File.CreateText(saveLocation + DATEFOLDER + "/" + "date" + ".txt");
        }
        else
        {
            if(File.Exists(saveLocation + DATEFOLDER + "/" + "date" + ".txt") == false)
                File.CreateText(saveLocation + DATEFOLDER + "/" + "date" + ".txt");
        }
    }

    #region DateHandling
    public void SaveDate (string[] _data)
    {
        string directoryString = saveLocation + DATEFOLDER + "/" + "date" + ".txt";
        File.WriteAllLines(directoryString, _data);
    }

    public string[] LoadDate ()
    {
        string directoryString = saveLocation + DATEFOLDER + "/" + "date" + ".txt";
        return File.ReadAllLines(directoryString);
    }
    #endregion

    #region Exercise Handling
    public void SaveExcercise (Exercise _excercise)
    {
        string directoryString = saveLocation + EXCERSISEFOLDER + "/" + _excercise.GetFileNameString() + ".txt";
        File.WriteAllLines(directoryString, _excercise.GetExcerciseContentAsString());
    }

    public void DeleteExcercise (Exercise _excercise)
    {
        string directoryString = saveLocation + EXCERSISEFOLDER + "/" + _excercise.GetFileNameString() + ".txt";
        File.Delete(directoryString);
    }

    public List<Exercise> LoadAllExcercise ()
    {
        List<Exercise> excerciseList = new List<Exercise>();
        string[] allExcercises = Directory.GetFiles(saveLocation + EXCERSISEFOLDER);

        for (int i = 0; i < allExcercises.Length; i++)
        {
            excerciseList.Add(LoadExcercise(allExcercises[i]));
        }

        return excerciseList;
    }

    public Exercise LoadExcercise (string _path)
    {
        Exercise excercise = new Exercise();
        string[] lines = File.ReadAllLines(_path);

        excercise.excerciseName = GetValueFromString(lines[0]);
        excercise.muscleGroup = (Exercise.MuscleGroup)System.Enum.Parse(typeof(Exercise.MuscleGroup), GetValueFromString(lines[1]));
        excercise.equipmentGroup = (Exercise.EquipmentGroup)System.Enum.Parse(typeof(Exercise.EquipmentGroup), GetValueFromString(lines[2]));
        excercise.setIsTimed = (GetValueFromString(lines[3]) == "True");
        excercise.setAmount = int.Parse(GetValueFromString(lines[4]));
        excercise.repetitionAmount = int.Parse(GetValueFromString(lines[5]));
        excercise.repDuration = int.Parse(GetValueFromString(lines[6]));
        excercise.breakDuration = int.Parse(GetValueFromString(lines[7]));
        excercise.totalTimesDone = int.Parse(GetValueFromString(lines[8]));
        excercise.totalTimesDoneToday = int.Parse(GetValueFromString(lines[9]));
        excercise.totalTimesDoneWeek = int.Parse(GetValueFromString(lines[10]));
        excercise.totalTimesDoneMonth = int.Parse(GetValueFromString(lines[11]));
        return excercise;
    }
    #endregion

    #region ProgressImage Handling
    public void SaveProgressImage (ProgressImage _progressImage)
    {
        Texture2D toSaveTexture = _progressImage.texture as Texture2D;
        byte[] saveBytes = toSaveTexture.EncodeToPNG();
        File.WriteAllBytes(saveLocation + IMAGESFOLDER + "/" + _progressImage.date.Replace('/','_') + ".png", saveBytes);
    }

    public void RemoveProgressImage(ProgressImage _progressImage)
    {
        File.Delete(saveLocation + IMAGESFOLDER + "/" + _progressImage.date.Replace('/', '_') + ".png");
    }

    public List<ProgressImage> GetAllProgressImages ()
    {
        List<ProgressImage> progressImages = new List<ProgressImage>();
        Debug.Log("Loading Images from: " + saveLocation + IMAGESFOLDER);
        string[] allImages = Directory.GetFiles(saveLocation + IMAGESFOLDER);
        for (int i = 0; i < allImages.Length; i++)
        {
            StartCoroutine(LoadProgressImage(allImages[i], progressImages));

        }

        return progressImages;
    }

    public IEnumerator LoadProgressImage (string _path, List<ProgressImage> _imagelist)
    {
        ProgressImage progressImage = new ProgressImage();
        Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);

        Debug.Log("file://" + _path);
        WWW www = new WWW("file://" + _path);
        yield return www;
        www.LoadImageIntoTexture(texture);

        string correctedURL = _path.Replace('\\', '/');
        int startOfName = correctedURL.LastIndexOf('/') + 1;
        int lenghtName = correctedURL.LastIndexOf('.') - startOfName;
        string photoName = correctedURL.Substring(startOfName, lenghtName);
        photoName = photoName.Replace('/', '_');
        progressImage.texture = texture;
        progressImage.date = photoName;
        _imagelist.Add(progressImage);
    }
    #endregion


    private string GetValueFromString (string _contentString)
    {
        int startIndex = _contentString.IndexOf("[");
        int endIndex = _contentString.IndexOf("]");
        return _contentString.Substring(startIndex + 1, endIndex - startIndex - 1);
    }

}
