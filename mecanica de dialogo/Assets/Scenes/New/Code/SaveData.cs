using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int currentLevelIndex;
    public bool reachedCheckpoint;
    public int coinsAtCheckpoint;
    public List<string> collectedCoinIDsAtCheckpoint = new List<string>();
}