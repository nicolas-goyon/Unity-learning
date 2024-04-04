using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    public bool isWalking { get;  private set; } = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        isWalking = moveDir != Vector3.zero;

        transform.position += moveDir * Time.deltaTime * moveSpeed;

        float rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

}
