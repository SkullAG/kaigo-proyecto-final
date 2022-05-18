using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System.Linq;

public class LevelManager : Singleton<LevelManager>
{

    [SerializeField] private GameObject _partyContainer;

    public static List<TransferData> transferDataList = new List<TransferData>();

    [System.Serializable]
    public class TransferData {

        public string identifier;
        public string sceneName;
        public Vector3 position;
        public Quaternion rotation;

        public TransferData(string identifier, string sceneName, Vector3 position, Quaternion rotation) {

            this.identifier = identifier;
            this.sceneName = sceneName;
            this.position = position;
            this.rotation = rotation;

        }

    }

    public TransferData GetData(string identifier) {

        foreach (var data in transferDataList) {

            if(data.identifier == identifier) {
                return data;
            }
            
        }

        return null;

    }

    public static void RegisterSpawnPoint(SpawnPoint spawnPoint) {

        string _id = spawnPoint.uniqueIdentifier;

        if(!string.IsNullOrWhiteSpace(_id)) {

            // If there isn't a spawnpoint with that id
            if(!transferDataList.Any(x => x.identifier == spawnPoint.uniqueIdentifier)) {

                TransferData _data = new TransferData(
                    spawnPoint.uniqueIdentifier,
                    spawnPoint.scene.name,
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation
                );

                transferDataList.Add(_data);

                Debug.LogWarning("Spawn point: " + _data.identifier +" registered successfully!");

            } else {

                Debug.LogWarning("Spawn point with the id " + spawnPoint.uniqueIdentifier +" is already registered!");

            }

        } else {

            Debug.LogWarning( "Invalid spawn point name." );

        }

    }

    public void Teleport(Scene sceneOfOrigin, string identifier) {

        TransferData _data = GetData(identifier);

        if(_data != null) {

            // If spawn point is in another scene
            if(SceneManager.GetSceneByName(_data.sceneName) != sceneOfOrigin) {

                Scene _previous = SceneManager.GetActiveScene();

                AsyncOperation _op = SceneManager.LoadSceneAsync(_data.sceneName, LoadSceneMode.Additive);

                _op.completed += op => {

                    MoveParty(_data.position);

                    SceneManager.UnloadSceneAsync(_previous);

                };
                

            } else {

                MoveParty(_data.position); // Normal TP

            }

        } else {

            Debug.LogError("Spawn point with id: " + _data.identifier + " doesn't exist.");

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
