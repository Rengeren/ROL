using UnityEngine;
using System.Collections;

public class Cameramove : MonoBehaviour
{

    public GameObject player;
    public float mapleftlimit = -35;
    public float maprightlimit = 4044;
    public float mapunderlimit = 180;
    private float width = 1366f;
    private float height = 768f;
    // 画像のPixel Per Unit
    private float pixelPerUnit = 1f;
    private Camera cam;

    void Awake()
    {
        float aspect = (float)Screen.height / (float)Screen.width;
        float bgAcpect = height / width;

        // カメラコンポーネントを取得します
        cam = GetComponent<Camera>();
        // カメラのorthographicSizeを設定
        cam.orthographicSize = (height / 2f / pixelPerUnit);


        if (bgAcpect > aspect)
        {
            // 倍率
            float bgScale = height / Screen.height;
            // viewport rectの幅
            float camWidth = width / (Screen.width * bgScale);
            // viewportRectを設定
            cam.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
        }
        else
        {
            // 倍率
            float bgScale = width / Screen.width;
            // viewport rectの幅
            float camHeight = height / (Screen.height * bgScale);
            // viewportRectを設定
            cam.rect = new Rect(0f, (1f - camHeight) / 2f, 1f, camHeight);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x;
        newPosition.y = player.transform.position.y + 100f;
        newPosition.z = -30;


        if (newPosition.x <= mapleftlimit)
        {
            newPosition.x = mapleftlimit;

        }

        else if (newPosition.x >= maprightlimit)
        {
            newPosition.x = maprightlimit;

        }
        if (newPosition.y <= mapunderlimit)
        {
            newPosition.y = mapunderlimit;

        }

        transform.position = Vector3.Lerp(transform.position, newPosition, 5.0f * Time.deltaTime);

    }
}