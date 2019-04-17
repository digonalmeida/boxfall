using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Shooting,
    ComeBack
}

public class PlayerCharacterEntity : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _shotSpeed = 10;

    [SerializeField]
    private float _recoilForce = 3;

    [SerializeField]
    private float _walkingSpeed = 3;

    [SerializeField]
    private PlayerState _state;



    public delegate void StateChangeDelegate(PlayerState newState);
    public event StateChangeDelegate OnStateChanged;

    public event Action OnShotFinished;

    public PlayerState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            OnStateChanged?.Invoke(_state);
        }
    }

    public GameObject BulletPrefab
    {
        get { return _bulletPrefab; }
    }
    public float ShotSpeed
    {
        get { return _shotSpeed; }
    }

    public float RecoilForce
    {
        get { return _recoilForce; }
    }

    public float WalkingSpeed
    {
        get { return _walkingSpeed; }
    }

    public bool Shooting { get; set; }
}
