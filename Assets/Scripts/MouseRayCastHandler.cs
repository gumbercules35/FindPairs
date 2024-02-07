using UnityEngine;

public class MouseRayCastHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start() {
        _mainCamera = Camera.main;
    }

    public bool TryGetCardFromRayCastHit(RaycastHit raycastHit, out Card card){
        return raycastHit.transform.gameObject.TryGetComponent<Card>(out card);
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
