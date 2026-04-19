using UnityEngine;

public class MovimientoWASD_SOLOPARAPRUEBAS : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody2D rigidbody2;
    private float move;
    void Start()
    {
     rigidbody2 = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rigidbody2.linearVelocity = new Vector2 (move * speed, rigidbody2.linearVelocity.y);
    }
}
