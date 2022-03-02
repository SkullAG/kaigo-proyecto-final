using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneKiller : MonoBehaviour
{
    
    [SerializeField]
    private string _sceneToKill;

    private void Start() {

        // Kill scene with all your might!
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(_sceneToKill));
        
        Destroy(gameObject);

    }

}
