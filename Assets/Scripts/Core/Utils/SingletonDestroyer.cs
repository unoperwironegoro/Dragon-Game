using System;
using System.Linq;
using UnityEngine;

namespace Unoper.Unity.Utils
{
    public abstract class SingletonDestroyer<T> : MonoBehaviour
        where T : struct, IConvertible
    {
        [SerializeField] private T[] TagsToDestroy;

	    private void Start () 
        {
            var singletonsToDestroy = FindObjectsOfType<SingletonGameObject<T>>()
                .Where(s => TagsToDestroy.Contains(s.SingletonTag));
            
            foreach (var singleton in singletonsToDestroy)
            {
                Destroy(singleton);
            }
	    }

    }
}
