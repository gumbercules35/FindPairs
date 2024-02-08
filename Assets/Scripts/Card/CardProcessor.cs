using System.Collections.Generic;
using UnityEngine;

public class CardProcessor : MonoBehaviour
{
    public bool CheckSelectedCardsForMatch(Card firstCard, Card secondCard){
        return firstCard.GetCardType() == secondCard.GetCardType();
    }

    public void ProcessMatchingCards (List<Card> _selectedCards){
        
        foreach (Card card in _selectedCards)
            {
                card.DestroySelf();
            }
    }

    public void ProcessFailedCards(List<Card> _selectedCards){

        foreach (Card card in _selectedCards)
            {
                card.Flip();
            }
    }
    
    public void FlipSelectedCard(Card card){
        card.Flip();
    }
}
