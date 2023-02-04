using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] private Transform _charactersContainer;
    [SerializeField] private Character _characterPrefab;
    
    public Dictionary<int, Character> Characters { private set; get; } = new Dictionary<int, Character>();
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
        foreach (var item in Characters.ToArray<KeyValuePair<int, Character>>())
        {
            RemoveCharacter(item.Key);
        }
    }
    
    public void InstantiatePlayer(int number)
    {
        Debug.Log($"Creating character {number}", gameObject);
        var newCharacter = Instantiate(_characterPrefab, _charactersContainer);
        newCharacter.transform.localPosition += new Vector3(UnityEngine.Random.Range(-10, 10), 0f, UnityEngine.Random.Range(-10, 10));
        newCharacter.AssignId(number);
        newCharacter.OnCharacterTouchedWater.AddListener(OnCharacterTouchedWater);
        Characters.Add(number, newCharacter);
        CharactersAmount++;

        OnPlayerAdded?.Invoke(CharactersAmount);
    }

    private void OnCharacterTouchedWater(int characterId)
    {
        RemoveCharacter(characterId);
    }

    public void RemoveCharacter(int characterId)
    {
        var character = Characters[characterId];
        Destroy(character.gameObject);

        Characters.Remove(characterId);
        CharactersAmount--;
        Debug.Log($"Removed character {characterId}");
    }

    public void SubscribeToCharacterMovement(PlayState playState)
    {
        _characterMovement = new CharactersMovement(Characters, playState);
    }

    public void UnsubscribeFromCharacterMovement()
    {
        _characterMovement = null;
    }
}

[Serializable]
public class PlayerAddedEvent : UnityEvent<int>
{

}