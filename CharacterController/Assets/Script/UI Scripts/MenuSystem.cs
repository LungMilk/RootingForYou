using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{

#if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneAsset;
#endif
    public void StartGame()
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}
