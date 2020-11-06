using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // maps id to their playermanager, my guess is there will be duplicates which is an issue
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    // Singleton initializer
    private void Awake() {
        if (instance == null) {
            instance = this;
        } 
        else if (instance != this) {
            Debug.Log("Instance already exists, self-destructing GameManager.cs");
            Destroy(this);
        }
    }

    public void SpawnPlayer (int _id, string _username, Vector3 _position, Quaternion _rotation) {
        GameObject _player;
        if (_id == Client.instance.myId) {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        } else {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        players.Add(_id, _player.GetComponent<PlayerManager>());
        Debug.Log($"Spawning P{_id} \"{_username}\"...");
    }

    public void RemovePlayer (int _id) {
        Debug.Log($"Destructing player {_id}...");
        PlayerManager toRemove = players[_id];
        toRemove.SelfDestruct();
        
        bool found = players.Remove(_id);
        if (!found) {
            Debug.LogError($"Could not find player {_id} in GameManager dictionary to remove.");
        }
    }

}
