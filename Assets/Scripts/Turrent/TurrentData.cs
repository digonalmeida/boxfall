using UnityEngine;

[System.Serializable]
public class TurrentData
{
    [SerializeField]
    private float _shotSpeed = 17.0f;
    public float ShotSpeed => _shotSpeed;
}
