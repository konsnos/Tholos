using System.Collections.Generic;

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
