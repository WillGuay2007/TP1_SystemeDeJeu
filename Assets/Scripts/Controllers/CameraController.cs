using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_sensitivity;
    [SerializeField] private Transform m_pivotPoint;
    [SerializeField] private float m_initialCameraDistance;
    private Vector3 m_localRot;
    private float m_cameraDistanceOffset;
    private float m_sprintingOffset = 0f;

    private PlayerInputController m_playerInputController;

    public void SetDependencies(GameController gameController)
    {
        m_playerInputController = gameController.playerInputController;
    }

    public void Init()
    {
        m_playerInputController.OnSprintInputChanged += sprinting => m_sprintingOffset = sprinting ? -0.5f : 0f; ;
    }

    public void InternalStart()
    {

    }

    void Update()
    {
        m_cameraDistanceOffset += Input.GetAxis("Mouse ScrollWheel");
        m_cameraDistanceOffset = Mathf.Clamp(m_cameraDistanceOffset, -1f, 1f);
        m_pivotPoint.transform.position = m_Player.transform.position + Vector3.up;
        transform.position = m_pivotPoint.position - m_pivotPoint.forward * (m_initialCameraDistance - m_cameraDistanceOffset - m_sprintingOffset);
        transform.LookAt(m_pivotPoint.position);
        if (!Input.GetMouseButton(1)) return;
        m_localRot.x += Input.GetAxis("Mouse X") * m_sensitivity;
        m_localRot.y += Input.GetAxis("Mouse Y") * m_sensitivity;
        m_localRot.y = Mathf.Clamp(m_localRot.y, -89, 89);
        m_Player.transform.rotation = Quaternion.Euler(0f, m_localRot.x, 0f);


        Quaternion QT = Quaternion.Euler(-m_localRot.y, m_localRot.x, 0f);
        m_pivotPoint.rotation = QT;
    }
}
