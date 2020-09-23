using UnityEngine;

public class AlligatorScript : MonoBehaviour
{
    public Transform _target;
    private bool _isFocussingDuck;
    private int _targetIndex;
    public Transform[] moveSpots;

    public float speed;
    public float speedBoost;
    
    // Start is called before the first frame update
    void Start()
    {
        _isFocussingDuck = false;
        _target = moveSpots[0];
        GetNewTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get to next spot
        var position = _target.position;
        Transform transform1;
        
        (transform1 = transform).position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        
        // rotate aligator to face direction
        var lookDir = position.normalized - transform1.position.normalized ; ///new Vector3(movement.x, 0f, movement.z);
        transform.rotation = Quaternion.LookRotation(lookDir);
        
        if (_isFocussingDuck)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position, _target.position) < 0.2f)
        {

            _targetIndex++;
            if (_targetIndex >= moveSpots.Length)
            {
                _targetIndex = 0;
            }

            _target = moveSpots[_targetIndex];

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Inventory playerDuck = other.GetComponent<Inventory>();

        // check, if duck is in range
        if (playerDuck == null)
        {
            return;
        }
        
        AimForDuck(playerDuck);
    }

    private void AimForDuck(Inventory playerDuck)
    {
        _isFocussingDuck = true;
        _target = playerDuck.transform;
        speed += speedBoost;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject playerDuck = GameObject.FindWithTag("Player");
        
        Debug.Log("fucus: "+_isFocussingDuck);

        // check, if duck has exited detection range
        if (_isFocussingDuck && playerDuck == null)
        {
            Debug.Log("HERE");
            speed -= speedBoost;
            GetNewTarget();
            
            return;
        }
    }
    

    // select randomly the new target
    private void GetNewTarget()
    {
        _targetIndex = Random.Range(0, moveSpots.Length);
        _target = moveSpots[_targetIndex];
    }
}
