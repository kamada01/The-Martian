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
        if  (GlobalVariables.gunEnabled == 3)
        {
            HandleShooting();            
        };
        
    }


    private void HandleShooting(){
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
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
