using UnityEngine;

//シーンに配置するシングルトンクラス
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = default(T);
    protected virtual bool IsDontDestroyOnLoad => false; //シングルトンインスタンスをシーンをまたいで残すか

    public virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            if (IsDontDestroyOnLoad)
                DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static T Instance => _instance;
}
