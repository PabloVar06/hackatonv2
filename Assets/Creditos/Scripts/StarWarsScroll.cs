using UnityEngine;
using UnityEngine.SceneManagement;

public class StarWarsScroll : MonoBehaviour
{
    public float scrollSpeed = 35f;   
    public float endYPosition = 2800f; 
    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime, Space.Self);

        if (transform.localPosition.y > endYPosition || Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}