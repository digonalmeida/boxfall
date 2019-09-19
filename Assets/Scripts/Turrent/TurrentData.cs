using UnityEngine;

[System.Serializable]
public class TurrentData
{
    [SerializeField]
    private float _shotSpeed = 17.0f;
    
    [SerializeField] 
    private Bullet _bulletPrefab;
    
    public float ShotSpeed => _shotSpeed;
    
    public Bullet BulletPrefab => _bulletPrefab;
}
