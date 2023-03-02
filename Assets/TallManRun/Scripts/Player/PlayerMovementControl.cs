using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementControl : GenericSingletonClass<PlayerMovementControl>
{
    [Header("Player Movement")]
    public float movementSpeed = 4.25f;
    public float xSpeed;
    public float xForce;
    public float yForce;
    public float leftBoundary;
    public float rightBoundary;
    public float rotationLerpValue = 0.1f;
    public bool toSprint;

    [Space]
    public Rigidbody playerRB;

    //private float _swipeSpeed = 5f;

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
    public List<CylinderGeneration> cylinderList;
    public GameObject spherePrefab;
    public List<Transform> spheres;
    public List<Transform> spherePositions;

    [Space]
    [Header("Materials")]
    [SerializeField] private List<Material> bodyMaterials;
    /*public Material cylinderMaterial;
    public Color cylinderPositiveColor;
    public Color cylinderNegativeColor;

    public Color cylinderDefaultColor;*/

    [HideInInspector]
    public CylinderGeneration _spineCylinder;

    public float minimumScaleValue = 0.01f;
    public float minimumPositionDifferenceValue = 1f;

    [Space]
    [Header("BodyScale")]
    private Vector3 lastCyliderScale, lastSphereScale, lastSpineCylinderScale, lastLengthTargetPos;


    [Space]
    [Header("Partical Effects")]
    public ParticleSystem gatePassEffect;

    [Space]
    public float bounceMultiplier = 0;


    [Space]
    private float _rotationVelocity;

    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    public static Sequence ExistingSequence;

    public static event Action OnGameOver;
    public static event Action OnGameWin;



    public override void Awake()
    {
        base.Awake();
        DOTween.SetTweensCapacity(1000, 30);
        Input.simulateMouseWithTouches = true;
    }

    private void Start()
    {
        //cylinderMaterial = bodyMaterials[GameData.PlayerMaterialColor];
        OnNewMaterialSelected();

        movementSpeed = GameData.currentSpeed;
        //lengthEffector.position = new Vector3(lengthEffector.position.x,lengthEffector.position.y + 1.1f, lengthEffector.position.z);

        _animator = GetComponent<Animator>();
        _ropeController = GetComponent<RopeController>();

        spheres = new List<Transform>();
        GenerateCylinders();

        _spineCylinder = scalingBone.GetComponent<CylinderGeneration>();
        print(_spineCylinder);
        //cylinderMaterial.color = cylinderDefaultColor;

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
        if (GameManager.Instance.isGameStart)
        {
            //if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            
            xForce = Input.GetMouseButton(0) ? Input.GetAxis("Mouse X") * xSpeed : 0;
            yForce = Input.GetMouseButton(0) ? Input.GetAxis("Mouse Y") * xSpeed : 0;

            PlayerMovement();
        }
        if (_toGrowTall)
            UpdateRopeControllerHeight();
        if (_toShrink)
            UpdateRopeControllerHeight();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BounceCounting"))
        {
            bounceMultiplier += 0.2f;
        }
    }


    private void PlayerMovement()
    {
        if (GameManager.Instance.isGameEnd)
        {
            _animator.SetBool(RunningHash, false);
            _animator.SetBool(SprintHash, false);
            return;
        }

        if (toSprint)
        {
            _animator.SetBool(SprintHash, true);
            transform.Translate(
                (Vector3.forward * movementSpeed + new Vector3(xForce * xSpeed, 0f, 0f))
                * Time.deltaTime, Space.World);
        }
        else if (Input.GetMouseButton(0))
        {
            _animator.SetBool(RunningHash, true);
            transform.Translate(
                (Vector3.forward * movementSpeed + new Vector3(xForce * xSpeed, 0f, 0f))
                * Time.deltaTime, Space.World);
        }
        else
        {
            _animator.SetBool(RunningHash, false);
            _animator.SetBool(SprintHash, false);
        }

        if (transform.position.x < leftBoundary)
            transform.position = new Vector3(leftBoundary, transform.position.y, transform.position.z);
        else if (transform.position.x > rightBoundary)
            transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);

        if (xForce < 0f)
            //rotate anticlockwise
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), rotationLerpValue * Time.deltaTime);
        else if (xForce > 0f)
            //rotate clockwise
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), rotationLerpValue * Time.deltaTime);
        else
            //face forward
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationLerpValue * Time.deltaTime);

    }


    private void CityPlayerMovement()
    {

        if (GameManager.Instance.isGameEnd)
        {
            _animator.SetBool(RunningHash, false);
            _animator.SetBool(SprintHash, false);
            return;
        }

        else if (Input.GetMouseButton(0))
        {
            _animator.SetBool(RunningHash, true);
            transform.Translate((new Vector3(xForce * xSpeed, 0f, yForce * xSpeed)) * Time.deltaTime, Space.World);

        }
        else
        {
            _animator.SetBool(RunningHash, false);
            _animator.SetBool(SprintHash, false);
        }
        

        Vector3 inputDirection = new Vector3(xForce, 0.0f, yForce).normalized;

        var _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

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
        //ChangeToPositiveColor();
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
        //_spineCylinder.transform.DOScale(new Vector3(buffValue, buffValue, 0) + spineCylinder, 2f).OnComplete(ChangeToDefaultColor);
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
            // if(_lastScalingCylinderTweens[i].IsActive()) _lastScalingCylinderTweens[i].Kill();
            // _lastScalingCylinderTweens[i] = 
            cylinderList[i].transform.DOScale(cylinderInitialScale - new Vector3(buffValue, buffValue, 0), 0.7f);
        }

        for (var i = 0; i < spherePositions.Count; i++)
        {
            var sphereInitialScale = spheres[i].transform.localScale;
            // if(_lastScalingSphereTweens[i].IsActive()) _lastScalingSphereTweens[i].Kill();
            // _lastScalingSphereTweens[i] = 
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
        //ChangeToPositiveColor();
        var buffValue = factor * 0.05f;
        lengthEffector.transform.DOMoveY(lengthEffector.position.y + buffValue, 1f).SetEase(Ease.OutExpo);
        _toGrowTall = false;
        //Invoke(nameof(ChangeToDefaultColor), 0.5f);
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

    private void UpdateRopeControllerHeight()
    {
        _ropeController.UpdateRope(hipTransform.transform, spineTransform.transform, _spineCylinder);
    }

    private void GenerateCylinders()
    {
        var maxIterations = startingPositions.Count;
        for (var i = 0; i < maxIterations; i++)
        {
            _ropeController.UpdateRope(startingPositions[i], endingPositions[i], cylinderList[i]);

            UpdatePlayerBodyMaterial(cylinderList[i].gameObject);

        }

        foreach (var position in spherePositions)
        {
            var sphere = Instantiate(spherePrefab, position);
            spheres.Add(sphere.transform);

            UpdatePlayerBodyMaterial(sphere.transform.GetChild(0).gameObject);

        }

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
            OnGameOver?.Invoke();
            ResetPlayerInput();
            DisappearAllTheCylinders();
            head.transform.parent = null;
            head.GetComponent<Rigidbody>().isKinematic = false;
            head.GetComponent<Rigidbody>().AddForce(0, 2, 5, ForceMode.Impulse);

            head.SetActive(true);
            if (!isAtSprintingPhase)
            {
                GameManager.Instance.ShowLoseScreen();
            }
            /*			else if (isAtSprintingPhase)
                        {
                            GameManager.Instance.ShowWinScreen();

                        }*/
            return x;
        }
        else
        {
            print(scale <= minimumScaleValue);
            return x;
        }
    }

    private bool MinimumHeightThresholdHasCrossed(Vector3 lengthEffectorPosition, float variablePositionValue)
    {
        var yPos = lengthEffectorPosition.y - variablePositionValue;
        return yPos <= minimumPositionDifferenceValue;
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
        xSpeed = 0f;
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


    public void RestrictPlayerInput()
    {
        xSpeed = 0f;
        movementSpeed = 0f;
    }

    public void ResetPlayerInput()
    {
        xSpeed = 1.75f;
        movementSpeed = GameData.currentSpeed;
    }

    public void DisappearAllTheCylinders()
    {
        var maxIterations = startingPositions.Count;
        for (var i = 0; i < maxIterations; i++) cylinderList[i].gameObject.SetActive(false);

        foreach (var sphere in spheres) sphere.gameObject.SetActive(false);
    }

    public void ExecuteJump(Vector3 jumpTargetPosition)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.finalJumpSound);
        Tween tween = transform.DOJump(jumpTargetPosition, 5f, 1, 3f)
            .OnComplete(() =>
            {
                toSprint = false;
                RestrictPlayerInput();
                transform.DOMove(new Vector3(transform.position.x, 0f, transform.position.z), 1f).OnComplete(
                    () =>
                    {
                        PlayWinAnimation();
                        GameManager.Instance.ShowWinScreen();

                        OnGameWin?.Invoke();
                    });
            });

        DOTween.Kill(tween);
    }

    /*public void ChangeToPositiveColor()
    {
        cylinderMaterial.color = cylinderPositiveColor;
    }

    public void ChangeToDefaultColor()
    {
        cylinderMaterial.color = cylinderDefaultColor;
    }*/


    public void ResetPlayer(Transform playerDefaultPos)
    {

        PlayerMovementControl.Instance.transform.position = playerDefaultPos.position;
        PlayerMovementControl.Instance.transform.rotation = playerDefaultPos.rotation;

        PlayerMovementControl.Instance.MakeThePlayerTall(5 * GameData.currentHeightLevel);
        PlayerMovementControl.Instance.BuffThePlayer(GameData.bodyIncrementFactor * GameData.currentWidthLevel);

        PlayerMovementControl.Instance._animator.Rebind();

        PlayerMovementControl.Instance.ResetPlayerInput();
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


    public void OnGameStartSavePlayerBodyData()
    {
        lastCyliderScale = cylinderList[0].transform.localScale;
        lastSphereScale = spheres[0].transform.localScale;
        lastSpineCylinderScale = _spineCylinder.transform.localScale;
        lastLengthTargetPos = lengthEffector.transform.position;
    }

    public void ResetPlayerBody()
    {
        DOTween.KillAll();
        for (var i = 0; i < startingPositions.Count; i++)
        {
            cylinderList[i].gameObject.transform.localScale = lastCyliderScale;
        }
        for (var i = 0; i < spheres.Count; i++)
        {
            spheres[i].transform.localScale = lastSphereScale;
        }
        _spineCylinder.transform.localScale = lastSpineCylinderScale;

        lengthEffector.transform.position = lastLengthTargetPos;
    }


    public void BounceLevelComplete()
    {
        RestrictPlayerInput();
        PlayWinAnimation();
        GameManager.Instance.ShowWinScreen();
        OnGameWin?.Invoke();
    }

}// CLASS
