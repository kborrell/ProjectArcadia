/*************************************************************
 *       Unity Singleton Class (c) by ClockStone 2011        *
 * 
 * Allows to use a script components like a singleton
 * 
 * Usage:
 * 
 * Derive your script class MyScriptClass from 
 * SingletonMonoBehaviour<MyScriptClass>
 * 
 * Access the script component using the static function
 * MyScriptClass.Instance
 * 
 * use the static function SetSingletonAutoCreate( GameObject )
 * to specify a GameObject - containing the MyScriptClass component -  
 * that should be instantiated in case an instance is requested and 
 * and no objects exists with the MyScriptClass component.
 * 
 * ***********************************************************/

using System;
using UnityEngine;

#pragma warning disable 1591 // undocumented XML code warning

public class UnitySingleton<T>
    //where T : SingletonMonoBehaviour<T>
    where T : MonoBehaviour
{

#if UNITY_FLASH
    static UnityEngine.Object _instance;
#else
    static T _instance;
    
#endif

    static internal Type _myType = typeof( T ); // not working for Flash builds. Requires SetSingletonType() for correct Flash support

    static private  int _GlobalInstanceCount = 0;
    static private bool _awakeSingletonCalled = false;

	static readonly string autocreateRooName = "AutoSingletons";

	static Transform autoCreateRoot;
	static Transform AutocreateRoot
	{
		get
		{
			if (autoCreateRoot == null)
			{
				GameObject result = GameObject.Find(autocreateRooName);

				if (result == null)
				{
					result = new GameObject(autocreateRooName);
					GameObject.DontDestroyOnLoad(result);
				}

				autoCreateRoot = result.transform;
			}

			return autoCreateRoot;
		}
	}


    static public T GetSingleton( bool autoCreate )
    {
        if ( !_instance ) // Unity operator to check if object was destroyed, 
        {
            T component = null;

#if UNITY_FLASH
            if( _myType == null )
            {
                CustomDebug.LogError( "Flash builds require SetSingletonType() to be called!" );
                return null;
            }
#endif 
            var components = GameObject.FindObjectsOfType<T>();

            foreach ( var c in components )
            {
                //var cpt = ( (Component)c ).GetComponent( _myType );
                var singletonCpt = (ISingletonMonoBehaviour)( c );
                if ( singletonCpt.isSingletonObject )
                {
                    component = c;
                    break;
                }
            }

            if ( !component )
            {
                if ( autoCreate
#if UNITY_EDITOR
				    && Application.isPlaying
#endif
				    )
                {
					GameObject go = new GameObject(_myType.Name, new Type[] { _myType });
					GameObject.DontDestroyOnLoad(go);

					go.transform.parent = AutocreateRoot;

                    Debug.LogWarning("Create singleton : " + _myType);

					component = go.GetComponent<T>();
					if ( component == null )
					{
                        Debug.LogError( "Auto created object does not have component " + _myType.Name );
                        return null;
                    }
                }
                else
                {                
                    return null;
                }
            }
            else
            {
                _AwakeSingleton( component as T );
            }

#if UNITY_FLASH
            _instance = component;
#else
            _instance = (T) component;
#endif
        }

        return (T)_instance;
    }

    private UnitySingleton( )
    { }

#if UNITY_FLASH
    static internal void _Awake( UnityEngine.Object instance )
#else
    static internal void _Awake( T instance )
#endif
    {
        _GlobalInstanceCount++;
        if ( _GlobalInstanceCount > 1 )
        {
            Debug.LogError( "More than one instance of SingletonMonoBehaviour " + typeof( T ).Name );
        } else 
            _instance = instance;

        _AwakeSingleton( instance as T);
    }

    static internal void _Destroy()
    {
        if ( _GlobalInstanceCount > 0 )
        {
            _GlobalInstanceCount--;
            if ( _GlobalInstanceCount == 0 )
            {
                _awakeSingletonCalled = false;
                _instance = null;
            }
        }
        else
        {
            // can happen if an exception occurred that prevented correct instance counting 
            // CustomDebug.LogWarning( "_GlobalInstanceCount < 0" );
        }
    }

    static private void _AwakeSingleton( T instance )
    {
        if ( !_awakeSingletonCalled )
        {
            _awakeSingletonCalled = true;
            instance.SendMessage( "AwakeSingleton", SendMessageOptions.DontRequireReceiver );
        }
    }
}


