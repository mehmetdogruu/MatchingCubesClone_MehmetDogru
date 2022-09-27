using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private CharacterMovement _player;
    private void Awake()
    {
        _player = FindObjectOfType<CharacterMovement>();
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isPlaying) return;
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);
    }
}
