using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardDialog : MonoBehaviour
{
    [HeaderAttribute("Header")]
    [SerializeField]
    private Text _headerText;

    [HeaderAttribute("Content")]
    [SerializeField]
    private Text _contentText;

    [SerializeField] private RawImage _contentImage;
    [SerializeField] private Button _contentLeftButton;
    [SerializeField] private Button _contentRightButton;

    public Action OnContentLeftButtonClickAction { get; private set; }
    public Action OnContentRightButtonClickAction { get; private set; }

    public void SetHeaderText(string text)
    {
        _headerText.text = text;
    }

    public void SetContentText(string text)
    {
        _contentText.text = text;
    }


    public void SetContentImage(Sprite sprite)
    {
        _contentImage.texture = sprite.texture;
    }

    public void SetContentImage(string url)
    {
        StartCoroutine(SetContentImageCoroutine(url));
    }

    public void SetContentLeftButtonLabel(string labelText)
    {
        _contentLeftButton.GetComponentInChildren<Text>().text = labelText;
    }

    public void SetContentRightButtonLabel(string labelText)
    {
        _contentRightButton.GetComponentInChildren<Text>().text = labelText;
    }

    public void SetOnContentLeftButtonClickAction(Action onClickAction)
    {
        OnContentLeftButtonClickAction = onClickAction;
    }

    public void SetOnContentRightButtonClickAction(Action onClickAction)
    {
        OnContentRightButtonClickAction = onClickAction;
    }

    private void Awake()
    {
        OnContentLeftButtonClickAction = () => { };
        OnContentRightButtonClickAction = () => { };
    }

    private void Start()
    {
        _contentLeftButton.onClick.AddListener(OnLeftClick);
        _contentRightButton.onClick.AddListener(OnRightClick);
    }

    private void OnLeftClick()
    {
        OnContentLeftButtonClickAction?.Invoke();
    }

    private void OnRightClick()
    {
        OnContentRightButtonClickAction?.Invoke();
    }

    private IEnumerator SetContentImageCoroutine(string url)
    {
        var uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            throw new Exception();
        }
        else
        {
            _contentImage.texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
        }
    }
}
