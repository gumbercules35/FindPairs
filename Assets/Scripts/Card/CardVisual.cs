using UnityEngine;

public class CardVisual : MonoBehaviour
{
    [SerializeField] Sprite cardBackSprite;
    [SerializeField] Sprite cardFrontSprite;
    private SpriteRenderer spriteRenderer;

    [SerializeField]private Card card;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardBackSprite;
    }

    private void Start() {
        card.OnCardFlip += Card_OnCardFlip;
    }

    private void Card_OnCardFlip(object sender, Card.OnCardFlipEventArgs e)
    {
        switch (e.isSelected){
            case true:
                spriteRenderer.sprite = cardFrontSprite;
                break;
            case false:
                spriteRenderer.sprite = cardBackSprite;
                break;
        }
    }

    private void OnDestroy() {
        card.OnCardFlip -= Card_OnCardFlip;
    }
}
