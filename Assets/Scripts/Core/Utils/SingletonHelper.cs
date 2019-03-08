using System;
using System.Linq;
using UnityEngine;

namespace Unoper.Unity.Utils {

    public static class SingletonHelper{

        public static GameObject Find<T>(T singletonTag)
             where T : struct, IConvertible {
            return UnityEngine.Object
                .FindObjectsOfType<SingletonGameObject<T>>()
                .Where(s => s.SingletonTag.Equals(singletonTag))
                .Where(s => s.IsSingleton)
                .First()
                .gameObject;
        }
    }
}
