using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDataController : MonoBehaviour
{
    [SerializeField] private List<CharacterData> _list;
    [SerializeField] private CharacterData _current;
    [SerializeField] private Image _character;

    private void Start()
    {
        if(_current.Sprite == null) SetData(0);
        _character.sprite = _current.Sprite;
    }

    private void SetData(int id)
    {
        if (id >= _list.Count) return;
        _current.SetNewData(_list[id]);
        _character.sprite = _current.Sprite;
    }
    
    public void NextCharacter()
    {
        var cId = PlayerPrefs.GetInt("Character", 0);
        if (cId >= _list.Count-1) cId = 0;
        else cId++;
        SetData(cId);
        PlayerPrefs.SetInt("Character", cId);
    }
}
