using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerClickMove : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacterScript;
    CameraRaycaster cameraRaycaster;
    Vector3 click_walk_pos, ClickPoint;

    public float walk_move_stop_raius = 0.5f;
    public float attack_move_stop_raius = 3f;

    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacterScript = GetComponent<ThirdPersonCharacter>();
        click_walk_pos = transform.position;
    }

    void ClickMove()
    {
        var move_target_pos = click_walk_pos - transform.position;

        if(move_target_pos.magnitude >= walk_move_stop_raius)
        {
            thirdPersonCharacterScript.Move(move_target_pos, false, false);
        }
        else
        {
            thirdPersonCharacterScript.Move(Vector3.zero, false, false);
        }
    }

    //void FixedUpdate()
    //{
    //    if(Input.GetMouseButton(0))
    //    {
    //        ClickPoint = cameraRaycaster.getHitInfo.point;

    //        switch (cameraRaycaster.getHitLayer)
    //        {
    //            case Layer_Enum.Walkable:
    //                click_walk_pos = Shorten_ClickPoint(ClickPoint, walk_move_stop_raius);
    //                break;
    //            case Layer_Enum.Enemy:
    //                click_walk_pos = Shorten_ClickPoint(ClickPoint, attack_move_stop_raius);
    //                // Attack
    //                break;
    //        }
    //    }
    //    ClickMove();
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, click_walk_pos);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(click_walk_pos, ClickPoint);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ClickPoint, 0.15f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(click_walk_pos, 0.2f);
    }


    /// *** To find the distance towards enemy....
    Vector3 Shorten_ClickPoint(Vector3 walk_point_destination, float player_move_stop_raius)
    {
        Vector3 reduce_click_walk_pos = (walk_point_destination - transform.position).normalized * player_move_stop_raius;
        return walk_point_destination - reduce_click_walk_pos;
    }
}