interface ISingletonMonoBehaviour
{
    bool isSingletonObject { get; }
}

/// <summary>
/// Provides singleton-like access to a unique instance of a MonoBehaviour. <para/>
/// </summary>
/// <example>
/// Derive your own class from SingletonMonoBehaviour. <para/>
/// <code>
/// public class MyScriptClass : SingletonMonoBehaviour&lt;MyScriptClass&gt;
/// {
///     public MyScriptClass()
///     {
///         MyScriptClass.SetSingletonType( typeof( MyScriptClass ) ); // workaround for Flash
///     }
///     public void MyFunction() { }
///     protected override void Awake()
///     {
///         base.Awake();
///     }
///     void AwakeSingleton()
///     {
///         // all initialisation code here. Will get called from Awake() by singleton.
///         // Can get called before Awake() if an instance is accessed in an Awake() function which
///         // was called earlier
///     }
/// }
/// </code>
/// <para/>
/// access the instance by writing
/// <code>
/// MyScriptClass.Instance.MyFunction();
/// </code>
/// </example>
/// <typeparam name="T">Your singleton MonoBehaviour</typeparam>
/// <remarks>
/// Makes sure that an instance is available from other Awake() calls even before the singleton's Awake()
/// was called. ( Requires AwakeSingleton() !)
/// </remarks>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingletonMonoBehaviour
        //where T : SingletonMonoBehaviour<T>
        where T : MonoBehaviour
{

    /// <summary>
    /// Gets the singleton instance.
    /// </summary>
    /// <returns>
    /// A reference to the instance if it exists, otherwise <c>null</c>
    /// </returns>
    /// <remarks>
    /// Outputs an error to the debug log if no instance was found.
    /// </remarks>
    static public T Instance
    { get { return UnitySingleton<T>.GetSingleton( true ); } }

    /// <summary>
    /// Checks if an instance of this MonoBehaviour exists.
    /// </summary>
    /// <returns>
    /// A reference to the instance if it exists, otherwise <c>null</c>
    /// </returns>
    static public T InstanceIfExist
    {
		get { return UnitySingleton<T>.GetSingleton( false ); }
    }

    /// <summary>
    /// Activates the singleton instance.
    /// </summary>
    /// <remarks>
    /// Call this function if you set an singleton object inactive before ever accessing the <c>Instance</c>. This is 
    /// required because Unity does not (yet) offer a way to find inactive game objects.
    /// </remarks>
    static public void ActivateSingletonInstance() // 
    {
        UnitySingleton<T>.GetSingleton( true );
    }

    /// <summary>
    /// Only required for Flash builds. If this function is not called by the class deriving from 
    /// SingletonMonoBehaviour in the constructor the singleton can not be found by GetSingleton(...)
    /// </summary>
    /// <param name="type"></param>
    static public void SetSingletonType( Type type )
    {
        UnitySingleton<T>._myType = type;
    }

    protected virtual void Awake() // should be called in derived class
    {
        if ( isSingletonObject )
        {
#if UNITY_FLASH
            UnitySingleton<T>._Awake( this );
#else
            UnitySingleton<T>._Awake( this as T );
#endif
            //Debug.Log( "Awake: " + this.GetType().Name );
        }
    }

    protected virtual void OnDestroy()  // should be called in derived class
    {
        if ( isSingletonObject )
        {
            UnitySingleton<T>._Destroy();
        }
    }

    /// <summary>
    /// must return true if this instance of the object is the singleton. Can be used to allow multiple objects of this type
    /// that are "add-ons" to the singleton.
    /// </summary>
    public virtual bool isSingletonObject
    {
        get
        {
            return true;
        }
    }
}



