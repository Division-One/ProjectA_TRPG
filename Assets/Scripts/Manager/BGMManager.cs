using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGMManager : MonoBehaviour
{
    #region singletone
    private static BGMManager instance = null;
    public static BGMManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else
            Destroy(this.gameObject);
    }
    #endregion singletone

    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update

    public void SetVolume(float f)
    {
        if(audioSource != null)
            audioSource.volume = f;
    }
    public void IncreaseVolume(float f)
    {
        SetVolume(audioSource.volume + f);
    }
    public void DecreaseVolume(float f)
    {
        SetVolume(audioSource.volume - f);
    }
}
