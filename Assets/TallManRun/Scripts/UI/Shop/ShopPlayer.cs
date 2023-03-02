using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopPlayer : MonoBehaviour
{

    public GameObject s_head;
    public List<GameObject> body = new List<GameObject>();


    [Space]
    [SerializeField] private List<Material> s_bodyMaterials;

    private void Start()
    {
        UpdatePlayerBodyMaterial(s_head);
        UpdateAllMaterial();

        transform.DOLocalRotate(new Vector3(0, 360, 0), 4f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }


    private void OnEnable()
    {
        ShopBodyItem.OnNewMaterialSelected += OnNewMaterialSelected;
        ShopHeadItem.OnNewHeadWearSelected += OnNewHeadWearSelected;
    }

    private void OnDisable()
    {
        ShopBodyItem.OnNewMaterialSelected -= OnNewMaterialSelected;
        ShopHeadItem.OnNewHeadWearSelected -= OnNewHeadWearSelected;
    }

    private void UpdateAllMaterial()
    {
        for (int index = 0; index < body.Count; index++)
        {
            UpdatePlayerBodyMaterial(body[index]);
        }
    }


    private void UpdatePlayerBodyMaterial(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = s_bodyMaterials[GameData.PlayerMaterialColor];
    }

    private void OnNewMaterialSelected()
    {
        UpdatePlayerBodyMaterial(s_head);

        UpdateAllMaterial();
    }

    private void OnNewHeadWearSelected()
    {
        for (int index = 0; index < s_head.transform.childCount; index++)
        {
            if (index == GameData.PlayerHeadWear)
            {
                s_head.transform.GetChild(index).gameObject.SetActive(true);
            }
            else
            {
                s_head.transform.GetChild(index).gameObject.SetActive(false);
            }
        }
    }









}// CLASS
