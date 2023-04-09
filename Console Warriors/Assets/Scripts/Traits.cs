using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
using UnityEngine;
using TMPro;
[Serializable]
public class Traits
{
    [SerializeField]
    public enum traits
    {
        human,
        undead,
        skeleton,
        saint,
    }
}
