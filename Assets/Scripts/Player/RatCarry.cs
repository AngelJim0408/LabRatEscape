using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RatCarry : MonoBehaviour
{
    private InputAction _interactAction;
    [SerializeField]
    private bool interact = false;
    [SerializeField]
    private bool interactHeld = false;

    [SerializeField]
    private CarryableObject carriedObject;

    [SerializeField]
    private List<CarryableObject> objectsInRange;
    [SerializeField]
    private CarryableObject closestObjInRange;

    public void AddObjectInRange(CarryableObject objInRange) {
        if (!objectsInRange.Contains(objInRange)) {
            objectsInRange.Add(objInRange);
        }
    }

    public void RemoveObjectFromRange(CarryableObject objInRange) {
        objectsInRange.Remove(objInRange);
    }

    private void Awake() {
       _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void FixedUpdate() {
        // Interact Press
        if (_interactAction.ReadValue<float>() > 0) {
            interact = !interactHeld;
            interactHeld = true;
        }
        else {
            interact = false;
            interactHeld = false;
        }

        if (carriedObject) {
            ManageCarry();
        }
        else {
            ManagePickup();
        }
    }

    private void ManageCarry() {
        if (interact) {
            Drop();
        }
        else {
            carriedObject.transform.position = transform.position + new Vector3(0f, carriedObject.carryHeight, 0f);
        }
    }

    private void ManagePickup() {
        if (objectsInRange.Count > 0) {
            // Find closest object to pickup
            float minSqrDistance = 10000000;
            foreach (CarryableObject obj in objectsInRange) {
                Vector3 offsetVector = obj.transform.position - transform.position;
                float sqrDistance = offsetVector.sqrMagnitude;

                if (sqrDistance < minSqrDistance) {
                    closestObjInRange = obj;
                    minSqrDistance = sqrDistance;
                }
            }

            // Pickup Object
            if (interact) {
                interact = false;
                Pickup(closestObjInRange);
            }
        }
        else {
            closestObjInRange = null;
        }
    }

    private void Pickup(CarryableObject obj) {
        carriedObject = obj;
        objectsInRange.Remove(obj);
        foreach (Collider collider in obj.GetComponents<Collider>()) {
            collider.enabled = false;
        }
        Rigidbody objRb = obj.GetComponent<Rigidbody>();
        if (objRb) {
            objRb.isKinematic = true;
            objRb.detectCollisions = false;
        }
        ManageCarry();
    }

    private void Drop() {
        foreach (Collider collider in carriedObject.GetComponents<Collider>()) {
            collider.enabled = true;
        }
        Rigidbody objRb = carriedObject.GetComponent<Rigidbody>();
        if (objRb) {
            objRb.isKinematic = false;
            objRb.detectCollisions = true;
        }
        carriedObject = null;
    }
}
