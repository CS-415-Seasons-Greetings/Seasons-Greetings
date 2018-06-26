using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMainMenu : MonoBehaviour {

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
