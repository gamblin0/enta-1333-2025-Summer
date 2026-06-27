using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlidePanelScript : MonoBehaviour
{

    [SerializeField] private float _openPosition;
    [SerializeField] private float _closePosition;
    [SerializeField] private float _transitionDuration = 0.2f;
    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _closePosition = -rectTransform.rect.width;
        _openPosition = 0;
    }
    public void Open()
    {
        rectTransform.DOAnchorPosX(_openPosition, _transitionDuration).SetEase(Ease.OutBounce).OnComplete(() => { GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f).SetEase(Ease.InQuad);
        
        } );
        GetComponentInChildren<Button>().GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.InQuad);
    }


    public void Close()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        rectTransform.DOAnchorPosX(_closePosition, _transitionDuration).SetEase(Ease.InQuad).OnComplete(() => { GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -160); }); ;
    }




}
