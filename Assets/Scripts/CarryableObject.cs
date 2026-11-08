using UnityEngine;

public class CarryableObject : MonoBehaviour
{
    [Min(0f)]
    public float carryHeight = 1f;

    private RatCarry ratCarry;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            ratCarry = other.GetComponent<RatCarry>();
            ratCarry.AddObjectInRange(this);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" && ratCarry) {
            ratCarry.RemoveObjectFromRange(this);
        }
    }
}
