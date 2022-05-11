using System;
using System.Collections.Generic;

[Serializable]
public class BingsMapsJsonClass
{
    public List<resourceSets> resourceSets;
}
[Serializable]
public class resourceSets
{
    public List<resources> resources;
}
[Serializable]
public class resources
{
    public List<routeLegs> routeLegs;
}
[Serializable]
public class routeLegs
{
    public List<itineraryItems> itineraryItems;
}
[Serializable]
public class itineraryItems
{
    public string compassDirection;
    public List<details> details;
    public string exit;
    public string iconType;
    public instruction instruction;
    public maneuverPoint maneuverPoint;
    public string sideOfStreet;
    public string tollZone;
    public string transitTerminus;
    public float travelDistance;
    public int travelDuration;
    public string travelMode;
}
[Serializable]
public class details
{
    public int compassDegrees;
    public int[] endPathIndices;
    public string maneuverType;
    public string mode;
    public string roadType;
    public int[] startPathIndice;
    public List<string> names;
}

[Serializable]
public class instruction
{
    public string formattedText;
    public string maneuverType;
    public string text;
}
[Serializable]
public class maneuverPoint
{
    public string type;
    public float[] coordinates;
}