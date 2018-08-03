using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    static int CurrentLevel = 0;

    public void ChangeScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        CurrentLevel++;
        SceneManager.LoadScene(CurrentLevel);
    }
    
    public void ReloadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(CurrentLevel);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Collectibles.finalScore = Collectibles.currentScore;
            ChangeScene();
        }
    }
}