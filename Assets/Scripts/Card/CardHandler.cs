
using UnityEngine;

[RequireComponent(typeof(CardProcessor), typeof(CardSelector), typeof(CardSpawner))]
public class CardHandler : MonoBehaviour
{
    //This Class should coordinate card handling NO BUSINESS LOGIC JUST DATA PASSING
    [SerializeField] private CardSelector _cardSelector;
    [SerializeField] private CardProcessor _cardProcessor;
    [SerializeField] private CardSpawner _cardSpawner;
    private void OnEnable() {
        _cardSelector.OnCardSelected += CardSelector_OnCardSelected;
    }
    private void OnDisable() {
        _cardSelector.OnCardSelected -= CardSelector_OnCardSelected;
    }

    private void CardSelector_OnCardSelected(object sender, CardSelector.OnCardSelectedEventArgs e)
    {
        _cardProcessor.FlipSelectedCard(e.selectedCard);
    }

    public void ResetSelection(){
        _cardSelector.ResetSelection();
    }

    public void AddCardToSelection(Card _card) {
        _cardSelector.AddCardToSelection(_card);
    }

    public bool IsSelectionComplete(){
        return _cardSelector.IsSelectionComplete();
    }

    public bool CheckSelectedCardsForMatch(){
        Card firstSelection = _cardSelector.GetFirstCard();
        Card secondSelection = _cardSelector.GetSecondCard();
        return _cardProcessor.CheckSelectedCardsForMatch(firstSelection,secondSelection);
    }

    public void ProcessMatchingCards (){
        _cardProcessor.ProcessMatchingCards(_cardSelector.SelectedCards);
    }

    public void ProcessFailedCards(){
        _cardProcessor.ProcessFailedCards(_cardSelector.SelectedCards);
    }

    public void SpawnCards() {
        _cardSpawner.Spawn();
    }
}
