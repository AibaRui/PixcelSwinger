using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveManagerBase : MonoBehaviour
{

    public abstract bool Save();

    public abstract bool Load();

}
