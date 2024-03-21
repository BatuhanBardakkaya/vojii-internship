using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public class PlayerDash : PlayerSkillBaseAbstract
{
    protected override void SkillUsed()
    {
        Debug.Log("Dash skill is ready to use again.");
    }

    private void OnEnable()
    {
        CoreGameSignals.OnDashUsed += StartCooldown;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnDashUsed -= StartCooldown;
    }
}
