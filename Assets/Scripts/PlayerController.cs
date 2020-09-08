using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject restartButton;
    public bool scoreDetect = false;
	public LayerMask groundLayer;

    [Header("CHARACTER")]
    private Rigidbody2D rb;
    [SerializeField] private float speedX = 4.0f;
    [SerializeField] private float fallLimitSpeed = 4.0f;
    [SerializeField] private bool isGround;
    private Vector2 movement;
    private float yTempCoord;
    

    [Header("FLAGS")]
    private bool flyFlag = false;
    private bool fallFlag = false;

    [Header("SCRIPTS")]
    private ScoreManager scoreManager;
    private LevelGenerator levelGenerator;

    private DateTime dateTime;


    void Start()
    {
        Time.timeScale = 1; // Zamanı normal olarak başlatıyorum
        Debug.Log("Oyun başladı!");
        dateTime = DateTime.Now;
        restartButton.SetActive(false);
        rb = this.GetComponent<Rigidbody2D>();
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        levelGenerator = GameObject.Find("GameManager").GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
	}

    private void FixedUpdate()
    {
        PlayerControls();
        FallControl();
        DamageControl();
        ComboControl();
    }

    private void ComboControl()
	{
        float speed = rb.velocity.magnitude;
        // Debug.Log(speed);
		if (speed < 4)
		{
            scoreManager.comboFlag = false;
		}
		else
		{
            scoreManager.comboFlag = true;
		}
	}

    private void PlayerControls()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGround) // Yerdeyse aşağıya kuvvet vermeden hareket edecek
            {
                movement = new Vector2(1, 0);
                // Debug.Log("Yerde");
            }
            else
            {
                movement = new Vector2(0, -1);
                // Debug.Log("Yerde değil");
            }
            moveCharacter(movement);
        }
        else
        {
            if (isGround)
            {
                // asd
            }
            else
            {
                rb.AddForce(transform.forward * rb.velocity.magnitude);
            }
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (isGround) // Yerdeyse aşağıya kuvvet vermeden hareket edecek
            {
                movement = new Vector2(1, 0);
                // Debug.Log("Yerde");
            }
            else
            {
                movement = new Vector2(0, -1);
                // Debug.Log("Yerde değil");
            }
            moveCharacter(movement);
        }
        else
        {
            if (isGround)
            {
                // asd
            }
            else
            {
                rb.AddForce(transform.forward * rb.velocity.magnitude);
            }
        }
#endif
    }

    private void moveCharacter(Vector2 direction)
    {
        rb.AddForce(movement * speedX);
    }

    private void DamageControl()
    { 
        // Burada karakterin hızını alıp ona göre işlem yaptırıyoruz
        if (FallDamage(rb.velocity.magnitude))
        {
            // Debug.LogError("Hız çok yüksek!");
            if (IsDamage())
            {
                // Oyun bitti hızlı çarptı düz alana
                restartButton.SetActive(true);
                TimeSpan date = DateTime.Now - dateTime;
                Debug.Log($"Game has finished in {date.Seconds} seconds");
                Time.timeScale = 0;
            }
        }
        else
        {
           // Debug.Log("Hız düşük");
        }
    }

    private void FallControl()
	{
		if (yTempCoord < transform.position.y) // Burada bir önceki y koordinatından yüksekse yükseldiğini anlıyoruz değilse aşağı doğru düşmeye başlar.
		{
			if (flyFlag == false)
			{
				Debug.Log("Yükseliyor");
				flyFlag = true;
				fallFlag = false;
			}
			scoreDetect = true;
		}
		else
		{
			if (fallFlag == false)
			{
				Debug.Log("Düşüyor");
				fallFlag = true;
				flyFlag = false;
			}
			scoreDetect = false;
		}
		yTempCoord = transform.position.y;

	}

	private bool FallDamage(float speed)
    {
        // Debug.Log(speed);
        if (speed > fallLimitSpeed) // eğer belirlediğimiz değerden yüksek bir değerde hızlanma varsa true döndürüp oyunu bitirecek çarpınca
		{
            return true;
		}
		else
		{
            return false;
		}
    }

    private bool IsDamage()
    {
        // Raycast ile sağ tarafa ray gönderiyorum böylece düzleme çarptığını anlıyoruzki yükselme rampalarında bozulmasın
        Vector2 position = transform.position;
        Vector2 direction = Vector2.right;
        float distance = 0.25f;
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null && hit.collider.isTrigger == false)
        {
			if (isGround && scoreDetect) // scoreDetect düştüğünü anlıyoruz
			{
                return true;
			}
        }
        return false;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.transform.tag == "Ground")
		{
            // Debug.Log("Yerde");
            isGround = true; // Yerde olduğunu bildiriyor
		}

        
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
        isGround = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.LogWarning(scoreDetect);

        if (scoreDetect) // Yükselirken skor yap
        {
            if (collision.transform.tag == "Score") // Score triggerlarına girip skor yaptırıyor
            {
                // scoreManager.score = (scoreManager.score + 1);
                scoreManager.score = scoreManager.comboCount * (scoreManager.score + 1); // eğer beklenenden az yavaşlarsa combo yapacak
                if (scoreManager.comboFlag)
                {
                    scoreManager.comboCount = scoreManager.comboCount + 1;
                }
                else
                {
                    scoreManager.comboCount = 1;
                }
                Debug.Log($"Combo sayısı: {scoreManager.comboCount}");
            }
        }

        if (collision.transform.tag == "Platform")
        {
            levelGenerator.SpawnPlatform(); // Platformları spawn ettik
            levelGenerator.SpawnScore(); // Score triggerlarını spawn ettik
            collision.enabled = false;
        }
    }
}
