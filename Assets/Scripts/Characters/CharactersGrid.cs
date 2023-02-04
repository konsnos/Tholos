using System;
using NaughtyAttributes;
using UnityEngine;

public class CharactersGrid : MonoBehaviour
{
    [ShowNonSerializedField] private float size;
    [SerializeField] private float maxSize;
    public float CharactersScale { private set; get; }

    public Vector3[] GetGridForAmount(int amount)
    {
        int gridSize = Mathf.FloorToInt(Mathf.Sqrt(amount));
        size = 1f / ((float)amount / CharactersManager.MaxCharactersAmount);
        Mathf.Min(size, maxSize);
        
        //todo: calculate positions

        return Array.Empty<Vector3>();
    } 
}
