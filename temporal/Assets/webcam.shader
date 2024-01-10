Shader "Unlit/webcam"
{
    Properties
    {

        _MainTex ("Texture", 2D) = "white" {}
		_color1("_color1",Color) = (0.5,0.5,0.5,1.)
		_color2("_color2",Color) = (0.5,0.5,0.5,1.)
		_handRight("_handRight",Vector)=(0.,0.,0.,0.)
			_handLeft("_handLeft",Vector) = (0.,0.,0.,0.)
				_head("_head",Vector) = (0.,0.,0.,0.)
			_shoulderRight("_shoulderRight", Vector) = (0., 0., 0., 0.)
			_shoulderLeft("_shoulderLeft", Vector) = (0., 0., 0., 0.)
			_elbowRight("_elbowRight", Vector) = (0., 0., 0., 0.)
			_elbowLeft("_elbowLeft", Vector) = (0., 0., 0., 0.)
			_hipRight("_hipRight", Vector) = (0., 0., 0., 0.)
			_hipLeft("_hipLeft", Vector) = (0., 0., 0., 0.)
			_kneeRight("_kneeRight", Vector) = (0., 0., 0., 0.)
			_kneeLeft("_kneeLeft", Vector) = (0., 0., 0., 0.)
			_ankleRight("_ankleRight", Vector) = (0., 0., 0., 0.)
			_ankleLeft("_ankleLeft", Vector) = (0., 0., 0., 0.)
			_a("_a",Float) =0
			_a2("_a2",Float) = 0
			_b("_b",Float) = 0
			_b2("_b2",Float) = 0
			_c("_c",Float) = 0
			_c2("_c2",Float) = 0
			_d("_d",Float) = 0
			_d2("_d2",Float) = 0			
			_e("_e",Float) = 0
			_e2("_e2",Float) = 0
			_f("_f",Float) =0
			_f2("_f2",Float) = 0
			_ff("_ff",Float) = 0
			_fff("_fff",Float) = 0
			_ff2("_ff2",Float) = 0
			_fff2("_fff2",Float) = 0
			_g("_g",Float) = 0
			_g2("_g2",Float) = 0
			_h("_h",Float) = 0
			_h2("_h2",Float) = 0
			_i("_i",Float) = 0
			_i2("_i2",Float) = 0
			_j("_j",Float) = 0
			_j2("_j2",Float) = 0
			_k("_k",Float) = 0
			_k2("_k2",Float) = 0
			_l("_l",Float) = 0
			_l2("_l2",Float) = 0
			_m("_m",Float) = 0
			_m2("_m2",Float) = 0
			_n("_n",Float) = 0
			_n2("_n2",Float) = 0
			_o("_o",Float) = 0
			_o2("_o2",Float) = 0
			_p("_p",Float) = 0
			_p2("_p2",Float) = 0
			_q("_q",Float) = 0
			_q2("_q2",Float) = 0
			_r("_r",Float) = 0
			_r2("_r2",Float) = 0
			_s("_s",Float) = 0
			_s2("_s2",Float) = 0
			_t("_t",Float) = 0
			_t2("_t2",Float) = 0
			_rs("_rs",Float) = 0
			_rs2("_rs2",Float) = 0
			

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _color1;
			float4 _color2;
			float4 _handRight;
			float4 _handLeft;
			float4 _head;
			float4 _hipRight;
			float4 _hipLeft;
			float4 _shoulderRight;
			float4 _shoulderLeft;
			float4 _elbowRight;
			float4 _elbowLeft;
			float4 _kneeRight;
			float4 _kneeLeft;
			float4 _ankleRight;
			float4 _ankleLeft;
			float _a;
			float _a2;
			float _b;
			float _b2;
			float _c;
			float _c2;
			float _d;
			float _d2;
			float _e;
			float _e2;
			float _f;
			float _ff;
			float _fff;
			float _ff2;
			float _fff2;
			float _f2;
			float _g;
			float _g2;
			float _h;
			float _h2;
			float _i;
			float _i2;
			float _j;
			float _j2;
			float _k;
			float _k2;
			float _l;
			float _l2;
			float _m;
			float _m2;
			float _n;
			float _n2;
			float _o;
			float _o2;
			float _p;
			float _p2;
			float _q;
			float _q2;
			float _r;
			float _r2;
			float _s;
			float _s2;
			float _t;
			float _t2;
			float _rs;
			float _rs2;
			
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
			float li(float2 uv, float2  a, float2 b) {
				float2 ua = uv - a; float2 ba = b - a;
				float h = clamp(dot(ua, ba) / dot(ba, ba), 0., 1.);
				return length(ua - ba * h);
			}
			float smin(float d1, float d2, float k) {
				float h = clamp(0.5 + 0.5*(d2 - d1) / k, 0.0, 1.0);
				return lerp(d2, d1, h) - k * h*(1.0 - h);
			}
			float ov(float base, float blend) {
				return base < 0.5 ? (2.0*base*blend) : (1.0 - 2.0*(1.0 - base)*(1.0 - blend));
			}
			float3 ov3(float3 a, float b) {
				return float3(ov(a.x, b), ov(a.y, b), ov(a.z, b));
			}
			float3 ov33(float3 a, float3 b) {
				return float3(ov(a.x, b.x), ov(a.y, b.y), ov(a.z, b.z));
			}
            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv);
			/*float d1 = smoothstep(0.01, 0.009, distance(i.uv, _handRight.xy))*step(va, _handRight.z);
			float d2 = smoothstep(0.01, 0.009, distance(i.uv, _handLeft.xy))*step(va, _handLeft.z);
			float d3 = smoothstep(0.01, 0.009, distance(i.uv, _head.xy))*step(va, _head.z);
			float d4 = smoothstep(0.01, 0.009, distance(i.uv, _hipRight.xy))*step(va, _hipRight.z);
			float d5 = smoothstep(0.01, 0.009, distance(i.uv, _hipLeft.xy))*step(va, _hipLeft.z);
			float d6 = smoothstep(0.01, 0.009, distance(i.uv, _shoulderRight.xy))*step(va, _shoulderRight.z);
			float d7 = smoothstep(0.01, 0.009, distance(i.uv, _shoulderLeft.xy))*step(va, _shoulderLeft.z);
			float d8 = smoothstep(0.01, 0.009, distance(i.uv, _kneeRight.xy))*step(va, _kneeRight.z);
			float d9 = smoothstep(0.01, 0.009, distance(i.uv, _kneeLeft.xy))*step(va, _kneeLeft.z);
			float d10 = smoothstep(0.01, 0.009, distance(i.uv, _ankleRight.xy))*step(va, _ankleRight.z);
			float d11 = smoothstep(0.01, 0.009, distance(i.uv, _ankleLeft.xy))*step(va, _ankleLeft.z);
			float d12 = smoothstep(0.01, 0.009, distance(i.uv, _elbowRight.xy))*step(va, _elbowRight.z);
			float d13 = smoothstep(0.01, 0.009, distance(i.uv, _elbowLeft.xy))*step(va, _elbowLeft.z);
                return col+d1+d2+d3+d4+d5+d6+d7+d8+d9+d10+d11+d12+d13;*/
			float k = 0.02;
			float t1 = 0.002;
			float va = 0.5;
			float l1 = max(li(i.uv, _handRight.xy, _elbowRight.xy), 1.-max(step(va, _handRight.z),step(va, _elbowRight.z)));
			float l2 = max( li(i.uv, _handLeft.xy, _elbowLeft.xy), 1. - max(step(va, _handLeft.z),step(va, _elbowLeft.z)));
			float l3 = max( li(i.uv, _shoulderRight.xy, _elbowRight.xy), 1. - max(step(va, _shoulderRight.z),step(va, _elbowRight.z)));
			float l4 = max( li(i.uv, _shoulderLeft.xy, _elbowLeft.xy), 1. - max(step(va, _shoulderLeft.z),step(va, _elbowLeft.z)));
			float l5 = max( li(i.uv, _hipRight.xy, _kneeRight.xy), 1. - max(step(va, _hipRight.z),step(va, _kneeRight.z)));
			float l6 = max( li(i.uv, _hipLeft.xy, _kneeLeft.xy), 1. - max(step(va, _hipLeft.z),step(va, _kneeLeft.z)));
			float l7 = max(li(i.uv, _ankleRight.xy, _kneeRight.xy), 1. - max(step(va, _ankleRight.z),step(va, _kneeRight.z)));
			float l8 = max(li(i.uv, _ankleLeft.xy, _kneeLeft.xy), 1. - max(step(va, _ankleLeft.z),step(va, _kneeLeft.z)));
			float l9 = max(li(i.uv, _hipLeft.xy, _hipRight.xy), 1. - max(step(va, _hipLeft.z),step(va, _hipRight.z)));
			float l10 = max( li(i.uv, _shoulderLeft.xy, _shoulderRight.xy), 1. - max(step(va, _shoulderLeft.z),step(va, _shoulderRight.z)));
			float l11 = max( li(i.uv, _head.xy, (_hipRight.xy+ _hipLeft.xy)*0.5), 1. - step(va, _head.z));
			float lf = smin(smin(smin(smin(smin(smin(smin(smin(smin(smin(l1, l2,k), l3, k), l4, k), l5, k), l6, k), l7, k), l8, k), l9, k), l10, k), l11, k);
			float lf1 = smoothstep(0.003, 0.002, lf);
			
			float ra = distance(_head.xy, (_hipRight.xy + _hipLeft.xy)*0.5);
			float2 s = float2(0.5/1.6,0.5);
			float s2 = 1.;
			float2 po = float2((-1.+ (_hipRight.x + _hipLeft.x)*0.5), -0.5);
			


			float a1 = li(i.uv + po, float2(_l, _l2)*s, float2(_m, _m2)*s);
			float a2 = li(i.uv + po, float2(_m, _m2)*s, float2(_n, _n2)*s);
			//float a3 = smoothstep(t1*s2, t1*s2 - 0.001, li(i.uv + po, float2(_n, _n2)*s, float2(_o, _o2)*s))*0.;
			//float a4 = smoothstep(t1*s2, t1*s2 - 0.001, li(i.uv + po, float2(_o, _o2)*s, float2(_p, _p2)*s))*0.;
			
			float a5 =  li(i.uv + po, float2(_g, _g2)*s, float2(_h, _h2)*s);
			float a6 = li(i.uv + po, float2(_h, _h2)*s, float2(_i, _i2)*s);
			//float a7 = smoothstep(t1*s2, t1*s2 - 0.001, li(i.uv + po, float2(_i, _i2)*s, float2(_j, _j2)*s))*0.;
			//float a8 = smoothstep(t1*s2, t1*s2 - 0.001, li(i.uv + po, float2(_j, _j2)*s, float2(_k, _k2)*s))*0.;

			float a9 =  li(i.uv + po, float2(_f, _f2)*s, float2(_ff, _ff2)*s);
			float a10 =  li(i.uv + po, float2(_ff, _ff2)*s, float2(_fff, _fff2)*s);
			//float a11 = smoothstep(t1*s2, t1*s2 - 0.001, li(i.uv + po, float2(_f, _f2)*s, float2(_rs, _rs2)*s));
			
			float a12 = li(i.uv + po, float2(_c, _c2)*s, float2(_d, _d2)*s);
			float a13 =  li(i.uv + po, float2(_d, _d2)*s, float2(_e, _e2)*s);
			//float a14 = smoothstep(t1*s2, t1*s2 - 0.001, li(i.uv + po, float2(_f, _f2)*s, float2(_rs, _rs2)*s));

			float a15 =  li(i.uv + po, float2(_l, _l2)*s, float2(_g, _g2)*s);
			float a16 =  li(i.uv + po, float2(_f, _f2)*s, float2(_c, _c2)*s);
			float a17 =  li(i.uv + po, float2(_a, _a2)*s, float2(_b, _b2)*s);
			float a18 =  li(i.uv + po, float2(_a, _a2)*s, (float2(_l, _l2)+ float2(_g, _g2))*0.5*s);

			float af = smin(smin(smin(smin(smin(smin(smin(smin(smin(smin(smin(a1, a2, k), a5, k), a6, k), a9, k), a10, k), a12, k), a13, k), a15, k), a16, k), a17, k), a18, k);
			float aff = smoothstep(0.003, 0.002, af);

			
			float lf2 = smoothstep(0.001, 0.0009, distance(lf, 0.05))*step(lf,af);
			float af2 = smoothstep(0.001, 0.0009, distance(af, 0.05))*step(af, lf);
			
			float sp = 0.;
			float u1 = i.uv.x*20. + i.uv.y*20.;
			float u2 = i.uv.y*20. - i.uv.x*20.;
			float baf = smoothstep(0.05, 0.049, af);
			float rp = max(max(smoothstep(0.,0.02,distance(0.5,frac(u1))), baf),step(frac(u2*4.+_Time.y*10.),0.5));
			float bb = max(step(0.01,frac(af*  10.)),baf);
			float3 co = _color1.xyz;
			if (af > lf) {
				co = _color2.xyz;
				sp = 1.;
				float bff = smoothstep(0.05, 0.049, lf);
				rp = max(max(smoothstep(0., 0.02, distance(0.5, frac(u2))) ,bff), step(frac(u1*4. + _Time.y*10.), 0.5));
				bb = max(step(0.01,frac(lf*  10.)),bff);
			}
			
			float sp2 = 0.;
			if (af > lf-0.002) {
				sp2 = 1.;
			}
			float sp3 = 1.-(sp2 - sp);
			float vh1 = distance(0.25, i.uv.y);
			float vh2 = distance(0.75, i.uv.y);
			float vc1 = distance(0.2, i.uv.x);
			float vc2 = distance(0.4, i.uv.x);
			float vc3 = distance(0.6, i.uv.x);
			float vc4 = distance(0.8, i.uv.x);
			float h1 = step(0.02,vh1);
			float h2 = step(0.02, vh2);
			float c1 = max(step(0.001/1.5,vc1),h1)*max(step(0.001,vh1),step(0.02/1.7,vc1));
			float c2 = max(step(0.001 / 1.5, vc2), h1)*max(step(0.001, vh1), step(0.02 / 1.7, vc2));
			float c3 = max(step(0.001 / 1.5, vc3), h1)*max(step(0.001, vh1), step(0.02 / 1.7, vc3));
			float c4 = max(step(0.001 / 1.5, vc4), h1)*max(step(0.001, vh1), step(0.02 / 1.7, vc4));
			float c5 = max(step(0.001 / 1.5, vc1), h2)*max(step(0.001, vh2), step(0.02 / 1.7, vc1));
			float c6 = max(step(0.001 / 1.5, vc2), h2)*max(step(0.001, vh2), step(0.02 / 1.7, vc2));
			float c7 = max(step(0.001 / 1.5, vc3), h2)*max(step(0.001, vh2), step(0.02 / 1.7, vc3));
			float c8 = max(step(0.001 / 1.5, vc4), h2)*max(step(0.001, vh2), step(0.02 / 1.7, vc4));
			float cf = c1 * c2*c3*c4*c5*c6*c7*c8;
			float4 r1 = float4(ov33(lerp(float3(0.5,0.5,0.5),co,lerp(0.,0.5,smoothstep(0.3,1.,sin(_Time.y)*0.5+0.5))),
				ov3(col, ov(lerp(0.5*smoothstep(0.00, 0.001, distance(af, 0.003)), 1., aff),
				lerp(0.5*smoothstep(0.00, 0.001, distance(lf, 0.003)), 1., lf1)))), 1.) 
				* (1. - lf2)*(1. - af2)*sp3*rp*c1*c2*c3 *c4*c5*c6*c7*c8*bb;

			if (af > lf) {
				r1 *= cf;
			}
			else {
				r1 += 1. - cf;
			}
			return r1;
            }
            ENDCG
        }
    }
}
