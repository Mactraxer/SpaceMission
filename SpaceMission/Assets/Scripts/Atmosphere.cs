using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    [SerializeField] 
    private float _gravityForce;
    [SerializeField]
    private float _planetMass;
    [SerializeField]
    private Transform _planetTransform;

    [SerializeField]
    private  float GravityConst = 0.00667f;

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
            var distanseVector = _planetTransform.position - _target.transform.position;
            var distanse = distanseVector.magnitude;
            
            var appliedForce = GravityConst * (_target.mass * _planetMass / (distanse * distanse));
                       
            _target.AddForceAtPosition(distanseVector.normalized * appliedForce, _target.transform.position, ForceMode2D.Force);
            
        }
        
    }

    

}
