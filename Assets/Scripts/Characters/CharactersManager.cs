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

    private CharactersMovement _characterMovement;

    public int CharactersAmount { private set; get; }

    public PlayerAddedEvent OnPlayerAdded;

    public void SubscribeToCharacterCreation(JoinState joinState)
    {
        _joinState = joinState;

        _joinState.OnPlayerAdded += InstantiatePlayer;
    }

    public void UnsubscribeFromCharacterCreation()
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

    public void SubscribeToCharacterMovement(PlayState playState)
    {
        _characterMovement = new CharactersMovement(_characters, playState);
    }

    public void UnsubscribeFromCharacterMovement()
    {
        _characterMovement = null;
    }
}

public class CharactersMovement
{
    private readonly Dictionary<int, Character> _characters;
    private PlayState _playState;

    public CharactersMovement(Dictionary<int, Character> characters, PlayState playState)
    {
        _characters = characters;
        _playState = playState;
        _playState.OnPlayerButton += OnPlayerButton;
    }

    ~CharactersMovement()
    {
        _playState.OnPlayerButton -= OnPlayerButton;
    }

    private void OnPlayerButton(int playerId, int button)
    {
        if (!_characters.ContainsKey(playerId)) return;

        _characters[playerId].AddForce(button);
    }
}

[Serializable]
public class PlayerAddedEvent : UnityEvent<int>
{

}