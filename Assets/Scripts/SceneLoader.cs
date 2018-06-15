using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void ChangeScene(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
