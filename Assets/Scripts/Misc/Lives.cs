using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] private ProcessCard_ChannelSO processCard_ChannelSO;
    public event EventHandler OnNoLivesRemaining;
    public int PlayerLives {get; private set;}

    private void Awake() {
        PlayerLives = 3;
        
    }
    private void Start() {
        processCard_ChannelSO.OnProcessCardSelection += GameHandler_OnProcessCardSelection;
    }

    private void GameHandler_OnProcessCardSelection(object sender, ProcessCard_ChannelSO.OnProcessCardSelectionEventArgs e)
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
        processCard_ChannelSO.OnProcessCardSelection -= GameHandler_OnProcessCardSelection;
    }
}