using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.ModulerSystem;
using Assets.Scripts.Player.PlayerModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : AgentModuleBase,IResettable
{
    public float distance = 50.0f; // The distance from the target object
    public float xSpeed = 120.0f; // The speed of rotation along the x-axis
    public float ySpeed = 120.0f; // The speed of rotation along the y-axis

    public float yMinLimit = 5f; // Minimum y-axis rotation limit
    public float yMaxLimit = 80f; // Maximum y-axis rotation limit

    public float distanceMin = .5f; // Minimum distance from the target
    public float distanceMax = 15f; // Maximum distance from the target

    private Rigidbody Rigidbody; // Rigidbody component attached to the camera
    [field: SerializeField] public Transform CameraT;

    float x = 0.0f; // Initial x-axis rotation
    float y = 0.0f; // Initial y-axis rotation
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float initialDistance;


    public  override IEnumerator IE_Initialize()
    {
        Debug.Log("Deneme1");
        base.IE_Initialize();
        Debug.Log("Deneme2");
        Vector3 angles = CameraT.eulerAngles; // Get the initial rotation angles
        x = angles.y; // Set initial x-axis rotation
        y = angles.x; // Set initial y-axis rotation

        Rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component

        // Freeze rotation of the Rigidbody to manually control camera rotation
        if (Rigidbody != null)
        {
            Rigidbody.freezeRotation = true;
        }
        initialPosition = CameraT.position;
        initialRotation = CameraT.rotation;
        initialDistance = distance;

        PlayerMovementModule movementModule = GetComponentInParent<AgentControlBase>().GetModule<PlayerMovementModule>();


        
        if (movementModule != null)
        {
            movementModule.OnJump += HandleJump; // Event'e handler atama
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return null;
    }

    private void HandleJump()
    {
        Debug.Log("Jumping");
    }
    
    public void ResetToInitialState()
    {
        
        CameraT.position = initialPosition;
        CameraT.rotation = initialRotation;
        distance = initialDistance;

       
        Vector3 angles = initialRotation.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

  
    public override void LateTick()
    {
        base.LateTick();
        // If a target is assigned
        if (GeneralState != ModuleState.Activated)
        {
            return;
        }

        // Rotate the camera based on mouse movement
        if (Input.GetMouseButton(1)) // Check if right mouse button is pressed
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                // Clamp the y-axis rotation within specified limits
                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            // Adjust the camera distance using the mouse scroll wheel
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            // Calculate the rotation and position of the camera
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + transform.position;

            // Apply the calculated rotation and position to the camera
            CameraT.rotation = rotation;
            CameraT.position = position;
        
    }
 
    // ClampAngle function ensures the angle stays within the specified min and max range
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}