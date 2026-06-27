using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RTSPause : MonoBehaviour
{
    [SerializeField] private float _openPosition;
    [SerializeField] private float _closedPosition;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] Button _pauseButton;
    private RectTransform rectTransform;
    private RectTransform buttonRectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonRectTransform = _pauseButton.gameObject.GetComponent<RectTransform>();
        _closedPosition = -rectTransform.rect.width;
        _openPosition = 0;
    }

    public void Open()
    {
        rectTransform.DOAnchorPosX(_openPosition, _transitionDuration);
        buttonRectTransform.DOAnchorPosY(buttonRectTransform.rect.height, _transitionDuration);
    }

    public void Close()
    {
        rectTransform.DOAnchorPosX(_closedPosition, _transitionDuration);
        buttonRectTransform.DOAnchorPosY(-buttonRectTransform.rect.height, _transitionDuration);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
