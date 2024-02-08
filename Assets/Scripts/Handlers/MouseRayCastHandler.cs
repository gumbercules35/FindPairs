using UnityEngine;

public class MouseRayCastHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private CardSelectedVisual _currentCardSelectedVisual;

    [Header("Event Channels")]
    [SerializeField] private CardHighlightEvent_ChannelSO _cardHighlightEvent_ChannelSO;
    private void Start() {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        ShowOutlineOnMouseHover();
    }

    private void ShowOutlineOnMouseHover()
    {
        if (RayCastMouse(out RaycastHit raycastHit))
        {
            if (TryGetCardSelectedVisualFromRayCastHit(raycastHit, out CardSelectedVisual newCardSelectedVisual))
            {
                if (_currentCardSelectedVisual != newCardSelectedVisual)
                {
                    _currentCardSelectedVisual = newCardSelectedVisual;
                    _cardHighlightEvent_ChannelSO.RaiseEvent(this, _currentCardSelectedVisual);
                }

            }
            else
            {
                _currentCardSelectedVisual = null;
                _cardHighlightEvent_ChannelSO.RaiseEvent(this, _currentCardSelectedVisual);
            }

        }
    }

    public bool TryGetCardFromRayCastHit(RaycastHit raycastHit, out Card card){
        return raycastHit.transform.gameObject.TryGetComponent<Card>(out card);
    }
    public bool TryGetCardSelectedVisualFromRayCastHit(RaycastHit raycastHit, out CardSelectedVisual cardSelectedVisual){
        cardSelectedVisual = null;
        foreach (Transform childTransform in raycastHit.transform)
        {
            childTransform.TryGetComponent<CardSelectedVisual>(out cardSelectedVisual);
        }
        if(cardSelectedVisual){
            return true;
        }
        return false;
    }

    public bool RayCastMouse (out RaycastHit raycastHit){

        return Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out raycastHit);
    }

    public bool TrySelectCard(out Card card){
        if (RayCastMouse(out RaycastHit _rayCastHit)){
            if (TryGetCardFromRayCastHit(_rayCastHit, out card)){
                return true;
            }
        }

        card = null;
        return false;
    }


}
