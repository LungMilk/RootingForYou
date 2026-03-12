using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    //public SceneAsset sceneAsset;

    public void StartGame()
    {
        SceneManager.LoadScene("Alpha");
    }
}
