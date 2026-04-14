using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private CraftableController m_craftableController;
    [SerializeField] private DialogueController m_dialogueController;
    [SerializeField] private ExperienceController m_experienceController;
    [SerializeField] private HealthController m_healthController;
    [SerializeField] private HUDController m_hudController;
    [SerializeField] private InventoryController m_inventoryController;
    [SerializeField] private ItemController m_itemController;
    [SerializeField] private NpcController m_npcController;
    [SerializeField] private HungerController m_hungerController;
    [SerializeField] private PlayerInputController m_playerInputController;
    [SerializeField] private SlotController m_slotController;


    public CameraController cameraController => m_cameraController;
    public CraftableController craftableController => m_craftableController;
    public DialogueController dialogueController => m_dialogueController;
    public ExperienceController experienceController => m_experienceController;
    public HealthController healthController => m_healthController;
    public HUDController hudController => m_hudController;
    public InventoryController inventoryController => m_inventoryController;
    public ItemController itemController => m_itemController;
    public NpcController npcController => m_npcController;
    public HungerController hungerController => m_hungerController;
    public PlayerInputController playerInputController => m_playerInputController;
    public SlotController slotController => m_slotController;

    private void Start()
    {
        SetControllerDependencies();
        InitControllers();
        InternalStartControllers();
    }

    //J'appelle les 3 étapes de chacuns des controllers meme si certains sont pas implémentés car dans le future, il pourrait y'avoir des changements

    private void SetControllerDependencies()
    {
        cameraController.SetDependencies(this);
        craftableController.SetDependencies(this);
        dialogueController.SetDependencies(this);
        experienceController.SetDependencies(this);
        healthController.SetDependencies(this);
        hudController.SetDependencies(this);
        inventoryController.SetDependencies(this);
        itemController.SetDependencies(this);
        npcController.SetDependencies(this);
        hungerController.SetDependencies(this);
        playerInputController.SetDependencies(this);
        slotController.SetDependencies(this);
    }

    private void InitControllers()
    {
        cameraController.Init();
        craftableController.Init();
        dialogueController.Init();
        experienceController.Init();
        healthController.Init();
        hudController.Init();
        inventoryController.Init();
        itemController.Init();
        npcController.Init();
        hungerController.Init();
        playerInputController.Init();
        slotController.Init();
    }

    private void InternalStartControllers()
    {
        cameraController.InternalStart();
        craftableController.InternalStart();
        dialogueController.InternalStart();
        experienceController.InternalStart();
        healthController.InternalStart();
        hudController.InternalStart();
        inventoryController.InternalStart();
        itemController.InternalStart();
        npcController.InternalStart();
        hungerController.InternalStart();
        playerInputController.InternalStart();
        slotController.InternalStart();
    }

}
