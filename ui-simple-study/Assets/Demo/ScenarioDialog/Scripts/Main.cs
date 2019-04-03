using UnityEngine;
using Newtonsoft.Json;
using MTG;
using UnityEditor;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject _scenarioDialogPrefab;
    [SerializeField]
    private Sprite _loadingSprite;
    [SerializeField]
    Language _language = Language.ENGLISH;

    private GameObject _cardDialogGameObject;
    private CardDialog _cardDialogScript;
    private MTGData _mtgData;
    [SerializeField]
    private int _nowCard = 0;

    public enum Language
    {
        GERMAN = 0,
        SPANISH = 1,
        FRENCH = 2,
        ITALIAN = 3,
        JAPANESE = 4,
        KOREAN = 5,
        PORTUGUESE = 6,
        RUSSIAN = 7,
        CHINESE_SIMPLIFIED = 8,
        CHINESE_TRADITIONAL = 9,
        ENGLISH = 10,
    }

    void Awake()
    {
        _mtgData = GetMTGData();
    }

    void Start()
    {
        _cardDialogGameObject = Instantiate(_scenarioDialogPrefab);
        _cardDialogScript = _cardDialogGameObject.GetComponent<CardDialog>();
        _cardDialogScript.SetContentLeftButtonLabel("< Back");
        _cardDialogScript.SetContentRightButtonLabel("Next >");
        _cardDialogScript.SetOnContentLeftButtonClickAction(BackCard);
        _cardDialogScript.SetOnContentRightButtonClickAction(NextCard);

        ResetCardData();

        SetCardData(_mtgData.cards[_nowCard]);
    }

    private void BackCard()
    {
        if (_nowCard <= 0)
        {
            _nowCard = 0;
            return;
        }

        ResetCardData();

        _nowCard--;
        SetCardData(_mtgData.cards[_nowCard]);
    }
    
    private void NextCard()
    {
        if (_nowCard >= _mtgData.cards.Length - 1)
        {
            _nowCard = _mtgData.cards.Length-1;
            return;
        }

        ResetCardData();

        _nowCard++;
        SetCardData(_mtgData.cards[_nowCard]);
    }

    private MTGData GetMTGData()
    {
        var cardsJson = Resources.Load<TextAsset>("CardData").ToString();
        return JsonConvert.DeserializeObject<MTGData>(cardsJson);
    }

    private void SetCardData(Card card)
    {
        var name = string.Empty;
        var text = string.Empty;
        var imageUrl = string.Empty;
        if (_language == Language.ENGLISH || card.foreignNames.Length <= 0)
        {
            if (!string.IsNullOrEmpty(card.name))
                name = card.name;
            if (!string.IsNullOrEmpty(card.text))
                text = card.text.Replace("\n", "\r\n");
            if (!string.IsNullOrEmpty(card.imageUrl))
                imageUrl = card.imageUrl;
        }
        else
        {
            var languageIndexNum = (int) _language;

            if (!string.IsNullOrEmpty(card.foreignNames[languageIndexNum].name))
                name = card.foreignNames[languageIndexNum].name;
            if(!string.IsNullOrEmpty(card.foreignNames[languageIndexNum].text))
                text = card.foreignNames[languageIndexNum].text.Replace("\n", "\r\n");
            if (!string.IsNullOrEmpty(card.foreignNames[languageIndexNum].imageUrl))
                imageUrl = card.foreignNames[languageIndexNum].imageUrl;
        }

        if (!string.IsNullOrEmpty(name))
            _cardDialogScript.SetHeaderText(name);
        if (!string.IsNullOrEmpty(text))
            _cardDialogScript.SetContentText(text);
        if(!string.IsNullOrEmpty(imageUrl))
            _cardDialogScript.SetContentImage(imageUrl);
    }

    private void ResetCardData()
    {
        _cardDialogScript.SetHeaderText(string.Empty);
        _cardDialogScript.SetContentText(string.Empty);
        _cardDialogScript.SetContentImage(_loadingSprite);
    }
}
