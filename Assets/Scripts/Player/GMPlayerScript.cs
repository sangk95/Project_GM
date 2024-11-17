using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMPlayerScript : MonoBehaviour
{
    private GMPlayerController _controller;

    public void Start()
    {
        _controller = GetComponent<GMPlayerController>();
        if (_controller != null)
            _controller.Init();
    }
    public void SetCharacter()
    {
        _controller = GetComponent<GMPlayerController>();
        if (_controller != null)
            _controller.Init();
        // Set character's sprite
    }
}
