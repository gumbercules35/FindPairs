using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    public event EventHandler<OnCardFlipEventArgs> OnCardFlip;
    public class OnCardFlipEventArgs : EventArgs {
        public bool isSelected;
    }
    [SerializeField]private CardType cardType;
    private bool isSelected;

    public CardType GetCardType () {
        return cardType;
    }

    public void Flip(){
        isSelected = !isSelected;
        OnCardFlip?.Invoke(this, new OnCardFlipEventArgs{isSelected = isSelected});
    }

    
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
