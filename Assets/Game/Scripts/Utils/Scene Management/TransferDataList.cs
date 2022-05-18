using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;
using System.Linq;

[CreateAssetMenu(fileName = "TransferDataList", menuName = "Game/TransferDataList")]
public class TransferDataList : ScriptableObject
{
    [SerializeField] public List<TransferData> transferDataList = new List<TransferData>();

    public void RegisterSpawnPoint(SpawnPoint spawnPoint)
    {

        string _id = spawnPoint.uniqueIdentifier;

        if (!string.IsNullOrWhiteSpace(_id))
        {

            // If there isn't a spawnpoint with that id
            if (!transferDataList.Any(x => x.identifier == spawnPoint.uniqueIdentifier))
            {

                TransferData _data = new TransferData(
                    spawnPoint.uniqueIdentifier,
                    spawnPoint.scene.name,
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation
                );

                transferDataList.Add(_data);

                Debug.LogWarning("Spawn point: " + _data.identifier + " registered successfully!");

            }
            else
            {

                Debug.LogWarning("Spawn point with the id " + spawnPoint.uniqueIdentifier + " is already registered!");

            }

        }
        else
        {

            Debug.LogWarning("Invalid spawn point name.");

        }

    }
}
