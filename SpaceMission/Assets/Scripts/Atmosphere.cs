using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    [SerializeField] float _gravityForce;
    private Rigidbody2D _target;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var rigidBody = other.gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            _target = rigidBody;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        var rigidBody = other.gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            _target = null;
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            var atmosphereCentr = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            _target.AddForceAtPosition((atmosphereCentr - _target.position) * _gravityForce, gameObject.transform.position, ForceMode2D.Force);
        }
        
    }

}
