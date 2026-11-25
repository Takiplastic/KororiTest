using UnityEngine;

public interface IParser
{
    string FromDataToString(int data);
    int FromStringToData(string str);
}
