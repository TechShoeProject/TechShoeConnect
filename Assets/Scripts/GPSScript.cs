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

    private void Update()
    {
        if(PlayerPrefs.GetInt("LaunchGPS") == 1)
        {
            PlayerPrefs.SetInt("LaunchGPS", 0);
            StartGeolocate();
        }
        if (PlayerPrefs.GetInt("GPSRequestTime") != 0 && timeInDay - PlayerPrefs.GetInt("GPSRequestTime") < 1)
        {
            //Demande de vibration
        }
        timeInDay = System.DateTime.Now.Hour * 3600 + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Second;
        if (PlayerPrefs.GetInt("GPSRequestTime") != 0 && (timeInDay - PlayerPrefs.GetInt("GPSRequestTime")  > 10 || timeInDay - PlayerPrefs.GetInt("GPSRequestTime")  > 2 * PlayerPrefs.GetInt("Duration1")))
        {
            PlayerPrefs.SetInt("GPSRequestTime", timeInDay);
            Adresse = PlayerPrefs.GetString("NextAdressesNames5");
            instructions = new List<string>();
            maneuvre = new List<string>();
            degrees = new List<int>();
            distances = new List<float>();
            durations = new List<int>();
            names = new List<string>();
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

    IEnumerator Locate(bool full)
    {
        Debug.Log("Start");
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
        Debug.Log("O : " + url);
        //PythonRunner.RunFile($"{Application.dataPath}/Python/Test BingAPI.py");
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.result);
            else
            {

            }
            Debug.Log("MOMOMOM");
            string jsonstring = www.downloadHandler.text;
            Debug.Log(jsonstring);
            BingsMapsJsonClass data = JsonUtility.FromJson<BingsMapsJsonClass>(jsonstring);
            Debug.Log(data.resourceSets);
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
        }
        Input.location.Stop();

        PlayerPrefs.SetString("Maneuver1", maneuvre[0]);
        PlayerPrefs.SetString("Maneuver2", maneuvre[1]);
        PlayerPrefs.SetString("Maneuver3", maneuvre[2]);
        PlayerPrefs.SetString("Maneuver4", maneuvre[3]);
        PlayerPrefs.SetFloat("Distance1", distances[0]);
        PlayerPrefs.SetFloat("Distance2", distances[1]);
        PlayerPrefs.SetFloat("Distance3", distances[2]);
        PlayerPrefs.SetFloat("Distance4", distances[3]);
        PlayerPrefs.SetInt("Duration1", durations[0]);
        PlayerPrefs.SetInt("Duration2", durations[1]);
        PlayerPrefs.SetInt("Duration3", durations[2]);
        PlayerPrefs.SetInt("Duration4", durations[3]);
        PlayerPrefs.SetString("NextAdressesNames1", names[0]);
        PlayerPrefs.SetString("NextAdressesNames2", names[1]);
        PlayerPrefs.SetString("NextAdressesNames3", names[2]);
        PlayerPrefs.SetString("NextAdressesNames4", names[3]);
        PlayerPrefs.SetString("NextAdressesNames5", names[4]);

        PlayerPrefs.SetInt("GPSRequestTime", timeInDay);
        if (full)
        {

        }
        else
        {
            StartGeolocate();
        }
    }
}