using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public event EventHandler<OnCardSelectedEventArgs> OnCardSelected;
    public class OnCardSelectedEventArgs : EventArgs {
        public Card selectedCard;
    }
    private List<Card> _selectedCards;
    public List<Card> SelectedCards => _selectedCards;
    private void Awake() {
        _selectedCards = new List<Card>();
    }

     public void AddCardToSelection(Card clickedCard) {
        if (IsSelectionEmpty()){
            _selectedCards.Add(clickedCard);
            OnCardSelected?.Invoke(this, new OnCardSelectedEventArgs{ selectedCard = clickedCard});
            return;
        } else {
            if (_selectedCards.Contains(clickedCard)){
                return;
            } else if (_selectedCards.Count < 2){
                _selectedCards.Add(clickedCard);
                OnCardSelected?.Invoke(this, new OnCardSelectedEventArgs{ selectedCard = clickedCard});
                        
                return;
            }
        }
    }

    public int GetSelectionCount(){
        return _selectedCards.Count;
    }
    public bool IsSelectionEmpty () {
        return _selectedCards.Count == 0;
    }

    public bool IsSelectionComplete () {
        return _selectedCards.Count == 2;
    }
    
    public Card GetFirstCard() {
        return _selectedCards[0];
    }
    public Card GetSecondCard() {
        return _selectedCards[1];
    }

    public void ResetSelection() {
        _selectedCards.Clear();
    }

}
