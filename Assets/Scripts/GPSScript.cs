using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using TMPro;

public class PythonManager : MonoBehaviour
{
    [SerializeField] List<string> instructions, maneuvre;
    [SerializeField] List<int> degrees;
    [SerializeField] List<float> distances;
    public float Latitude = 48.780931f, Longitude = 2.260204f;
    public string Adresse;
    string maneuver, bingMapsKey = "Al21Qu3r-IGPQvohMdo6S9Tjs6lDGTZSiblAAJbXkXNmcZPYlkNJx5E5JW8xEODA";

    public void StartGeolocate()
    {
        instructions = new List<string>();
        maneuvre = new List<string>();
        degrees = new List<int>();
        maneuver = "";
        Adresse = PlayerPrefs.GetString("Adresse", "");
        StartCoroutine(Locate());
    }
    private void Start()
    {
#if PLATFORM_ANDROID
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
        }
    }

    IEnumerator Locate()
    {
        while (true)
        {
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("OOF");
                //yield break;
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
                //yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed || !Input.location.isEnabledByUser)
            {
                Debug.Log("Unable to determine device location");
                //yield break;
            }
            else
            {
                Latitude = Input.location.lastData.latitude;
                Longitude = Input.location.lastData.longitude;
                // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
                Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }
            Latitude = 48.780931f;
            Longitude = 2.260204f;
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
                }
            }
            Input.location.Stop();

            foreach (string x in instructions)
            {
                maneuver = maneuver + x + "\n";
            }
            PlayerPrefs.SetString("Maneuver1", maneuvre[0]);
            PlayerPrefs.SetString("Maneuver2", maneuvre[1]);
            PlayerPrefs.SetString("Maneuver3", maneuvre[2]);
            PlayerPrefs.SetFloat("Distance1", distances[0]);
            PlayerPrefs.SetFloat("Distance2", distances[1]);
            PlayerPrefs.SetFloat("Distance3", distances[2]);
            yield return new WaitForSeconds(10);
        }
    }
}