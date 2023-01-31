using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ZombieAI1 : MonoBehaviour, IDamage
{
    
    private Animator animZombie;
    private Transform target;
    public GameObject player;
    public AudioClip attackSound;
    public AudioClip dyingSound;
    AudioSource attackSound1;
    AudioSource dyingSound1;
    private Vector3 direction;
    private PauseMenu pause;
    private Animation animDye;
    private Player play;
    private bool dead;
    private bool attack;
    private bool run;
    private bool isDead;
    public static bool deadHuman;
    public static int zombieCounter;
    public int zomHealth;
    public float lookRadius;
    private float countDownTimer;
    private float delay;
    private float distance;
    private float angle;

    public float GetZombieHealth()
    {
        return zomHealth;
    }

    public bool GetZombieDead()
    {
        return dead;
    }

    public bool GetZombieAttack()
    {
        return attack;
    }

    public bool GetZombieRun()
    {
        return run;
    }

    public void Damage(int x)
    {
        zomHealth = zomHealth - x;
        if (zomHealth <= 0 && !isDead)
        {
            isDead = true;
            zombieCounter++;
            Debug.Log(zombieCounter);
            dead = true;
        }
    }

    void Start()
    {
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 2f;
        run = false;
        zombieCounter = 0;
        delay = 1f;
        countDownTimer = delay;
        dead = false;
        attack = false;
        deadHuman = false;
        zomHealth = 150;
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = PlayerManager.instance.player.transform;
        animZombie = GetComponent<Animator>();
        pause = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        animDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation>();
        attackSound1 = GameObject.Find("zombie attack").GetComponent<AudioSource>();
        dyingSound1 = GameObject.Find("zombie dying").GetComponent<AudioSource>();
        Knife weapon1 = player.GetComponent<Knife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
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
        else
        {
            dyingSound1.PlayOneShot(dyingSound, 4f);
            animZombie.Play("Zombie Dying");
            StartCoroutine(MakeZombieDisapper());
        }
        IEnumerator MakeZombieDisapper()
        {
            yield return new WaitForSeconds(15f);
            this.gameObject.SetActive(false);
        }
        if (play.playerHealth <= 0)
        {
            animDye.Play("playAnim");
            if (countDownTimer > 0f)
            {
                countDownTimer -= Time.deltaTime;
            }
            if (countDownTimer <= 0)
            {
                pause.resumeButton.SetActive(false);
                countDownTimer = delay;
                pause.Pause();
            }
            deadHuman = true;
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

    private void FeelSense()
    {
        if (distance <= 2f)
        {
            Knife weapon1 = player.GetComponent<Knife>();
            if (weapon1.knifeActive && weapon1.firingKnife)
            {
                Damage(weapon1.knifeDamage);
            }
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
                StartCoroutine(DecrementOfPlayerHealth());
            }
            IEnumerator DecrementOfPlayerHealth()
            {
                yield return new WaitForSeconds(3f);
                play.playerHealth -= 40f;
            }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), Time.deltaTime * 5f);
    }

    public void SaveGame()
    {
        ZombieAI1Data save = new ZombieAI1Data();
        save.zombieHealthFile = zomHealth;
        save.zombieDeadFile = GetZombieDead();
        save.zombieAttackFile = GetZombieAttack();
        save.zombieRunFile = GetZombieRun();
        save.deadHumanFile = deadHuman;
        save.zombieCounterFile = ZombieAI1.zombieCounter;
        SavingSystem<ZombieAI1Data>.Save(Application.persistentDataPath + "/file11.pqr", save);
    }

    public void LoadGame()
    {
        deadHuman = false;
        ZombieAI1Data load = SavingSystem<ZombieAI1Data>.Load(Application.persistentDataPath + "/file11.pqr");
        zomHealth = load.zombieHealthFile;
        dead = load.zombieDeadFile;
        attack = load.zombieAttackFile;
        run = load.zombieRunFile;
        zombieCounter = load.zombieCounterFile;
        deadHuman = load.deadHumanFile;
    }
}

[System.Serializable]
public class ZombieAI1Data
{
    public bool zombieDeadFile;
    public bool zombieAttackFile;
    public bool zombieRunFile;
    public int zombieHealthFile;
    public int zombieCounterFile;
    public bool deadHumanFile;
}