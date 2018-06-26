using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int NextLevel = 1; //Main Menu is 0, Tutorial Level is 1 etc.


    public void ChangeScene()
    {
        SceneManager.LoadScene(NextLevel);
        NextLevel++;
    }
    

}