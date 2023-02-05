using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    private int FlipVal = 1;
    [SerializeField] GameObject Anchor;
    private bool usedAbility = false;
    private bool ActiveAbility = false;
    [SerializeField] float AgroRange = 10f;
    [SerializeField] GameObject player;



    [SerializeField] float MaxDist;
    [SerializeField] float speed;

    
      
        private void Start()
    {
            if (gameObject.transform.localScale.x < 0)
            {
                Debug.Log("WRONG");
                FlipVal = -FlipVal;
            }
            rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player)
        {
            float disty = Vector2.Distance(new Vector2(gameObject.transform.position.x, 0), new Vector2(player.transform.position.x, 0));

            if (usedAbility == false && disty <= AgroRange)
            {
                usedAbility = true;
                ActiveAbility = true;
                StartCoroutine(FollowArc(gameObject.transform, gameObject.transform.position, gameObject.transform.position + new Vector3(-9f * FlipVal, 0, 0), 2, .6f));
            }
            else if (ActiveAbility == false)
            {

                LerpMovement();

            }



        }
    }
    public void LerpMovement()
    {
        Debug.Log("MOVING");
        float value = Mathf.Lerp(-MaxDist * speed, MaxDist * speed, Mathf.PingPong(Time.time / (3 / speed), 1));
        rb.velocity = new Vector2(value, rb.velocity.y);
    }
    IEnumerator FollowArc(
       Transform mover,
       Vector2 start,
       Vector2 end,
       float radius, // Set this to negative if you want to flip the arc.
       float duration)
    {

        rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(.3f);
        Vector2 difference = end - start;
        float span = difference.magnitude;

        // Override the radius if it's too small to bridge the points.
        float absRadius = radius;
        if (span > 2f * absRadius)
            radius = absRadius = span / 2f;

        Vector2 perpendicular = new Vector2(difference.y, -difference.x) / span;
        perpendicular *= Mathf.Sign(radius) * Mathf.Sqrt(radius * radius - span * span / 4f);

        Vector2 center = start + difference / 2f + perpendicular;

        Vector2 toStart = start - center;
        float startAngle = Mathf.Atan2(toStart.y, toStart.x);

        Vector2 toEnd = end - center;
        float endAngle = Mathf.Atan2(toEnd.y, toEnd.x);

        // Choose the smaller of two angles separating the start & end
        float travel = ((endAngle - startAngle + 5f * Mathf.PI) % (2f * Mathf.PI) - Mathf.PI) * FlipVal;

        float progress = 0f;
        do
        {
            float angle = startAngle + progress * travel;
            mover.position = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * absRadius;
            progress += Time.deltaTime / duration;
            yield return null;
        } while (progress < 1f);

        mover.position = end;
        float progressII = 0f;
        do
        {



            transform.position = Vector3.MoveTowards(transform.position, Anchor.transform.position, 1 * progressII);
            progressII += Time.deltaTime;
            yield return null;
        } while (progressII < 1f);
        rb.velocity = Vector2.zero;
        transform.position = Anchor.transform.position;
        ActiveAbility = false;
        yield return new WaitForSeconds(3f);
        usedAbility = false;

    }
}
