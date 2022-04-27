using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    public static T current;
    
    public virtual void OnEnable() {

        if(current == null) {
            current = this as T;
        }else{
            Debug.Log( $"Duplicates of {typeof(T)} destroyed." );
            Destroy( this );
        }

    }

}