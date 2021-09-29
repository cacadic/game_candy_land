using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController instance;

    [SerializeField]
    private MusicController musicController;

    [SerializeField]
    private GameObject settingPanel;

    [SerializeField]
    private Animator settingsPanelAnim;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OpenSettingsPanel()
    {
        slider.value = musicController.GetMusicVolumn();
        settingPanel.SetActive(true);
        settingsPanelAnim.Play("SlideIn");
    }

    public void CloseSettingsPanel()
    {
        StartCoroutine(CloseSettings());
    }

    public bool IsSettingPanelActive()
    {
        return settingPanel.activeInHierarchy;
    }

    IEnumerator CloseSettings()
    {
        settingsPanelAnim.Play("SlideOut");
        yield return new WaitForSeconds(1f);
        settingPanel.SetActive(false);
    }

    public void GetVolume(float volumn)
    {
        musicController.SetMusicVolumn(volumn);
    }
}
