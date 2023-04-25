using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : MonoBehaviour
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
        if  (GlobalVariables.gunEnabled == 4)
        {
            HandleShooting();            
        };
        
    }


    private void HandleShooting(){
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = GetMouseWorldPosition();
            aimAnimator.SetBool("shooting", true);  
            Fire();
        }else{
            stateInfo = aimAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1 && !aimAnimator.IsInTransition(0))
            {
                aimAnimator.SetBool("shooting", false);
                
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

    // Instantiate the bullets
    Transform bullet1 = Instantiate(bullet, muzzle.position, muzzle.rotation);
    Transform bullet2 = Instantiate(bullet, muzzle.position, muzzle.rotation);
    Transform bullet3 = Instantiate(bullet, muzzle.position, muzzle.rotation);

    // Calculate the direction
    Vector3 mousePosition = GetMouseWorldPosition();
    Vector3 aimDirection = (mousePosition - transform.position).normalized;

    // Create the spread angles for bullets 1 and 3
    float spreadAngle = 10f; // You can adjust this value to increase or decrease the spread
    Quaternion spreadQuaternion = Quaternion.Euler(0, 0, spreadAngle);
    Quaternion negativeSpreadQuaternion = Quaternion.Euler(0, 0, -spreadAngle);

    // Calculate the directions for bullets 1 and 3
    Vector3 spreadDirection1 = spreadQuaternion * aimDirection;
    Vector3 spreadDirection3 = negativeSpreadQuaternion * aimDirection;

    // Set the bullets' directions
    bullet1.GetComponent<Rigidbody2D>().velocity = spreadDirection1 * bulletSpeed;
    bullet2.GetComponent<Rigidbody2D>().velocity = aimDirection * bulletSpeed;
    bullet3.GetComponent<Rigidbody2D>().velocity = spreadDirection3 * bulletSpeed;

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
