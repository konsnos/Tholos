using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] private Transform _charactersContainer;
    [SerializeField] private Character _characterPrefab;
    
    private Dictionary<int, Character> _characters = new Dictionary<int, Character>();
    public const int MaxCharactersAmount = 130;
    private JoinState _joinState;

    public int CharactersAmount { private set; get; }

    public PlayerAddedEvent OnPlayerAdded;

    public void SubscribeToPlayerCreation(JoinState joinState)
    {
        _joinState = joinState;

        _joinState.OnPlayerAdded += InstantiatePlayer;
    }

    public void UnsubscribeFromPlayerCreation()
    {
        _joinState.OnPlayerAdded -= InstantiatePlayer;

        _joinState = null;
    }

    public void ResetPlayers()
    {
        //todo: destroy everything
        _characters.Clear();
        CharactersAmount = 0;
    }
    
    public void InstantiatePlayer(int number)
    {
        Debug.Log($"Creating character {number}", gameObject);
        var newCharacter = Instantiate(_characterPrefab, _charactersContainer);
        newCharacter.CharacterId = number;
        _characters.Add(number, newCharacter);
        CharactersAmount++;

        OnPlayerAdded?.Invoke(CharactersAmount);
    }
}

[Serializable]
public class PlayerAddedEvent : UnityEvent<int>
{

}