using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetupManager: MonoBehaviour
{
    [SerializeField]
    SeparatedVolume bgmVolumeControl;
    [SerializeField]
    SeparatedVolume effectVolumeControl;
    [SerializeField]
    SeparatedVolume textSpeedControl;
    [SerializeField]
    OnOffDualTogglelControl vibrationControl;
    [SerializeField]
    OnOffDualTogglelControl darkModeControl;
    [SerializeField]
    TextMeshProUGUI uid;
    [SerializeField]
    TextMeshProUGUI lastRecord;
    [SerializeField]
    TextButton dataStore;
    [SerializeField]
    TextButton dataLoad;
    [SerializeField]
    TextButton dataReset;
    [SerializeField]
    TextButton logOut;
    [SerializeField]
    TextButton contactUs;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        BGMManager.Instance.SetVolume(bgmVolumeControl.defaultFill);
        bgmVolumeControl.AddDecreaseListener(delegate { BGMManager.Instance.DecreaseVolume(bgmVolumeControl.threshold); });
        bgmVolumeControl.AddIncreaseListener(delegate { BGMManager.Instance.IncreaseVolume(bgmVolumeControl.threshold); });

    }
    public void CloseButton()
    {
        LobbyManager.Instance.SwitchCanvas(LobbyManager.LobbyCanvasType.Lobby);
    }
}
