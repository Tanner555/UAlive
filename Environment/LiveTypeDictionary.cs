using Ludiq;
using UnityEditor;
using System.Collections.Generic;
using Ludiq.OdinSerializer;
using System;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class LiveTypeDictionary : LudiqScriptableObject
    {
        public static LiveTypeDictionary instance;
        
        [Serialize][Inspectable][InspectorWide][InspectorLabel(null)]
        public Dictionary<string, DictionaryAsset> categories = new Dictionary<string, DictionaryAsset>();
    }
}
