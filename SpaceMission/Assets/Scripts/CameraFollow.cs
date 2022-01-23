using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject _target;

    private void Update()
    {
        if (_target != null)
        {
            gameObject.transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, gameObject.transform.position.z);
        }
                
    }
}
