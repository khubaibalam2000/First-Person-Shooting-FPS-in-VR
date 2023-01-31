using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public GameObject XRPlayerReference;
    private Animator animZombie;
    public Transform target;
    private bool run;
    private bool attack;
    public AudioClip attackSound;
    AudioSource attackSound1;
    private float distance;
    private Vector3 direction;
    private float angle;
    private float lookRadius = 32.5f;

    // Start is called before the first frame update
    void Start()
    {
        attackSound1 = GameObject.Find("zombie attack").GetComponent<AudioSource>();
        attack = false;
        run = false;
        animZombie = GetComponent<Animator>();
        XRPlayerReference = GameObject.FindGameObjectWithTag("Player");
        target = XRPlayerReference.transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        direction = target.position - this.transform.position;
        angle = Vector3.Angle(direction, this.transform.forward);

        //Sighting sense
        if ((distance <= lookRadius && angle <= 180) || (distance <= 3))
        {
            //then sighting sense will call feel sense on the basis of conditions
            SightingSense();
        }
        else
        {
            run = false;
            if (!run)
            {
                animZombie.SetBool("Run", false);
            }
            attack = false;
            if (!attack)
            {
                animZombie.SetBool("Attack", false);
            }
        }
    }
    private void SightingSense()
    {
        FaceTarget();
        run = true;
        if (run)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * 4f);
            animZombie.SetBool("Run", true);
        }
        FeelSense();
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), Time.deltaTime * 5f);
    }

    private void FeelSense()
    {
        if (distance <= 2f)
        {
            run = false;
            if (!run)
            {
                animZombie.SetBool("Run", false);
            }

            attack = true;
            if (attack)
            {
                animZombie.SetBool("Attack", true);
                attackSound1.PlayOneShot(attackSound, 6f);
                //StartCoroutine(DecrementOfPlayerHealth());
            }
            //IEnumerator DecrementOfPlayerHealth()
            //{
              //  yield return new WaitForSeconds(3f);
                //play.playerHealth -= 40f;
            //}
        }
        else
        {
            run = true;
            if (run)
            {
                animZombie.SetBool("Run", true);
                this.transform.Translate(Vector3.forward * Time.deltaTime * 4f);
            }
            attack = false;
            if (!attack)
            {
                animZombie.SetBool("Attack", false);
            }
        }
    }
}
