using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageUI : MonoBehaviour {

    public Vector3 areaTopLeft=new Vector3 (600f,300f,1f);
    public Vector3 areaBottomRight = new Vector3(700f, 400f, 1f);
    public GameObject canvasObject;
    public Font font;
    private Text damageText;
    private float alpha;
	
	public IEnumerator Damage (int dam,float myx,float myy) {
        // ダメージ表示オブジェクト生成
        GameObject damage1TextObject = new GameObject();

        // Textをつける
        damage1TextObject.AddComponent<Text>();

        // canvas下に(canvasはフィールド宣言)
        damage1TextObject.transform.SetParent(canvasObject.transform, false);

        damage1TextObject.transform.position = new Vector3(
            myx,myy + 70f,0);

        // テキスト編集
        Text damage1Text = damage1TextObject.GetComponent<Text>();
        damage1Text.text = "" + dam;
        damage1Text.font = font; //fontはフィールドで。
        damage1Text.fontSize = 20;
        damage1Text.alignment = TextAnchor.MiddleCenter;


        // 上に消えていく
        damageText = damage1Text;
        alpha = 1f;
        for (int t = 1; t < 10; t++) {
            alpha = alpha - 0.1f;
            yield return new WaitForSeconds(0.1f);
            ValueChange(alpha);
            damage1TextObject.transform.position = new Vector3(
            damage1TextObject.transform.position.x, damage1TextObject.transform.position.y + 1f, 0);
        }
        yield return new WaitForSeconds(1f);
        // 用済み
        Destroy(damage1TextObject);

       
    }
    
    void ValueChange(float value)
    {
        damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, value);
    }
    
}
