using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ProgressUIItem : MonoBehaviour {

    public delegate void OnProgressUIItemClickedEvent (ProgressUIItem _uiItem);
    public event OnProgressUIItemClickedEvent onRemoveClicked = delegate
    { };

    public ProgressImage progressImage;
    [SerializeField]
    private Button removeButton,zoomButton,downloadButton;
    [SerializeField]
    private RawImage imageOutput;
    [SerializeField]
    private Text dateText;

    void Awake ()
    {
        removeButton.onClick.AddListener(OnRemoveClicked);
        zoomButton.onClick.AddListener(OnZoomClicked);
        downloadButton.onClick.AddListener(OnDownloadClicked);
    }

    private void OnRemoveClicked ()
    {
        onRemoveClicked(this);
    }

    private void OnDownloadClicked ()
    {
        string path = SaveImageToGallery(imageOutput.texture as Texture2D, "Test Picture", "This is a description.");
        Debug.Log(path);
        using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
        using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
        {
            jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] { path }, null, null);
        }
    }

    private void OnZoomClicked ()
    {
        ProgressImageGallery.instance.SetImage(imageOutput.texture);
        ProgressImageGallery.instance.Show();
    }

    public void UpdateUI ()
    {
        imageOutput.texture = progressImage.texture;
        dateText.text = progressImage.date;
    }

    protected const string MEDIA_STORE_IMAGE_MEDIA = "android.provider.MediaStore$Images$Media";
    protected static AndroidJavaObject m_Activity;

    protected static string SaveImageToGallery (Texture2D a_Texture, string a_Title, string a_Description)
    {
        using (AndroidJavaClass mediaClass = new AndroidJavaClass(MEDIA_STORE_IMAGE_MEDIA))
        {
            using (AndroidJavaObject contentResolver = Activity.Call<AndroidJavaObject>("getContentResolver"))
            {
                AndroidJavaObject image = Texture2DToAndroidBitmap(a_Texture);
                return mediaClass.CallStatic<string>("insertImage", contentResolver, image, a_Title, a_Description);
            }
        }
    }

    protected static AndroidJavaObject Texture2DToAndroidBitmap (Texture2D a_Texture)
    {
        byte[] encodedTexture = a_Texture.EncodeToPNG();
        using (AndroidJavaClass bitmapFactory = new AndroidJavaClass("android.graphics.BitmapFactory"))
        {
            return bitmapFactory.CallStatic<AndroidJavaObject>("decodeByteArray", encodedTexture, 0, encodedTexture.Length);
        }
    }

    protected static AndroidJavaObject Activity
    {
        get
        {
            if (m_Activity == null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return m_Activity;
        }
    }
}
