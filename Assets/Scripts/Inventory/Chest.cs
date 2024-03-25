using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public class Chest : MonoBehaviour , IInteractable
{
    private Animator _animator;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Chest opened successfuly");
        _animator.SetTrigger("ChestOpen");
        return true;
    }
}
