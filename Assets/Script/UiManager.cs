using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private RectTransform settingPanelTransform;

    public void OpenSettingPanel()
    {
        settingPanelTransform.SetAsLastSibling();
        settingPanelTransform.gameObject.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        settingPanelTransform.gameObject.SetActive(false);
    }

}
