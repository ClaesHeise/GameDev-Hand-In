using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private GameObject PickUp;
    GameObject HitObject;

    SwitchCameraTarget CameraSwitcher;
    bool hasItem;
    bool isSmall;
    bool hitPlank;
    Vector3 normalScale;
    Vector3 smallScale;

    [SerializeField]
    private GameObject hiddenChest;

    public Text textElement;
    public Text textElement2;

    private int keys = 0;
    int bonfires = 0;
    bool key;

    private MovementInput moveInp;

    public Movement movement;

    void Awake()
    {
        moveInp = new MovementInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        hasItem = false;
        isSmall = false;
        hitPlank = false;
        key = false;
        textElement.text = "No keys";
        textElement2.text = "";
        normalScale = gameObject.transform.localScale;
        smallScale = normalScale * 0.2f;
        CameraSwitcher = GetComponent<SwitchCameraTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 5f))
        {
            if (
                hit.collider.tag == "PickUp"
                || hit.collider.tag == "Sizeify" && hasItem == false && PickUp == null
            )
            {
                if (hit.collider.tag == "PickUp" || hit.collider.tag == "Plank")
                {
                    textElement2.text = "e) Pick up";
                }
                else
                {
                    textElement2.text = "e) Sizeify";
                }
                HitObject = hit.transform.gameObject;
            }
            else if (hit.collider.tag == "Chest")
            {
                textElement2.text = "e) Loot chest";
                HitObject = hit.transform.gameObject;
            }
            else if (hit.collider.tag == "Door")
            {
                if (key)
                {
                    textElement2.text = "Open door";
                }
                else
                {
                    textElement2.text = "You don't have the key for the door";
                }
                HitObject = hit.transform.gameObject;
            }
            else if (hit.collider.tag == "Bonfire")
            {
                if (hit.collider.GetComponentsInChildren<ParticleSystem>()[0].isPlaying == false)
                {
                    textElement2.text = "Light fire";
                    HitObject = hit.transform.gameObject;
                }
            }
            else if (hit.collider.tag == "Ship")
            {
                textElement2.text = "Board ship";
                HitObject = hit.transform.gameObject;
            }
        }
            else if (HitObject != null && PickUp == null && hitPlank == false)
            {
                HitObject = null;
                textElement2.text = "";
                return;
            }

            if (moveInp.Interaction.Use.triggered && HitObject != null && PickUp == null)
            {
                InteractionE();
            }
            if (hasItem == true && PickUp != null)
            {
                textElement2.text = "q) Drop item\nu) Lift item\ni) Lower item";
                if (moveInp.Interaction.Drop.triggered)
                {
                    InteractionQ();
                }
                else
                {
                    float heightDirection = moveInp.Interaction.Height.ReadValue<float>();
                    PickUp.transform.position += new Vector3(0, 0.2f * heightDirection, 0);
                }
            }
        }

        void InteractionQ()
        {
            PickUp.GetComponent<BoxCollider>().isTrigger = false;
            PickUp.GetComponent<Rigidbody>().isKinematic = false;
            PickUp.transform.parent = null;
            hasItem = false;
            PickUp = null;
            textElement2.text = "";
        }
    
    

    void InteractionE()
    {
        if (HitObject.tag == "PickUp" || HitObject.tag == "Plank")
        {
            PickUp = HitObject;
            PickUp.GetComponent<Rigidbody>().isKinematic = true;
            PickUp.GetComponent<BoxCollider>().isTrigger = true;
            PickUp.transform.position =
                gameObject.transform.position
                + (
                    gameObject.transform.forward
                    + new Vector3(
                        gameObject.transform.forward.x * 5,
                        HitObject.GetComponent<Renderer>().bounds.size.y,
                        gameObject.transform.forward.z * 5
                    )
                );
            PickUp.transform.parent = gameObject.transform;
            hasItem = true;
        }
        else if (HitObject.tag == "Sizeify")
        {
            if (gameObject.transform.localScale == normalScale)
            {
                gameObject.transform.localScale = smallScale;
                movement.speed *= 0.5f;
            }
            else
            {
                gameObject.transform.localScale = normalScale;
                movement.speed *= 2.0f;
            }
        }
        else if (HitObject.tag == "Chest")
        {
            keys++;
            if (keys > 2)
            {
                key = true;
                textElement.text = "1 key";
            }
            else
            {
                textElement.text = keys + " Key fragments";
            }
            HitObject.SetActive(false);
        }
        else if (HitObject.tag == "Door" && key == true)
        {
            HitObject.SetActive(false);
            key = false;
            keys = 0;
            textElement.text = keys + " Key fragments";
        }
        else if (HitObject.tag == "Bonfire")
        {
            HitObject.GetComponentsInChildren<ParticleSystem>()[0].Play();
            bonfires++;
            if (bonfires > 2)
            {
                hiddenChest.SetActive(true);
            }
        }
        else if (HitObject.tag == "Ship")
        {
            HitObject.GetComponent<Ship>().Undock();
        }
    }

    void OnEnable()
    {
        moveInp.Interaction.Enable();
    }

    void OnDisable()
    {
        moveInp.Interaction.Disable();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Plank"){
            textElement2.text = "e) Pick up";
            HitObject = other.transform.gameObject;
        }    
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Plank"){
            HitObject = null;
            textElement2.text = "";
        }
    }
}
