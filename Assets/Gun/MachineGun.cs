using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Animator aimAnimator;
    public Transform muzzle;
    [SerializeField] private Transform bullet;
    private AnimatorStateInfo stateInfo;
    public int bulletSpeed = 10;

    public AudioClip gunshotSound;
    private AudioSource audioSource;

    public AmmCount ammo;
    public int maxammo;
    public int curammo;
    public float cooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        maxammo = 50;
        curammo = maxammo;
        ammo = (AmmCount)FindAnyObjectByType(typeof(AmmCount));

    }

    // Update is called once per frame
    void Update()
    {   
        //HandleGunEnable();
        if  (GlobalVariables.gunEnabled == 3)
        {
            HandleShooting();            
        };

        if (curammo <= 0)
        {
            ammo.colorRed();
        }
        else
        {
            ammo.colorBlack();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            ammo.colorBlack();
            StartCoroutine(reload());
        }

        ammo.updateCount(curammo, maxammo);
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(cooldown);
        curammo = maxammo;
    }

    private void HandleShooting(){
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && curammo > 0) {
            Vector3 mousePosition = GetMouseWorldPosition();
            aimAnimator.SetBool("shooting", true);  
            Fire();
        }else{
            aimAnimator.SetBool("shooting", false);
            //StopGunshotSound();
        }
    } 



    void Fire() {

        //GameController.Instance.GetComponent<AudioManager>().PlaySound("GunShot");
        SpawnBullet();
        PlayGunshotSound();
        curammo--;
    }


    void PlayGunshotSound() {
    if (gunshotSound != null && audioSource != null) {
        audioSource.PlayOneShot(gunshotSound);
    }
    }

    void StopGunshotSound() {
    if (audioSource != null) {
        audioSource.Stop();
    }
    } 



    void SpawnBullet() {

        /*if (bullet == null)
            return;*/

        //Transform shot = Instantiate(bullet, muzzle.position, muzzle.rotation);
        //shot.GetComponent<Bullet>().damage = damage;

        //Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);

        // Instantiate the bullet
        Transform bullet1 = Instantiate(bullet, muzzle.position, muzzle.rotation);

        // Calculate the direction
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Set the bullet's direction
        bullet1.GetComponent<Rigidbody2D>().velocity = aimDirection * bulletSpeed;

        return;

    }


    public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ() {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera){
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition,Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
