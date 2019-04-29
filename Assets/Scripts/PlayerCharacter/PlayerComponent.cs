using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterComponent : MonoBehaviour
{
    protected PlayerCharacterEntity PlayerCharacter { get; private set; }

    protected virtual void Awake()
    {
        PlayerCharacter = GetComponent<PlayerCharacterEntity>();
    }
}
