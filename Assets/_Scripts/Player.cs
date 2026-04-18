using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] Collider footCollider;
    public float stepSize = 5;
    public float vel = 5;
    Rigidbody2D rb2D;

    private bool moviendo;
    private bool cayendo = false;
    //public Transform target;
    private Vector3 targetPosition;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
       targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        


        if (!cayendo)
        {


            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float mouseX = mousePosition.x;
                float actualX = transform.position.x;
                float direccionX = 0;
                if (mouseX > actualX)
                {
                    direccionX = 1;
                }
                else
                {
                    direccionX = -1;
                }

                targetPosition = new Vector3(transform.position.x + (direccionX * stepSize), transform.position.y, 0);
                moviendo = true;
                //print("moviendo");

                //transform.position = Vector3.MoveTowards(transform.position, targetPosition, vel*Time.fixedDeltaTime);
            }
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == null)
        {
            print("se esta cayendo");
            cayendo = true;
            return;
        }

        cayendo = false ;
    }


    void FixedUpdate()
    {
            if (!moviendo)
        {
            return;
        }

            Vector2 newPosition = Vector2.MoveTowards(rb2D.position, targetPosition, vel);
            rb2D.MovePosition(newPosition);
            
        if(Vector2.Distance(rb2D.position, targetPosition) < 0.01)
        {
            rb2D.MovePosition(targetPosition);
            rb2D.linearVelocity = Vector2.zero;
            moviendo = false;

        }
        }
}
