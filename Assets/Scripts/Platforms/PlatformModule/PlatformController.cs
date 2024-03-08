using Assets.Scripts.MoveThings;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformController : MoveModuleBase
{
    public override IEnumerator IE_Initialize()
    {
        transform.DOLocalMoveX(16.14f, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        yield return null;
    }
    
}
