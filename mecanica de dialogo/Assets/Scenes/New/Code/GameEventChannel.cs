using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameEventChannel")]
public class GameEventChannel : ScriptableObject
{
    public event Action OnCoinCollected;
    public event Action OnCheckpointReached;
    public event Action OnVictory;
    public event Action OnTogglePause;

    public void RaiseCoinCollected() => OnCoinCollected?.Invoke();
    public void RaiseCheckpointReached() => OnCheckpointReached?.Invoke();
    public void RaiseVictory() => OnVictory?.Invoke();
    public void RaiseTogglePause() => OnTogglePause?.Invoke();
}