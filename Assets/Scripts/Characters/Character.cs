using UnityEngine;

public class Character : MonoBehaviour
{
    public int CharacterId { get; set; }

    private Rigidbody _rigidbody;

    [SerializeField] private float _forceModifier;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AddForce(int channel)
    {
        Vector3 force = GetForcePerDirectcion(channel);

        Debug.Log($"Adding force {force} to character {CharacterId}", gameObject);

        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    protected Vector3 GetForcePerDirectcion(int channel)
    {
        Vector3 force = new Vector3();

        Debug.Log($"Received channel {channel}");

        if (channel == (int)ForceDirectionPerChannel.ForwardLeft)
        {
            Debug.Log("Forward Left");
            force.x = -1 * _forceModifier;
            force.z = 1 * _forceModifier;
            return force;
        }
        if (channel == (int)ForceDirectionPerChannel.ForwardRight)
        {
            Debug.Log("Forward Right");
            force.x = 1 * _forceModifier;
            force.z = 1 * _forceModifier;
            return force;
        }
        if (channel == (int) ForceDirectionPerChannel.BackwardLeft)
        {
            Debug.Log("Backward Left");
            force.x = -1 * _forceModifier;
            force.z = -1 * _forceModifier;
            return force;
        }
        if(channel == (int) ForceDirectionPerChannel.BackwardRight)
        {
            Debug.Log("Backward Right");
            force.x = 1 * _forceModifier;
            force.z = -1 * _forceModifier;
            return force;
        }

        return Vector3.zero;
    }
}

public enum ForceDirectionPerChannel
{
    ForwardLeft = 0,
    ForwardRight = 1,
    BackwardLeft = 2,
    BackwardRight = 3,
}