using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int dmg)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.getAsset.DamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(dmg);

        return damagePopup;
    }
    private TextMeshPro textMesh;
    private static float disappearTime;
    private Color txtcolor;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
        txtcolor = textMesh.color;
        disappearTime = 0.2f;
    }

    private void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            txtcolor.a -= 3f * Time.deltaTime;
            textMesh.color = txtcolor;
            if (txtcolor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
