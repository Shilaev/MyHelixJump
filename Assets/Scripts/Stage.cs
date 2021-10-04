using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Platform
{
    //Platform can contain from 1 to 11 blocks
    //and Platforn can contain grom 0 to 11 death blocks
    [Range(1f, 11f)] public int blockCount = 11;
    [Range(0f, 11f)] public int deathBlockCount = 1;
}

[CreateAssetMenu(fileName = "New Stage")]
public class Stage : ScriptableObject
{
    public Color ballColor = Color.white;
    public Color backgroundColor = Color.white;
    public Color platformPartColor = Color.white;
    public Color goalPlatformColor = Color.red;

    public List<Platform> platforms = new List<Platform>();
}