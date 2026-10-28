using UnityEngine;

//浮遊する用の動作を行う機能コンポーネント
public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float _circle = 1.0f;         //周期運動の周期
    [SerializeField] private float _radius = 1.0f;         //周期運動の半径
    [SerializeField] private int _initialDegree = 0;       //初期位相(度)
    private bool _canMove;                                 //falseなら静止
    private float _initialY;                               //初期位置のy座標
    private float _initialRadian;                          //初期位相(ラジアン) 

    private void Awake()
    {
        _canMove = false;
        _initialY = transform.position.y;
        _initialRadian = 2.0f * Mathf.PI * (float)_initialDegree / 360.0f;
    }


    private void Update()
    {
        if (_canMove)
            Floating(); //状態フラグがONの間だけ上下運動
    }

    //運動の有効/無効を制御
    public void SetMoving(bool flag)
    {
        _canMove = flag;
    }

    //上下に周期運動を行う
    public void Floating()
    {
        float radian = _initialRadian + 2.0f * Mathf.PI * Time.time/_circle;  //現在の位相
        float y_move = _radius * Mathf.Sin(radian);                          //動く距離を計算
        transform.position = new Vector2(transform.position.x, _initialY + y_move); //オブジェクト座標に反映
    }

    //初期位相をランダムに返す(ばねごとに動作をばらけさせるため)
    private float RamdomDegree()
    {
        return Random.Range(0.0f, 2.0f * Mathf.PI);
    }
}
