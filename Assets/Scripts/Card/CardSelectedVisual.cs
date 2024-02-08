using UnityEngine;

public class CardSelectedVisual : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite incorrectSprite;
    [SerializeField] private Card card;
    [SerializeField] private ProcessCard_ChannelSO processCard_ChannelSO;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = selectedSprite;
        
    }

    private void Start() {
        card.OnCardFlip += Card_OnCardFlip;
        processCard_ChannelSO.OnProcessCardSelection += GameHandler_OnProcessCardSelection;
        Hide();
    }

    private void GameHandler_OnProcessCardSelection(object sender, ProcessCard_ChannelSO.OnProcessCardSelectionEventArgs e)
    {
        if(e.isMatch){
            spriteRenderer.sprite = correctSprite;
        } else {
            spriteRenderer.sprite = incorrectSprite;
        }
    }

    private void Card_OnCardFlip(object sender, Card.OnCardFlipEventArgs e)
    {
        if (e.isSelected){
            spriteRenderer.sprite = selectedSprite;
            Show();
        } else {
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
        processCard_ChannelSO.OnProcessCardSelection -= GameHandler_OnProcessCardSelection;
    }
}
