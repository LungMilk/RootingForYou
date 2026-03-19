using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public GameObject[] pages;
    public int currentPage = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPage = 0;
        NextScreen(currentPage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPage++;
            NextScreen(currentPage);
        }

        if (currentPage >= pages.Length)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NextScreen(int page)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        pages[page].SetActive(true);
    }
}

