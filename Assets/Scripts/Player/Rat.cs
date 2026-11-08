using UnityEngine;
using UnityEngine.SceneManagement;

public class Rat : MonoBehaviour
{
    public void Kill() {
        Debug.Log("Player Died");
        // TODO: Add Animation
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
