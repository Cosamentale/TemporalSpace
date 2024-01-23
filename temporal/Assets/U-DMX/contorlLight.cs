using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using neoludicGames.uDmx;
using OscSimpl.Examples;
public class contorlLight : MonoBehaviour
{
    public DmxLightSourceTemporal l1;
   // public DmxLightSource l2;
    //public DmxLightSource l3;
    public PoseEstimator script;
    public float dis;
    public float dis2;
    private Vector3 velocity = Vector3.zero;
    public Vector3 col1;
    private Vector3 velocity2 = Vector3.zero;
    public Vector3 col2;
    public float smoothTime = 0.0f;
    public float si;
    public float si2;
    public float si3;
    public float si4;
    public float si5;
    public GettingStartedReceiving osc;
    void Start()
    {

    }
    float sm(float edge0, float edge1, float x){
        float t = Mathf.Clamp01((x - edge0) / (edge1 - edge0));
        return t* t * (3.0f - 2.0f * t);}
float dist(float a, float b) { return Mathf.Pow(1-Mathf.Abs(a- sm(si4, si5, b)),5); }
    float distance(float a, float b) { return  Mathf.Abs(a -b); }

    float fract(float t) { return t - Mathf.Floor(t); }
    Vector3 fract3(Vector3 t)
    {
        return t - new Vector3(Mathf.Floor(t.x), Mathf.Floor(t.y), Mathf.Floor(t.z));
    }
    float rd(float x) { float fx = Mathf.Floor(x); return fract(Mathf.Sin(Vector2.Dot(new Vector2(fx, fx), new Vector2(54.56f, 54.56f))) * 7845.236f); }
    float no(float x) { return sm(0.3f,0.7f,Mathf.Lerp(rd(x), rd(x + 1), Mathf.SmoothStep(0, 1, fract(x)))); }
    float map(float value, float min1, float max1, float min2, float max2)
    {
        return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
    }
    Vector3 clamp3( Vector3 x) { return new Vector3(Mathf.Clamp01(x.x),Mathf.Clamp01(x.y),Mathf.Clamp01(x.z)); }
    Vector3 hue(float x)
    {
        Vector3 vv = new Vector3(0, -1.0f / 3, 1.0f / 3);
        Vector3 x3 = new Vector3(x,x,x);
       
        Vector3 v1 =  new Vector3(1, 1, 1)-2 *fract3(new Vector3(x, x, x )+ new Vector3(0, -1.0f / 3, 1.0f / 3));
        return clamp3(3 * new Vector3(Mathf.Abs(v1.x), Mathf.Abs(v1.y), Mathf.Abs(v1.z)) - new Vector3(1, 1, 1));
    }
    public float step(float edge, float x)
    {
        return x >= edge ? 1.0f : 0.0f;
    }
    Vector3 lerp3(Vector3 a, Vector3 b, Vector3 x)
    {
        return new Vector3(Mathf.Lerp(a.x, b.x, x.x), Mathf.Lerp(a.y, b.y, x.y), Mathf.Lerp(a.z, b.z, x.z));
    }
    Vector3 lerp3x(Vector3 a, Vector3 b, float x)
    {
        return new Vector3(Mathf.Lerp(a.x, b.x, x), Mathf.Lerp(a.y, b.y, x), Mathf.Lerp(a.z, b.z, x));
    }
    float pow(float a,float b) { return Mathf.Pow(a, b); }
    float mix(float a, float b ,float x) { return Mathf.Lerp(a, b, x); }
    void Update()
    {
        //dis = Mathf.Pow( Mathf.Abs(script.pos4.x - script.pos3.x),2)*si2;
        //col1 = Vector3.SmoothDamp(col1, new Vector3(dis , script.pos4.y, script.pos3.y ), ref velocity, smoothTime);
        //col2 = Vector3.SmoothDamp(col1, new Vector3(0, script.pos4.z, script.pos3.z), ref velocity2, smoothTime);
        dis = script.pos7.x;
        dis2 = script.pos7.y;
        float time = Time.time;
        for (int  i = 0; i <  48; i++)
        {
            float a = (i ) / 48.0f;
            float b = Mathf.Floor(i / 3.0f)*3 / 48.0f;
            // Vector3 h = hue(a)*0.5f;
            float c = fract((i-1) / 3.0f);
            float m1 = step(0.4f, c);
            float m2 = step( c,0);
            float m3 = 1- Mathf.Max(m1 + m2);
            /* float tt = Time.time * 4;
             float c2 = step(Mathf.Lerp(0.4f, 0.2f, step(0.5f, fract(tt))), fract(distance(a + 0.5f / 16, 0.5f) * fract(Mathf.Floor(Time.time / 16.0f) / 4) * 4));
             float c3 = Mathf.Lerp(c2, 1- c2, step(0.5f, fract(tt * 0.5f)));
             Vector3 c4b = lerp3x( new Vector3(1, 0, 0),new Vector3(0, 0, 0), step(0.5f, fract(tt * 0.125f)));
             Vector3 c4 = lerp3x(c4b, lerp3x(new Vector3(0, 0, 1), new Vector3(0, 0, 0), step(0.5f, fract(tt * 0.25f))), c3);   */
            //float g1 = Mathf.Sin(distance(a, Mathf.Sin(Time.time) * 0.5f + 0.5f) * 10- Time.time * 10) * 0.5f + 0.5f;
            //float r1 = rd(a * 16+ 65+ Time.time * 2);
            //Vector3 c4 = new Vector3(rd(a * 16 + 125 + Time.time * 2), r1, r1);
            //float r = Mathf.Pow(fract(t*si),5*si2);
            //float r2 = step(0.5f, fract(b * si4+t*si3))*0.2f;
            float pp = pow(fract(time * 0.125f), 1.5f);
            float v1 = pow(fract(time * 3), (1- pp) * 4);
            float v2 = step(0.5f, fract(b * 4+ 40 * pp));
            float v3 = mix(v1, v2, step(0.5f, fract(time * 0.125f)));
            l1.SetStrength(i, pow(v3,2)*0.5f);
        }
        /*l1.SetStrength(dist(0 , script.pos7.x));
        l1.SetStrength2(dist(1.0f/16, script.pos7.x));
        l1.SetStrength3(dist(2.0f / 16, script.pos7.x));
        l1.SetStrength4(dist(3.0f / 16, script.pos7.x));
        l1.SetStrength5(dist(4.0f / 16, script.pos7.x));
        l1.SetStrength6(dist(5.0f / 16, script.pos7.x));
        l1.SetStrength7(dist(6.0f / 16, script.pos7.x));
        l1.SetStrength8(dist(7.0f / 16, script.pos7.x));
        l1.SetStrength9(dist(8.0f / 16, script.pos7.x));
        l1.SetStrength10(dist(9.0f / 16, script.pos7.x));
        l1.SetStrength11(dist(10.0f / 16, script.pos7.x));
        l1.SetStrength12(dist(11.0f / 16, script.pos7.x));
        l1.SetStrength13(dist(12.0f / 16, script.pos7.x));
        l1.SetStrength14(dist(13.0f / 16, script.pos7.x));
        l1.SetStrength15(dist(14.0f / 16, script.pos7.x));
        l1.SetStrength16(dist(15.0f / 16, script.pos7.x));    */
        /*float p1 = Mathf.Clamp01(map(col1.y, 0.6f, 0.7f, 0, 1));
        float p2 = Mathf.Clamp01(map(col1.z, 0.6f, 0.7f, 0, 1));
        l1.SetStrength(p1);
        l1.SetStrength2(p1);
        l1.SetStrength3(col1.x);*/

    }

    
}
