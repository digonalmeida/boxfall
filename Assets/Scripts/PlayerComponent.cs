using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterComponent : MonoBehaviour
{
    protected List<PlayerState> activateOnStates;
    protected PlayerCharacterEntity PlayerCharacter { get; private set; }

    protected virtual void Awake()
    {
        PlayerCharacter = GetComponent<PlayerCharacterEntity>();
        PlayerCharacter.OnStateChanged += OnStateChanged;
        activateOnStates = new List<PlayerState>();
    }

    private void OnDestroy()
    {
        if(PlayerCharacter != null)
        {
            PlayerCharacter.OnStateChanged -= OnStateChanged;
        }
    }

    protected virtual void OnStateChanged(PlayerState newState)
    {

    }
}
