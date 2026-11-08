using UnityEngine;

public class Hazard : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Rat rat = other.GetComponent<Rat>();
            rat.Kill();
        }
    }
}
