using UnityEngine;

[RequireComponent (typeof(FloatingObject))]
public class score : MonoBehaviour
{
    private ScorePanel panel;           //スコア表示パネル
    private FloatingObject _boltMove;   //ねじの浮遊運動制御

    private void Start()
    {
        panel = FindFirstObjectByType<ScorePanel>();
        _boltMove = GetComponent<FloatingObject>();
        _boltMove.SetMoving(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            panel.addscore(1);
            Destroy(gameObject);
        }
            
    }
}
