using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public class PlayerGlide : PlayerSkillBaseAbstract
{
    protected override void SkillUsed()
    {
        Debug.Log("Glide skill is ready to use again.");
    }

    private void OnEnable()
    {
        CoreGameSignals.OnGlideUsed += StartCooldown;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnGlideUsed -= StartCooldown;
    }
}
