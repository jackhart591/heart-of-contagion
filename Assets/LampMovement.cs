using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int AIType = 0; // default is 0- nothing
    [SerializeField] Light2D lightInst;
    [SerializeField] float MaxDist;
    [SerializeField] float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb)
        {

            switch (AIType)
            {


                case 0:
                    Debug.Log("NOTHING");

                    break;

                case 1:
                    LerpMovement();
                    break;
                case 2:
                    RotoMover();

                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }
        }
    }

    public void LerpMovement()
    {
        float value = Mathf.Lerp(-MaxDist * speed, MaxDist * speed, Mathf.PingPong(Time.time / (3 / speed), 1));
        rb.velocity = new Vector2(value, rb.velocity.y);
    }

    public void RotoMover()
    {

        float value = Mathf.Lerp(130, 230, Mathf.PingPong(Time.time / (3 / speed), 1));
        gameObject.transform.localEulerAngles = new Vector3(0,0, value);
    }
}
