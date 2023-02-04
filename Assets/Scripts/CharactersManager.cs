using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] private Transform _charactersContainer;
    [SerializeField] private GameObject _characterPrefab;
    
    private List<GameObject> characters = new List<GameObject>();
    private JoinState _joinState;

    public void SubscribeToPlayerCreation(JoinState joinState)
    {
        _joinState = joinState;

        _joinState._onPlayerAdded += InstantiatePlayer;
    }

    public void UnsubscribeFromPlayerCreation()
    {
        _joinState._onPlayerAdded -= InstantiatePlayer;

        _joinState = null;
    }

    public void ResetPlayers()
    {
        //todo: destroy everything
        characters.Clear();
    }
    
    public void InstantiatePlayer(int number)
    {
        var newCharacter = GameObject.Instantiate(_characterPrefab, _charactersContainer);
        characters.Add(newCharacter);
    }
}