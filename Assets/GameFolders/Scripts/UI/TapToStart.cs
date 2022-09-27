using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    private Button _button;
    [SerializeField] private CharacterController _controller;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonHandle);
    }

    private void ButtonHandle()
    {
        GameManager.instance.isPlaying = true;
        _button.gameObject.SetActive(false);
        _controller.SetState(_controller.StackState);
    }
}
