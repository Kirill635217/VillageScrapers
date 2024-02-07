using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Ease animationEase = Ease.InBounce;
    [SerializeField] private Transform[] randomTileContent;

    private void Awake()
    {
        RandomizeContentPlacement();
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .3f).SetEase(animationEase);
    }

    private void RandomizeContentPlacement()
    {
        foreach (var randomTileTransform in randomTileContent)
        {
            float maxX = 1 - randomTileTransform.localScale.x / 2;
            float maxZ = 1 - randomTileTransform.localScale.z / 2;
            randomTileTransform.localPosition = new Vector3(UnityEngine.Random.Range(-maxX / 2, maxX / 2),
                randomTileTransform.localPosition.y, UnityEngine.Random.Range(-maxZ / 2, maxZ / 2));
        }
    }
}