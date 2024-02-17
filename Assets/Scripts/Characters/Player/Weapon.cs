using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] weapons; // Kullanılabilir silahlar
    private int currentWeaponIndex = 0;
    
    private bool IsAttack = false;
    private Rigidbody2D BulletRigidbody;

    private bool Direction;
    public GameObject bulletPrefab; // Mermi prefab'ı
    public Transform firePoint; // Ateşleme noktası
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public KeyCode ColorChangeKey;

    public Color[] bulletColors; // Renkli mermilerin renkleri
    private Color BulletColor;
    private int currentColorIndex = 0;
    

    void Update()
    {
        Direction = gameObject.GetComponent<PlayerMovement>().GetDirection();
        if ( Direction == true && currentWeaponIndex ==2)
        {
            firePoint.localPosition = new Vector2(-1.5f, 1);
        }
        else if ( Direction == false && currentWeaponIndex == 2)
        {
            firePoint.localPosition = new Vector2(1.5f, 1);
        }
        else if( Direction == true && currentWeaponIndex == 1)
        {
            firePoint.localPosition = new Vector2(-1.85f, 1.45f);
        }
        else
        {
            firePoint.localPosition = new Vector2(1.85f, 1.45f);
        }

        // Silah değiştirme mekanizması
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            ChangeWeapon(scroll);
        }

        if (Input.GetKey(ColorChangeKey))
        {
            ChangeColor();
        }

        // Silah ateşleme mekanizması
        if (Input.GetButtonDown("Fire1") && IsAttack !=true)
        {
            Attack();
            
        }
    }

    
    void ChangeColor()
    {
        if(currentColorIndex >= bulletColors.Length)
        {
            currentColorIndex = 0;
        }
        BulletColor = bulletColors[currentColorIndex];
        currentColorIndex++;
        Debug.Log(BulletColor);
    }
    void ChangeWeapon(float scroll)
    {

    weapons[currentWeaponIndex].tag = "Untagged"; // Önceki silahın tag'ini temizle

    // Scroll yönüne göre yeni silahın index'ini belirle
    if (scroll > 0) // Yukarı scroll
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
    }
    else if (scroll < 0) // Aşağı scroll
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
    }


    weapons[currentWeaponIndex].tag = "SelectedWeapon";
    }

    void Attack()
    {
        IsAttack = true;
        // Sadece seçili silah ateş etsin
        GameObject selectedWeapon = weapons[currentWeaponIndex];
        if (currentWeaponIndex !=0)
        {
            
            if (selectedWeapon.CompareTag("SelectedWeapon"))
            {
                
                switch (currentWeaponIndex)
                {
                    case 0:
                        break; 
                    case 1:
                        // Mermi rengini seçili silaha göre belirle
                        
                        //saldırı animasyonu için süre
                        StartCoroutine(SetAttackFalse());
                        // Mermi oluşturma
                        StartCoroutine(MakeBullet(BulletColor));
                        break; 
                    case 2:
                        StartCoroutine(SetAttackFalse());
                        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, attackRange, enemyLayers);
                        gameObject.GetComponent<PlayerMovement>().SetCombo(1);
                        foreach (Collider2D enemy in hitEnemies)
                        {
                            StartCoroutine(Attack(enemy));
                        }
                        
                        StartCoroutine(SetAttackFalse());
                        break;
                }
                
                
                
            }
            
        }
        else
        {
            IsAttack = false;
        }
        
    }

    private IEnumerator Attack(Collider2D enemy)
    {
        yield return new WaitForSeconds(1);
        enemy.GetComponent<Hunter>().TakeDamage(BulletColor);
        
    }
    private IEnumerator MakeBullet(Color BulletColor)
    {
        yield return new WaitForSeconds(0.6f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<SpriteRenderer>().color = BulletColor;
        bullet.SetActive(true);
        BulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (Direction)
        {
            BulletRigidbody.velocity = new Vector2(-15, 2);
        }
        else
        {
            BulletRigidbody.velocity = new Vector2(15, 2);
        }
        Destroy(bullet,2f);
        
    }

    private IEnumerator SetAttackFalse()
    {
        yield return new WaitForSeconds(1f);
        IsAttack = false;

    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        // Mermi ile çarpışma algılaması
        if (other.CompareTag("Target"))
        {
            // Renkli mermi objesi ile hedef objesi çarpıştığında yapılacak işlemler
            // ...
            // Örneğin, objeyi ve kendisini yok et
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }*/

    public int CurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }
    public bool isAttack()
    {
        return IsAttack;
    }

}
