using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;
	bool isWalking;
	Vector3 lookInput;

	// hashing
	int id_walk = Animator.StringToHash("IsWalking");

	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{

		Turning();
        //Animating(h, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

    void OnMovement(InputValue v)
    {
        Debug.Log("OnMovement Called!");
        Vector2 inputVector = v.Get<Vector2>();
        movement = new Vector3(inputVector.x, 0, inputVector.y);
        anim.transform.forward = movement.normalized;
		if (movement.magnitude > 0)
		{
			isWalking = true;
		}
        else
        {
            isWalking = false;
        }
		anim.SetBool(id_walk, isWalking);

    }

    void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	/*void OnLook(InputValue v)
	{

		lookInput = v.Get<Vector2>();

		Vector3 Look = new Vector3(lookInput.x, 0, lookInput.y);

		Debug.Log("Onlook Called");
        Ray camRay = Camera.main.ScreenPointToRay(Look);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }*/

	/*void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;

		anim.SetBool(id_walk, walking);
	}*/
}
