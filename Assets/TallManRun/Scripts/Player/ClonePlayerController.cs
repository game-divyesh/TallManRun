using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float movementSpeed = 4.25f;
    public float leftBoundary;
    public float rightBoundary;
    public float rotationLerpValue = 0.1f;
    public bool toSprint;

    [Space]
    public Rigidbody playerRB;

    [Space]
    [Header("Player Body")]
    public Transform lengthEffector;
    public Transform hipTransform;
    public Transform spineTransform;
    public GameObject head;

    [HideInInspector]
    public Animator _animator;
    private static readonly int RunningHash = Animator.StringToHash("ToRun");
    private static readonly int JumpingHash = Animator.StringToHash("ToJump");
    private static readonly int SkiddingHash = Animator.StringToHash("ToSkid");
    private static readonly int SprintHash = Animator.StringToHash("Sprint");
    private static readonly int FinalJumpHash = Animator.StringToHash("FinalJump");
    private static readonly int WinHash = Animator.StringToHash("FinalLanding");
    private static readonly int KickBossHash = Animator.StringToHash("KickBoss");

    public GameObject scalingBone;

    private RopeController _ropeController;
    private bool _toGrowTall;
    private bool _toShrink;
    public bool isAtSprintingPhase;

    public List<Transform> startingPositions;
    public List<Transform> endingPositions;
    public List<ClonePlayerCylinderGen> cylinderList;
    public GameObject spherePrefab;
    public List<Transform> spheres;
    public List<Transform> spherePositions;

    [Space]
    [Header("Materials")]
    [SerializeField] private List<Material> bodyMaterials;


    [HideInInspector]
    public CylinderGeneration _spineCylinder;

    public float minimumScaleValue = 0.01f;
    public float minimumPositionDifferenceValue = 1f;

    [Space]
    [Header("BodyScale")]
    private Vector3 lastCyliderScale, lastSphereScale, lastSpineCylinderScale, lastLengthTargetPos;


    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    private void Start()
    {
        OnNewMaterialSelected();

        movementSpeed = GameData.currentSpeed;

        _animator = GetComponent<Animator>();
        _ropeController = GetComponent<RopeController>();

        spheres = new List<Transform>();
        GenerateCylinders();

        _spineCylinder = scalingBone.GetComponent<CylinderGeneration>();
        print(_spineCylinder);

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



    private void FixedUpdate()
    {
        if (_toGrowTall)
            UpdateRopeControllerHeight();
        if (_toShrink)
            UpdateRopeControllerHeight();

    }


    
    public void MakeThePlayerTall(int factor)
    {
        MoveTheTorsoUp(factor);
        _toGrowTall = true;
    }

    public void MakePlayerShort(int factor)
    {
        MoveTheTorsoDown(factor);
        _toGrowTall = false;
        _toShrink = true;
    }

    public void BuffThePlayer(int factor)
    {
        var buffValue = factor * 0.1f;
        print(buffValue);
        for (var i = 0; i < startingPositions.Count; i++)
        {
            var cylinderInitialScale = cylinderList[i].transform.localScale;
            cylinderList[i].gameObject.transform.DOScale(cylinderInitialScale + new Vector3(buffValue, buffValue, 0), 2f);

        }
        for (var i = 0; i < spheres.Count; i++)
        {
            var sphereInitialScale = spheres[i].transform.localScale;
            spheres[i].transform.DOScale(sphereInitialScale + Vector3.one * buffValue, 2f);
        }
        var spineCylinder = _spineCylinder.transform.localScale;
        _spineCylinder.transform.DOScale(new Vector3(buffValue, buffValue, 0) + spineCylinder, 2f);
    }

    public void DeBuffThePlayer(int factor)
    {

        var buffValue = factor * 0.1f;
        var spineCylinder = _spineCylinder.transform.localScale;
        print(spineCylinder);
        if (MinimumScaleThresholdHasCrossed(spineCylinder, buffValue))
        {
            print("GameOver");
            return;
        }

        _spineCylinder.transform.DOScale(spineCylinder - new Vector3(buffValue, buffValue, 0), 0.7f);

        for (var i = 0; i < cylinderList.Count; i++)
        {
            var cylinderInitialScale = cylinderList[i].transform.localScale;
            cylinderList[i].transform.DOScale(cylinderInitialScale - new Vector3(buffValue, buffValue, 0), 0.7f);
        }

        for (var i = 0; i < spherePositions.Count; i++)
        {
            var sphereInitialScale = spheres[i].transform.localScale;
            spheres[i].transform.DOScale(sphereInitialScale - Vector3.one * buffValue, 1f);
        }
    }

    public void ShrinkThePlayer(int factor)
    {
        DeBuffThePlayer(factor);
        MakePlayerShort(factor);
    }

    private void MoveTheTorsoUp(int factor)
    {
        var buffValue = factor * 0.05f;
        lengthEffector.transform.DOMoveY(lengthEffector.position.y + buffValue, 1f).SetEase(Ease.OutExpo);
        _toGrowTall = false;
    }

    private void MoveTheTorsoDown(int factor)
    {
        var buffValue = factor * 0.05f;
        if (MinimumHeightThresholdHasCrossed(lengthEffector.transform.position, buffValue))
        {
            print("---- MinimumHeightThresholdHasCrossed ---- ");
            return;
        }
        else
            lengthEffector.transform.DOMoveY(lengthEffector.position.y - buffValue, 0.7f).SetEase(Ease.OutExpo);
        _toShrink = false;
    }
    private bool MinimumHeightThresholdHasCrossed(Vector3 lengthEffectorPosition, float variablePositionValue)
    {
        var yPos = lengthEffectorPosition.y - variablePositionValue;
        return yPos <= minimumPositionDifferenceValue;
    }


    private bool MinimumScaleThresholdHasCrossed(Vector3 spineCylinder, float variableScaleValue)
    {
        var scale = spineCylinder.x - variableScaleValue;
        print(scale);

        var x = scale <= minimumScaleValue;
        if (x)
        {
            GameManager.Instance.isGameEnd = true;
            isAtSprintingPhase = false;
            DisappearAllTheCylinders();
            head.transform.parent = null;
            head.GetComponent<Rigidbody>().isKinematic = false;
            head.GetComponent<Rigidbody>().AddForce(0, 2, 5, ForceMode.Impulse);

            head.SetActive(true);
            return x;
        }
        else
        {
            print(scale <= minimumScaleValue);
            return x;
        }
    }

    private void UpdateRopeControllerHeight()
    {
        _ropeController.UpdateRope(hipTransform.transform, spineTransform.transform, _spineCylinder, "ClonePlayer");
    }

    private void GenerateCylinders()
    {
        var maxIterations = startingPositions.Count;
        for (var i = 0; i < maxIterations; i++)
        {
            _ropeController.UpdateRope(startingPositions[i], endingPositions[i], cylinderList[i], "ClonePlayer");

            UpdatePlayerBodyMaterial(cylinderList[i].gameObject);

        }

        foreach (var position in spherePositions)
        {
            var sphere = Instantiate(spherePrefab, position);
            spheres.Add(sphere.transform);

            UpdatePlayerBodyMaterial(sphere.transform.GetChild(0).gameObject);

        }

    }


    #region ------------------ ANIMATION'S METHOD ------------------

    public void PlaySlidingAnimation()
    {
        _animator.SetBool(SkiddingHash, true);
    }

    public void StopSlidingAnimation()
    {
        _animator.SetBool(SkiddingHash, false);
    }


    public void PlayJumpAnimation()
    {
        _animator.SetBool(JumpingHash, true);
    }


    public void StopJumpAnimation()
    {
        _animator.SetBool(JumpingHash, false);
    }

    public void PlaySprintAnimation()
    {
        _animator.SetBool(SprintHash, true);
        movementSpeed = 5f;
    }

    public void PlayKickAnimation()
    {
        _animator.SetTrigger(KickBossHash);
    }

    public void PlayWinAnimation()
    {
        _animator.SetTrigger(WinHash);
    }

    #endregion



    public void DisappearAllTheCylinders()
    {
        var maxIterations = startingPositions.Count;
        for (var i = 0; i < maxIterations; i++) cylinderList[i].gameObject.SetActive(false);

        foreach (var sphere in spheres) sphere.gameObject.SetActive(false);
    }


    private void OnNewMaterialSelected()
    {
        UpdatePlayerBodyMaterial(head);

        // Update All Cylinder
        foreach (var cylinder in cylinderList)
        {
            UpdatePlayerBodyMaterial(cylinder.gameObject);
        }
        // UPDATE JOINTS
        foreach (var joint in spheres)
        {
            UpdatePlayerBodyMaterial(joint.transform.GetChild(0).gameObject);
        }
    }

    private void UpdatePlayerBodyMaterial(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = bodyMaterials[GameData.PlayerMaterialColor];
    }


    private void OnNewHeadWearSelected()
    {
        for (int index = 0; index < head.transform.childCount; index++)
        {
            if (index == GameData.PlayerHeadWear)
            {
                head.transform.GetChild(index).gameObject.SetActive(true);
            }
            else
            {
                head.transform.GetChild(index).gameObject.SetActive(false);
            }
        }
    }


    



}// Class
