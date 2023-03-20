using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private GameObject PickUp;
    GameObject HitObject;
    bool hasItem;
    bool isSmall;
    Vector3 normalScale;
    Vector3 smallScale;

    public Text textElement;
    public Text textElement2;

    private int keys = 0;
    // Start is called before the first frame update
    void Start()
    {
        hasItem = false;
        isSmall = false;
        textElement.text = "No keys";
        textElement2.text = "";
        normalScale = gameObject.transform.localScale;
        smallScale = normalScale * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 1.5f)){
            if(hit.collider.tag == "PickUp" || hit.collider.tag == "Sizeify" && hasItem == false && PickUp == null){
                print("hittin an object");
                if(hit.collider.tag == "PickUp"){
                    textElement2.text = "e) Pick up";
                }
                else{
                    textElement2.text = "e) Sizeify";
                }
                HitObject = hit.transform.gameObject;
            }
            else if(hit.collider.tag == "Chest"){
                textElement2.text = "e) Loot chest";
                HitObject = hit.transform.gameObject;
            }
        }
        else if (HitObject != null && PickUp == null){
            HitObject = null;
            textElement2.text = "";
            return;
        }
        if(Input.GetKeyDown("e") && HitObject != null && PickUp == null){
            if(HitObject.tag == "PickUp"){
                PickUp = HitObject;
                PickUp.GetComponent<Rigidbody>().isKinematic = true;
                PickUp.GetComponent<BoxCollider>().isTrigger = true;
                PickUp.transform.position = gameObject.transform.position + (gameObject.transform.forward + new Vector3(0, HitObject.GetComponent<Renderer>().bounds.size.y, 0));
                PickUp.transform.parent = gameObject.transform;
                hasItem = true;
            }
            if (HitObject.tag == "Sizeify") {
                if(gameObject.transform.localScale == normalScale){
                    gameObject.transform.localScale = smallScale;
                    // Add increased movement speed, probably
                    // MoveBehaviour.walkSpeed *= 5f;
                    // MoveBehaviour.sprintSpeed *= 5f;

                }
                else {
                    gameObject.transform.localScale = normalScale;
                    // Add decreased movement speed, probably
                    // MoveBehaviour.walkSpeed *= 0.2f;
                    // MoveBehaviour.sprintSpeed *= 0.2f;
                }
            }
            if(HitObject.tag == "Chest"){
                keys++;
                textElement.text = keys+" Key fragments";
                HitObject.SetActive(false);
            }
        }
        if(hasItem == true && PickUp != null){
            textElement2.text = "q) Drop item\nu) Lift item\ni) Lower item";
            if(Input.GetKeyDown("q")){
            PickUp.GetComponent<BoxCollider>().isTrigger = false;
            PickUp.GetComponent<Rigidbody>().isKinematic = false;
            PickUp.transform.parent = null;
            hasItem = false;
            PickUp = null;
            }
            else if(Input.GetKeyDown("u")){
                PickUp.transform.position += new Vector3(0,0.2f,0);
            }
            else if(Input.GetKeyDown("i")){
                PickUp.transform.position -= new Vector3(0,0.2f,0);
            }
        }
    }
}
