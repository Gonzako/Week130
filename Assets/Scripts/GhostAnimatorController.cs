using UnityEngine;

public class GhostAnimatorController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _ani;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _ani.SetFloat("horizontalSpeed", _rb.velocity.x);
        _ani.SetFloat("verticalSpeed", _rb.velocity.y);
    }

    public void AnimatePosession()
    {
    }
}