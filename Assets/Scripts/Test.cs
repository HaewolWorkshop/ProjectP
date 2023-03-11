using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            var dataBase = Global.I.dataManager.GetDataBase<TestGameDataBase>();
            var data = dataBase.GetData(1);

            Debug.Log(data.Name);
            Debug.Log(data.Desc);
            Debug.Log(data.Value);
            Debug.Log(data.Bool);
            Debug.Log(data.Enum);
        }
    }
}
