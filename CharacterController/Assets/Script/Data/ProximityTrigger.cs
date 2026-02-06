using UnityEngine;
using UnityEngine.Events;
public class ProximityTrigger : MonoBehaviour
{
    //when player is nearby, trigger an event
    [Header("Player Only!!!!!")]
    [Space(10)]
    public UnityEvent OnProxEnter;
    public UnityEvent OnProxExit;

    private PlayerStateMachine _player;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<PlayerStateMachine>(out _player);
        if(_player != null)
        {
            OnProxEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _player.gameObject) { return; }
        OnProxExit.Invoke();
        _player = null;
    }
}
