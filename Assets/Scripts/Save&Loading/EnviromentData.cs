using System.Collections.Generic;

[System.Serializable]

public class EnviromentData
{
    public List<string> pickedupItems;

    public EnviromentData(List<string> _pickedupItems)
    {
        pickedupItems = _pickedupItems;
    }
}
