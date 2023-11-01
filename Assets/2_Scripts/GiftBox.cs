using UnityEngine;

public class GiftBox : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetTrigger("OpenGiftBox");
    }
}
