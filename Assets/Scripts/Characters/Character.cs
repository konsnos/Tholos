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

        _rigidbody.AddForce(force);
    }

    protected Vector3 GetForcePerDirectcion(int channel)
    {
        Vector3 force = new Vector3();

        if (channel == (int)ForceDirectionPerChannel.TopLeft)
        {
            force.x = -1 * _forceModifier;
            force.z = 1 * _forceModifier;
            return force;
        }
        if (channel == (int)ForceDirectionPerChannel.TopRight)
        {
            force.x = 1 * _forceModifier;
            force.z = 1 * _forceModifier;
            return force;
        }
        if (channel == (int) ForceDirectionPerChannel.BottomLeft)
        {
            force.x = -1 * _forceModifier;
            force.z = -1 * _forceModifier;
            return force;
        }
        if(channel == (int) ForceDirectionPerChannel.BottomRight)
        {
            force.x = -1 * _forceModifier;
            force.z = -1 * _forceModifier;
            return force;
        }

        return Vector3.zero;
    }
}

public enum ForceDirectionPerChannel
{
    TopLeft = 1,
    TopRight = 2,
    BottomLeft = 3,
    BottomRight = 4,
}