using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.instance;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Human"))
        {
            _gameManager.AddPlayerScore(true);
            Destroy(hit.gameObject);
            Debug.Log("Human killed!");
        }
        else if (hit.gameObject.CompareTag("Enemy"))
        {
            _gameManager.AddPlayerScore(false);
            Destroy(hit.gameObject);
            Debug.Log("Enemy killed!");
        }
    }

}
