using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class LevelManager : Singleton<LevelManager>
{

    [SerializeField] private GameObject _partyContainer;

    public HashSet<SpawnPoint> spawnPoints = new HashSet<SpawnPoint>();

    public SpawnPoint GetPoint(string identifier) {

        foreach (var access in spawnPoints) {

            if(access.uniqueIdentifier == identifier) {
                return access;
            }
            
        }

        return null;

    }

    public void Teleport(Scene sceneOfOrigin, string spawnPointIdentifier) {

        SpawnPoint _spawnPoint = GetPoint(spawnPointIdentifier);

        if(_spawnPoint) {

            // If spawnpoint is in another scene
            if(_spawnPoint.scene != sceneOfOrigin) {

                SceneManager.LoadScene(_spawnPoint.scene.name);

                MoveParty(_spawnPoint.transform.position);
                

            } else {

                MoveParty(_spawnPoint.transform.position);

            }

        } else {

            Debug.LogError("Spawn point with id: " + _spawnPoint + " doesn't exist.");

        }

    }

    public void MoveParty(Vector3 position) {

        _partyContainer.transform.position = position;

        // Reset party positions
        foreach (Transform c in _partyContainer.transform) {
            c.localPosition = Vector3.zero;
        }

    }

    public void LoadScene(string sceneName) {

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }

    public void ExitGame() {

        Application.Quit();

    }

    public void Reset() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
