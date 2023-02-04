using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public int CharacterId { get; private set; }

    private Rigidbody _rigidbody;

    [SerializeField] private float _forceModifier;
    [SerializeField] private TMP_Text characterNameText;

    public CharacterTouchedWaterEvent OnCharacterTouchedWater;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AssignId(int newId)
    {
        CharacterId = newId;
        characterNameText.text = $"√{CharacterId}";
    }

    public void AddForce(int channel)
    {
        Vector3 force = GetForcePerDirection(channel);

        //Debug.Log($"Adding force {force} to character {CharacterId}", gameObject);

        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    protected Vector3 GetForcePerDirection(int channel)
    {
        Vector3 force = new Vector3();

        Debug.Log($"Received channel {channel}");

        if (channel == (int)ForceDirectionPerChannel.ForwardLeft)
        {
            force.x = -1 * _forceModifier;
            force.z = 1 * _forceModifier;
            return force;
        }
        if (channel == (int)ForceDirectionPerChannel.ForwardRight)
        {
            force.x = 1 * _forceModifier;
            force.z = 1 * _forceModifier;
            return force;
        }
        if (channel == (int) ForceDirectionPerChannel.BackwardLeft)
        {
            force.x = -1 * _forceModifier;
            force.z = -1 * _forceModifier;
            return force;
        }
        if(channel == (int) ForceDirectionPerChannel.BackwardRight)
        {
            force.x = 1 * _forceModifier;
            force.z = -1 * _forceModifier;
            return force;
        }

        return Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log($"Character {CharacterId} Collided with water", gameObject);
            //OnCharacterTouchedWater?.Invoke(CharacterId);
        }
    }
}

[Serializable]
public class CharacterTouchedWaterEvent : UnityEvent<int>
{ }

public enum ForceDirectionPerChannel
{
    ForwardLeft = 0, // red
    ForwardRight = 1, // green
    BackwardLeft = 2, // white
    BackwardRight = 3, // yellow
}