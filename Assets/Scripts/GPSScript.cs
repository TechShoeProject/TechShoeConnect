using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.UI;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif
using TMPro;

public class GPSScript : MonoBehaviour
{
    [SerializeField] List<string> instructions, maneuvre, names;
    [SerializeField] List<int> degrees, durations;
    [SerializeField] List<float> distances;
    int timeInDay;
    public float Latitude = 48.780931f, Longitude = 2.260204f;
    public string Adresse;
    string bingMapsKey = "Al21Qu3r-IGPQvohMdo6S9Tjs6lDGTZSiblAAJbXkXNmcZPYlkNJx5E5JW8xEODA";
    GameObject holder;

    public void StartGeolocate()
    {
        instructions = new List<string>();
        maneuvre = new List<string>();
        degrees = new List<int>();
        distances = new List<float>();
        durations = new List<int>();
        names = new List<string>();
        Adresse = PlayerPrefs.GetString("Adresse", "");
        StartCoroutine(Locate(true));
    }

    private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("LaunchGPS") == 1)
        {
            PlayerPrefs.SetInt("LaunchGPS", 0);
            StartGeolocate();
        }
        if (PlayerPrefs.GetInt("GPSRequestTime") != 0 && timeInDay - PlayerPrefs.GetInt("GPSRequestTime") < 1)
        {
            int maneuverData = PlayerPrefs.GetString("Maneuver1") switch
            {
                "KeepStraight" => 0,
                "TurnRight" => 1,
                "TurnLeft" => 2,
                "KeepRight" => 3,
                "KeepLeft" => 4,
                "KeepToStayRight" => 5,
                "KeepToStayLeft" => 6,
                "BearRight" => 7,
                "BearLeft" => 8,
                "TurnRightThenTurnLeft" => 9,
                "TurnLeftThenTurnRight" => 10,
                "TurnRightThenTurnRight" => 1,
                "TurnLeftThenTurnLeft" => 2,
                "EnterThenExitRoundabout" => 11,
                _ => -1,
            };
            if (maneuverData != -1)
            {
                holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
                holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
                holder.GetComponent<BluetoothWriterScript>().DataToSend.Add(0);
                holder.GetComponent<BluetoothWriterScript>().DataToSend.Add((byte)maneuverData);
            }
        }
        timeInDay = System.DateTime.Now.Hour * 3600 + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Second;
        if (PlayerPrefs.GetInt("GPSRequestTime") != 0 && (timeInDay - PlayerPrefs.GetInt("GPSRequestTime")  > 10 || timeInDay - PlayerPrefs.GetInt("GPSRequestTime")  > PlayerPrefs.GetInt("Duration1") / 2))
        {
            PlayerPrefs.SetInt("GPSRequestTime", timeInDay);
            //Adresse = PlayerPrefs.GetString("NextAdressesNames34") == "NON0" ? (PlayerPrefs.GetString("NextAdressesNames33") == "NON0" ? (PlayerPrefs.GetString("NextAdressesNames32") == "NON0" ?
            //    (PlayerPrefs.GetString("NextAdressesNames31") == "NON0" ? (PlayerPrefs.GetString("NextAdressesNames30") == "NON0" ? (PlayerPrefs.GetString("NextAdressesNames29") == "NON0" ? PlayerPrefs.GetString("Adresse")
            //    : PlayerPrefs.GetString("NextAdressesNames29")) : PlayerPrefs.GetString("NextAdressesNames30")) : PlayerPrefs.GetString("NextAdressesNames31")) : PlayerPrefs.GetString("NextAdressesNames32")) : 
            //    PlayerPrefs.GetString("NextAdressesNames33")) : PlayerPrefs.GetString("NextAdressesNames34");
            instructions = new List<string>();
            maneuvre = new List<string>();
            degrees = new List<int>();
            distances = new List<float>();
            durations = new List<int>();
            names = new List<string>();
            Adresse = PlayerPrefs.GetString("Adresse", "");
            StartCoroutine(Locate(false));
        }
    }

    private void Start()
    {
#if PLATFORM_ANDROID && !UNITY_EDITOR
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            Permission.RequestUserPermission(Permission.FineLocation);
#endif

    }
    public void addToInstructions(string instruction, int list)
    {
        switch (list)
        {
            case 0:
                instructions.Add(instruction);
                break;
            case 1:
                maneuvre.Add(instruction);
                break;
            case 2:
                degrees.Add(int.Parse(instruction));
                break;
            case 3:
                distances.Add(float.Parse(instruction));
                break;
            case 4:
                durations.Add(int.Parse(instruction));
                break;
            case 5:
                names.Add(instruction);
                break;
        }
    }

    //IEnumerator GetFullAdressName(string name, int index1)
    //{
    //    string url = "https://dev.virtualearth.net/REST/v1/Locations?&query=" + name + ",%20France&key=" + bingMapsKey;
    //    int index = url.IndexOf(" ");
    //    while (index != -1)
    //    {
    //        url = url.Substring(0, index) + "%20" + url.Substring(index + 1);
    //        index = url.IndexOf(" ");
    //    }
    //    using (UnityWebRequest www = UnityWebRequest.Get(url))
    //    {
    //        yield return www.SendWebRequest();
    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(Adresse + " : " + url);
    //            Debug.Log(www.result);
    //        }
    //        else
    //        {
    //            string jsonstring = www.downloadHandler.text;
    //            BingsMapsJsonClass data = JsonUtility.FromJson<BingsMapsJsonClass>(jsonstring);
    //            PlayerPrefs.SetString("NextAdressesNames" + (index1 + 1).ToString(), data.resourceSets[0].resources[0].address.formattedAddress);
    //        }
    //    }
    //}

    IEnumerator Locate(bool full)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed || !Input.location.isEnabledByUser)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
