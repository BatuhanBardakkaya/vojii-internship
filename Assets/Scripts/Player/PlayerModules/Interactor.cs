using System;
using UnityEngine;


using UnityEngine.UIElements;
using Input = UnityEngine.Windows.Input;

namespace Assets.Scripts.Player.PlayerModules
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform _interactionpoint;
        [SerializeField] private float _interactionPointRadius = 0.5f;
        [SerializeField] private LayerMask _interactableMask;
        [SerializeField] private InteractionPromptUI _interactionPromptUI;
        
        private readonly Collider[] _colliders = new Collider[3];

        [SerializeField] private int _numFound;

        private IInteractable _interactable;
        private void Update()
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionpoint.position, _interactionPointRadius, _colliders,
                _interactableMask);

            if (_numFound>0)
            {
                Debug.Log("NUmfound" + _numFound);
                var _interactable = _colliders[0].GetComponent<IInteractable>();
                if (_interactable != null ) 
                {
                    if (!_interactionPromptUI.isDisplayed)_interactionPromptUI.SetPromptUp(_interactable.InteractionPrompt);

                    if (UnityEngine.Input.GetKeyDown(KeyCode.E)) _interactable.Interact(this);
                    
                }
            }
            else
            {
                if (_interactable != null) _interactable = null;
                if (_interactionPromptUI.isDisplayed)_interactionPromptUI.Close();
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionpoint.position,_interactionPointRadius);
        }
        /*private void Update()
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionpoint.position, _interactionPointRadius, _colliders,
                _interactableMask);

            if (_numFound>0)
            {
                Debug.Log("NUmfound" + _numFound);
                var _interactable = _colliders[0].GetComponent<IInteractable>();
                if (_interactable != null && UnityEngine.Input.GetKeyDown(KeyCode.E)) 
                {
                    Debug.Log("AAA");
                    _interactable.Interact(this);
                }
            }
        }*/
    }
}