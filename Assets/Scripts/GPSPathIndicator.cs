using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GPSPathIndicator : MonoBehaviour
{
    [SerializeField] Sprite straight, right, left, bearRight, bearLeft, keepRight, keepLeft, stayRight, stayLeft, rightThenLeft, leftThenRight, rightThenRight, leftThenLeft, enterExitRoundabout, roadNameChange, depart, arrive, unknownManeuver;
    [SerializeField] Image nextManeuver, secondManeuver, thirdManeuver;
    [SerializeField] TextMeshProUGUI nextManeuverDistance, secondManeuverDistance, thirdManeuverDistance;
    Sprite nextManeuverSprite, secondManeuverSprite, thirdManeuverSprite;

    private void Update()
    {
        nextManeuverSprite = PlayerPrefs.GetString("Maneuver1") switch
        {
            "KeepStraight" => straight,
            "KeepRight" => keepRight,
            "KeepLeft" => keepLeft,
            "KeepToStayRight" => stayRight,
            "KeepToStayLeft" => stayLeft,
            "BearRight" => bearRight,
            "BearLeft" => bearLeft,
            "TurnRight" => right,
            "TurnLeft" => left,
            "TurnRightThenTurnLeft" => rightThenLeft,
            "TurnLeftThenTurnRight" => leftThenRight,
            "TurnRightThenTurnRight" => rightThenRight,
            "TurnLeftThenTurnLeft" => rightThenRight,
            "ArriveFinish" => arrive,
            "DepartStart" => depart,
            "EnterThenExitRoundabout" => enterExitRoundabout,
            "RoadNameChange" => roadNameChange,
            _ => unknownManeuver,
        };

        secondManeuverSprite = PlayerPrefs.GetString("Maneuver2") switch
        {
            "KeepStraight" => straight,
            "KeepRight" => keepRight,
            "KeepLeft" => keepLeft,
            "KeepToStayRight" => stayRight,
            "KeepToStayLeft" => stayLeft,
            "BearRight" => bearRight,
            "BearLeft" => bearLeft,
            "TurnRight" => right,
            "TurnLeft" => left,
            "TurnRightThenTurnLeft" => rightThenLeft,
            "TurnLeftThenTurnRight" => leftThenRight,
            "TurnRightThenTurnRight" => rightThenRight,
            "TurnLeftThenTurnLeft" => rightThenRight,
            "ArriveFinish" => arrive,
            "DepartStart" => depart,
            "EnterThenExitRoundabout" => enterExitRoundabout,
            "RoadNameChange" => roadNameChange,
            _ => unknownManeuver,
        };

        thirdManeuverSprite = PlayerPrefs.GetString("Maneuver3") switch
        {
            "KeepStraight" => straight,
            "KeepRight" => keepRight,
            "KeepLeft" => keepLeft,
            "KeepToStayRight" => stayRight,
            "KeepToStayLeft" => stayLeft,
            "BearRight" => bearRight,
            "BearLeft" => bearLeft,
            "TurnRight" => right,
            "TurnLeft" => left,
            "TurnRightThenTurnLeft" => rightThenLeft,
            "TurnLeftThenTurnRight" => leftThenRight,
            "TurnRightThenTurnRight" => rightThenRight,
            "TurnLeftThenTurnLeft" => rightThenRight,
            "ArriveFinish" => arrive,
            "DepartStart" => depart,
            "EnterThenExitRoundabout" => enterExitRoundabout,
            "RoadNameChange" => roadNameChange,
            _ => unknownManeuver,
        };

        nextManeuver.sprite = nextManeuverSprite;
        secondManeuver.sprite = secondManeuverSprite;
        thirdManeuver.sprite = thirdManeuverSprite;
        nextManeuverDistance.text = PlayerPrefs.GetFloat("Distance1") > 1 ? Mathf.RoundToInt(PlayerPrefs.GetFloat("Distance1")) + "km" : PlayerPrefs.GetFloat("Distance1") * 1000 + "m";
        secondManeuverDistance.text = PlayerPrefs.GetFloat("Distance2") > 1 ? Mathf.RoundToInt(PlayerPrefs.GetFloat("Distance2")) + "km" : PlayerPrefs.GetFloat("Distance2") * 1000 + "m";
        thirdManeuverDistance.text = PlayerPrefs.GetFloat("Distance3") > 1 ? Mathf.RoundToInt(PlayerPrefs.GetFloat("Distance3")) + "km" : PlayerPrefs.GetFloat("Distance3") * 1000 + "m";
    }
}