#endif
        string url = "http://dev.virtualearth.net/REST/V1/Routes/Walking?wp.0=" + Latitude.ToString() + "," + Longitude.ToString() + "&wp.1=" + Adresse + "%E2%80%8B&key=" + bingMapsKey;
        int index = url.IndexOf(" ");
        while (index != -1)
        {
            url = url.Substring(0, index) + "%20" + url.Substring(index + 1);
            index = url.IndexOf(" ");
        }
        //PythonRunner.RunFile($"{Application.dataPath}/Python/Test BingAPI.py");
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(Adresse + " : " + url);
                Debug.Log(www.result);
            }
            else
            {
                string jsonstring = www.downloadHandler.text;
                BingsMapsJsonClass data = JsonUtility.FromJson<BingsMapsJsonClass>(jsonstring);
                foreach (itineraryItems item in data.resourceSets[0].resources[0].routeLegs[0].itineraryItems)
                {
                    addToInstructions(item.instruction.text, 0);
                    addToInstructions(item.instruction.maneuverType, 1);
                    if (item.details[0].compassDegrees >= 0)
                        addToInstructions(item.details[0].compassDegrees.ToString(), 2);
                    else
                        addToInstructions("-1", 2);
                    addToInstructions(item.travelDistance.ToString(), 3);
                    addToInstructions(item.travelDuration.ToString(), 4);
                    if (item.details[0].names.Count != 0)
                        addToInstructions(item.details[0].names[0], 5);
                    else
                        addToInstructions("NON0", 5);
                }

                if (maneuvre.Count >= 0)
                    PlayerPrefs.SetString("Maneuver1", maneuvre[0]);
                if (maneuvre.Count >= 1)
                    PlayerPrefs.SetString("Maneuver2", maneuvre[1]);
                if (maneuvre.Count >= 2)
                    PlayerPrefs.SetString("Maneuver3", maneuvre[2]);
                if (maneuvre.Count >= 3)
                    PlayerPrefs.SetString("Maneuver4", maneuvre[3]);
                if (distances.Count >= 0)
                    PlayerPrefs.SetFloat("Distance1", distances[0]);
                if (distances.Count >= 1)
                    PlayerPrefs.SetFloat("Distance2", distances[1]);
                if (distances.Count >= 2)
                    PlayerPrefs.SetFloat("Distance3", distances[2]);
                if (distances.Count >= 3)
                    PlayerPrefs.SetFloat("Distance4", distances[3]);
                if (durations.Count >= 0)
                    PlayerPrefs.SetInt("Duration1", durations[0]);
                if (durations.Count >= 1)
                    PlayerPrefs.SetInt("Duration2", durations[1]);
                if (durations.Count >= 2)
                    PlayerPrefs.SetInt("Duration3", durations[2]);
                if (durations.Count >= 3)
                    PlayerPrefs.SetInt("Duration4", durations[3]);
                //if (names.Count >= 20)
                //    if (names[19] != "NON0")
                //        StartCoroutine(GetFullAdressName(names[19], 29));
                //if (names.Count >= 21)
                //    if (names[20] != "NON0")
                //        StartCoroutine(GetFullAdressName(names[20], 30));
                //if (names.Count >= 22)
                //    if (names[21] != "NON0")
                //        StartCoroutine(GetFullAdressName(names[21], 31));
                //if (names.Count >= 23)
                //    if (names[22] != "NON0")
                //        StartCoroutine(GetFullAdressName(names[22], 32));
                //if (names.Count >= 24)
                //    if (names[23] != "NON0")
                //        StartCoroutine(GetFullAdressName(names[23], 33));
                //if (names.Count >= 25)
                //    if (names[24] != "NON0")
                //        StartCoroutine(GetFullAdressName(names[24], 34));
            }
        }
        Input.location.Stop();

        PlayerPrefs.SetInt("GPSRequestTime", timeInDay);
        if (full)
        {

        }
        else
        {
            StartGeolocate();
        }
        Debug.Log("GPS exécuté");
    }
}