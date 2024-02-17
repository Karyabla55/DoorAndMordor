using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] weapons; // Kullanılabilir silahlar
    private int currentWeaponIndex = 0;
    private bool IsAttack = false;
    private Rigidbody2D BulletRigidbody;
    private Rigidbody2D PlayerRigidbody;


    public GameObject bulletPrefab; // Mermi prefab'ı
    public Transform firePoint; // Ateşleme noktası
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public Color[] bulletColors; // Renkli mermilerin renkleri
    

    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Silah değiştirme mekanizması
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            ChangeWeapon(scroll);
        }

        // Silah ateşleme mekanizması
        if (Input.GetButtonDown("Fire1") && IsAttack !=true)
        {
            Attack();
            
        }
    }

    

    void ChangeWeapon(float scroll)
    {
         // Eski silahı devre dışı bırak
    //weapons[currentWeaponIndex].SetActive(false);
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

    // Yeni silahı etkinleştir ve SelectedWeapon tag'ini ata
    //weapons[currentWeaponIndex].SetActive(true);
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
                Color bulletColor = bulletColors[currentWeaponIndex];
                switch (currentWeaponIndex)
                {
                    case 0:
                        break; 
                    case 1:
                        // Mermi rengini seçili silaha göre belirle
                        
                        //saldırı animasyonu için süre
                        StartCoroutine(SetAttackFalse());
                        // Mermi oluşturma
                        StartCoroutine(MakeBullet(bulletColor));
                        break; 
                    case 2:
                        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, attackRange, enemyLayers);                      
                        foreach(Collider2D enemy in hitEnemies)
                        {
                            enemy.GetComponent<Hunter>().TakeDamage(bulletColor);
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


    private IEnumerator MakeBullet(Color BulletColor)
    {
        yield return new WaitForSeconds(0.6f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<SpriteRenderer>().color = BulletColor;
        bullet.SetActive(true);
        BulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (PlayerRigidbody.velocity.x > 0f)
        {
            firePoint.position = new Vector2(1.5f,1);
            BulletRigidbody.velocity = new Vector2(15, 2);
        }
        else if(PlayerRigidbody.velocity.x < 0f)
        {
            firePoint.position = new Vector2(-1.5f, 0);
            BulletRigidbody.velocity = new Vector2(-15, 2);
        }
        else { firePoint.position = new Vector2(0,0);}
        
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
