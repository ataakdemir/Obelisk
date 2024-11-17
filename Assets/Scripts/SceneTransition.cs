using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    private bool isTransitioning = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isTransitioning)
        {
            isTransitioning = true;
            playerStorage.initialValue = playerPosition;
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        LevelLoad.Instance.FadeOut();
        yield return new WaitForSeconds(1); 

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LevelLoad.Instance.FadeIn();
        yield return new WaitForSeconds(1); 

        isTransitioning = false;
    }
}
