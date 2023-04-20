using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector2 position, int dmg)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.getAsset.DamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(dmg);

        return damagePopup;
    }
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
    }
}
