using System;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    [Serializable]
    public class Character
    {
        public Sprite Sprite;
        public ScriptableObject Scriptable;
    }
    
    [SerializeField] private List<Character> _characters;
}