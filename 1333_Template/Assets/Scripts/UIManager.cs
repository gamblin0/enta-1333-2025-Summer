using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _transitionDuration;
    [SerializeField] private GameObject _settingsMenu;
    private RectTransform settingsRectTransform;

    private void Awake()
    {
        settingsRectTransform = _settingsMenu.gameObject.GetComponent<RectTransform>();
    }

    public void OpenSettings()
    {
        _settingsMenu.transform.position = new Vector2(-settingsRectTransform.rect.width - 200, _settingsMenu.transform.position.y);
        settingsRectTransform.DOLocalMoveX(0, _transitionDuration).SetEase(Ease.OutQuad);
    }

    public void CloseSettings()
    {
        settingsRectTransform.DOLocalMoveX(settingsRectTransform.rect.width + 200, _transitionDuration).SetEase(Ease.OutQuad);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("RTS_Demo");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
