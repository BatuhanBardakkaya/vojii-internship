using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public interface IInteractable 
{
   public string InteractionPrompt { get; }
   public bool Interact(Interactor interactor);


}
