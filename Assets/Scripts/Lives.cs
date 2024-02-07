using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public event EventHandler OnNoLivesRemaining;
    public int PlayerLives {get; private set;}

    private void Awake() {
        PlayerLives = 3;
    }
    private void Start() {
        GameHandler.Instance.OnProcessCardSelection += GameHandler_OnProcessCardSelection;
    }

    private void GameHandler_OnProcessCardSelection(object sender, GameHandler.OnProcessCardSelectionEventArgs e)
    {
        if (!e.isMatch) RemoveLife();
    }

    private void RemoveLife()
    {
        PlayerLives -= 1;
        if (PlayerLives <= 0){
            PlayerLives = 0;
            OnNoLivesRemaining?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy() {
        GameHandler.Instance.OnProcessCardSelection -= GameHandler_OnProcessCardSelection;
    }
}
