using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserGun : MonoBehaviour
{
    public Animator aimAnimator;
    public Transform muzzle;
    [SerializeField] private Transform bullet;
    private AnimatorStateInfo stateInfo;
    public int bulletSpeed = 10;

    public AudioClip gunshotSound;
    private AudioSource audioSource;

    public static int damage = 6;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
  
    }

    // Update is called once per frame
    void Update()
    {   
        //HandleGunEnable();
        if  (GlobalVariables.gunEnabled == 2)
        {
            HandleShooting();
        };
        
    }


    private void HandleShooting(){
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = GetMouseWorldPosition();
            aimAnimator.SetBool("Shoot", true);

            Fire();
        }
        //aimAnimator.SetBool("Shoot", false);
        else{
            stateInfo = aimAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1 && !aimAnimator.IsInTransition(0))
            {
                aimAnimator.SetBool("Shoot", false);
                
            }
        }
    }

    void Fire() {

        //GameController.Instance.GetComponent<AudioManager>().PlaySound("GunShot");
        SpawnBullet();
        PlayGunshotSound();

    }


    void PlayGunshotSound() {
    if (gunshotSound != null && audioSource != null) {
        audioSource.PlayOneShot(gunshotSound);
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
