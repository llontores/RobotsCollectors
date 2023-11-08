using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private void OnTriggerStay(Collider collider){
        if(collider.TryGetComponent(out Ore ore)){
            print(ore.gameObject.transform.position);
        }
    }
}
