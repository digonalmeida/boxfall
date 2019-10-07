using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Birds;
using UnityEngine;

public class BirdFactory : MonoBehaviour
{
    public static BirdFactory Instance { get; private set; }
    
    [SerializeField] private GameObject _blueBirdSprite;
    [SerializeField] private GameObject _pinkBirdSprite;
    [SerializeField] private GameObject _yellowBirdSprite;
    [SerializeField] private GameObject _blackBirdSprite;
    [SerializeField] private GameObject _redBirdSprite;

    [SerializeField] private GameObject _baseBird;
    [SerializeField] private GameObject _baseDivingBird;

    private void Awake()
    {
        Instance = this;
    }

    public BirdController CreateBird(BirdData birdData)
    {
        GameObject basePrefab = GetBasePrefab(birdData.MovementType);
        GameObject spritePrefab = GetSpritePrefab(birdData.Color);

        BirdController baseInstance = InstantiateBaseBird(basePrefab);
        SpriteRenderer spriteInstance = InstantiateSprite(spritePrefab);
        baseInstance.SetData(birdData);
        baseInstance.SetupSpriteInstance(spriteInstance);

        return baseInstance;
    }

    private SpriteRenderer InstantiateSprite(GameObject spritePrefab)
    {
        GameObject instance = Instantiate(spritePrefab);
        instance.name = "Sprite";
        return instance.GetComponent<SpriteRenderer>();
    }

    private BirdController InstantiateBaseBird(GameObject basePrefab)
    {
        GameObject instance = Instantiate(basePrefab);
        instance.name = "bird_" + basePrefab.name;
        return instance.GetComponent<BirdController>();
    }

    private GameObject GetSpritePrefab(BirdColor birdDataColor)
    {
        switch (birdDataColor)
        {
            case BirdColor.Blue:
                return _blueBirdSprite;
                break;
            case BirdColor.Pink:
                return _pinkBirdSprite;
                break;
            case BirdColor.Yellow:
                return _yellowBirdSprite;
                break;
            case BirdColor.Black:
                return _blackBirdSprite;
                break;
            case BirdColor.Red:
                return _redBirdSprite;
                break;
            default:
                return _redBirdSprite;
        }
    }

    public GameObject GetBasePrefab(MovementType movementType)
    {
        switch (movementType)
        {
            case MovementType.Dive: return _baseDivingBird;
            case MovementType.Physics: return _baseBird;
            default: return _baseBird;
        }
    }
}
