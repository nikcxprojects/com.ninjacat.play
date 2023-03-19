using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Data", order = 51)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _attackPrefab;

    public Sprite Sprite => _sprite;
    public GameObject AttackPrefab => _attackPrefab;

    public void SetNewData(CharacterData data)
    {
        _sprite = data._sprite;
        _attackPrefab = data._attackPrefab;
    }
}