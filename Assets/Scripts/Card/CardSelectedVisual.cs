using System;
using UnityEngine;

public class CardSelectedVisual : MonoBehaviour
{
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _correctSprite;
    [SerializeField] private Sprite _incorrectSprite;
    [SerializeField] private Card _card;

    [Header("Event Channels")]
    [SerializeField] private ProcessCard_ChannelSO _processCard_ChannelSO;
    [SerializeField] private CardHighlightEvent_ChannelSO _cardHighlightEvent_ChannelSO;
    private SpriteRenderer _spriteRenderer;
    private bool _isSelected;


    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _selectedSprite;
        
    }

    private void Start() {
        _card.OnCardFlip += Card_OnCardFlip;
        _cardHighlightEvent_ChannelSO.OnMouseHover += CardHighlightEvent_OnMouseHover;
        _processCard_ChannelSO.OnProcessCardSelection += GameHandler_OnProcessCardSelection;
        Hide();
    }

    private void CardHighlightEvent_OnMouseHover(object sender, CardHighlightEvent_ChannelSO.CardHighlightEventArgs e)
    {
        if (e.currentCard == this && !_isSelected){
            _spriteRenderer.sprite = _selectedSprite;
            Show();
        } else if (!_isSelected) {
            Hide();
        }
    }

    private void GameHandler_OnProcessCardSelection(object sender, ProcessCard_ChannelSO.OnProcessCardSelectionEventArgs e)
    {
        if(e.isMatch){
            _spriteRenderer.sprite = _correctSprite;
        } else {
            _spriteRenderer.sprite = _incorrectSprite;
        }
    }

    private void Card_OnCardFlip(object sender, Card.OnCardFlipEventArgs e)
    {
        if (e.isSelected){
            _spriteRenderer.sprite = _selectedSprite;
            _isSelected = true;
            Show();
        } else {
            _isSelected = false;
            Hide();
        }
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    private void Show(){
        gameObject.SetActive(true);
    }
    private void OnDestroy() {
        _processCard_ChannelSO.OnProcessCardSelection -= GameHandler_OnProcessCardSelection;
        _cardHighlightEvent_ChannelSO.OnMouseHover += CardHighlightEvent_OnMouseHover;
    }
}
