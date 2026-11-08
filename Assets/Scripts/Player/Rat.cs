using UnityEngine;
using UnityEngine.SceneManagement;

public class Rat : MonoBehaviour
{
    public void Kill() {
        Debug.Log("Player Died");
        // TODO: Add Animation
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void Win() {
        Debug.Log("Player Won!");
        // TODO: Move to next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // TEMP: Reload the current scene
    }
}
