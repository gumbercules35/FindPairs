using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Highlight Channel SO")]
public class CardHighlightEvent_ChannelSO : ScriptableObject
{
    public event EventHandler<CardHighlightEventArgs> OnMouseHover;

    public class CardHighlightEventArgs : EventArgs {
        public CardSelectedVisual currentCard;
    }

    public void RaiseEvent(object sender, CardSelectedVisual cardSelectedVisual){
        OnMouseHover?.Invoke(this, new CardHighlightEventArgs { currentCard = cardSelectedVisual});
    }
}
