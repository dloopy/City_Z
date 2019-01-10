using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public int moveSpeed = 10;
    public int rotationSpeed = 10;

   

    private Animator _anim;
    private float _moveAnim;

	// Use this for initialization
	void Start () 
    {
        _anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        float verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        _moveAnim = verticalMove;

        //_moveInput = new Vector3(horizontalMove, 0f, verticalMove);

       //_moveVelocity = transform.forward * moveSpeed * _moveInput.sqrMagnitude;

        transform.Rotate(0, horizontalMove, 0);
        transform.Translate(0, 0, verticalMove);

        Animating();
    }

    void Animating()
    {
        _anim.SetFloat("Blend", _moveAnim);
    }


}
