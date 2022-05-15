using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour
{
    public GameObject joyStick;

    // joystickCanvasのポジション
    private RectTransform joyStickRectTransform;

    // joystickのうしろの丸いやつ
    public GameObject backGround;

    // スティックの動ける範囲
    public  int stickRange = 3;

    // 実際に動く値
    private int stickMovement = 0;

    public static float joyStickPosX;
    public static float joyStickPosY;


    void Start()
    {
        Initialization();    
    }

    private void Initialization() // 初期設定
    {
        // 使用端末の画面の解消度に対応させる
        stickMovement = stickRange * (Screen.width + Screen.height) / 100;
        // Debug.Log(stickMovement);

        joyStickRectTransform = joyStick.GetComponent<RectTransform>();

        // joystickの非表示
        JoyStickDisplay(false);

    }

    private void JoyStickDisplay(bool x) // ジョイスティックの非表示
    {
        backGround.SetActive(x);
        joyStick.SetActive(x);
    }

    public void MoveJoyStick(BaseEventData data) // JoyStickの動き
    {
        PointerEventData pointer = data as PointerEventData;

        // Joystickと入力位置の差を格納
        // backgroundの位置と入力された位置の比較し、その差分をxに入れる
        float x = backGround.transform.position.x - pointer.position.x;
        float y = backGround.transform.position.y - pointer.position.y;

        // 単位円を求める
        float angle = Mathf.Atan2(y, x); // 斜辺の角度

        if(Vector2.Distance(backGround.transform.position, pointer.position) > stickMovement)
        {
            y = stickMovement * Mathf.Sin(angle);
            x = stickMovement * Mathf.Cos(angle);
        }

        // プレイヤーを動かす値を格納(マイナスは移動方向の調整。)
        joyStickPosX = -x / stickMovement;
        joyStickPosY = -y / stickMovement;

        joyStick.transform.position = new Vector2(backGround.transform.position.x - x,
            backGround.transform.position.y - y);
    }


    public void PointerDown(BaseEventData data) // 入力中に呼ぶ関数
    {
        // dataに格納されたPonterEventDataをpointerに入れる
        PointerEventData pointer = data as PointerEventData;

        JoyStickDisplay(true);

        // プレイヤーが押したところに表示(pointerにはタップした位置情報が入っている)
        backGround.transform.position = pointer.position;
    }

    public void PointerUp(BaseEventData data) // 指を離したとき
    {
        // joystickのポジション初期化関数
        PositionInitialization();

        JoyStickDisplay(false);
    }

    public void PositionInitialization() // JoyStickのPosition初期化
    {
        joyStickRectTransform.anchoredPosition = Vector2.down;

        // 指を離したらポジションを初期化
        joyStickPosX = 0;
        joyStickPosY = 0;

    }
    
}
