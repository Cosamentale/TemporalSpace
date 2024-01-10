/*
	Created by Carl Emil Carlsen.
	Copyright 2016-2018 Sixth Sensor.
	All rights reserved.
	http://sixthsensor.dk
*/

using UnityEngine;

namespace OscSimpl.Examples
{
	public class GettingStartedReceiving : MonoBehaviour
	{
		[SerializeField] OscIn _oscIn;

		public string address1 = "/f7/f1";
		public string address2 = "/1/1";
        float _incomingFloat;
        private int Nbr_portIn;
        public float a;
        public float b;
        public float c;
        public float d;
        public float e;
        public float f;
        public float g;
        public float h;
        public float i;
        public float j;
        public float k;
        public float l;
        public float m;
        public float n;
        public float o;
        public float p;
        public float q;
        public float r;
        public float s;
        public float t;
        public float a2;
        public float b2;
        public float c2;
        public float d2;
        public float e2;
        public float f2;
        public float ff;
        public float fff;
        public float ff2;
        public float fff2;
        public float g2;
        public float g3;
        public float g4;
        public float h2;
        public float i2;
        public float j2;
        public float k2;
        public float l2;
        public float m2;
        public float n2;
        public float o2;
        public float p2;
        public float q2;
        public float r2;
        public float s2;
        public float t2;
        public float rs;
        public float rs2;
       // public InfraredSourceCompute script;
        public Material mat;
        void Start()
		{
            Nbr_portIn = _oscIn.port;
			// Ensure that we have a OscIn component and start receiving on port 7000.
			if( !_oscIn ) _oscIn = gameObject.AddComponent<OscIn>();
			_oscIn.Open( Nbr_portIn);

        }


		void OnEnable()
		{
            // You can "map" messages to methods in two ways:

            // 1) For messages with a single argument, route the value using the type specific map methods.
            /////// EVENEMENT MAPPING_oscIn.MapFloat( address1, Event1 );

            // 2) For messages with multiple arguments, route the message using the Map method.
            //_oscIn.Map( address2, OnCusto );
            // _oscIn.Map(address2, OnCusto);
            _oscIn.Map(address2, OnTest2);
        }


		void OnDisable()
		{
			// If you want to stop receiving messages you have to "unmap".
			//_oscIn.UnmapFloat( OnTest1 );
			_oscIn.Unmap( OnTest2 );
		}
        void Test1(OscMessage incomingMessage)
        {
            if (incomingMessage.TryGet(0, out _incomingFloat))
            {
                Debug.Log("ConditionOk");
            }
          //  Debug.Log("Received f7/f1" + "\n");
            
        }
        void OnTest2(OscMessage message)
        {
            // Get arguments from index 0, 1 and 2 safely.


            message.TryGet(12, out a);
            message.TryGet(13, out a2);

            message.TryGet(18, out b);
            message.TryGet(19, out b2);

            message.TryGet(24, out c);
            message.TryGet(25, out c2);

            message.TryGet(27, out d);
            message.TryGet(28, out d2);

            message.TryGet(33, out e);
            message.TryGet(34, out e2);

            /*message.TryGet(111, out rs);
            message.TryGet(112, out rs2); */

            message.TryGet(114, out f);
            message.TryGet(115, out f2);

            message.TryGet(117, out ff);
            message.TryGet(118, out ff2);

            message.TryGet(123, out fff);
            message.TryGet(124, out fff2);

            message.TryGet(201, out g);
            message.TryGet(202, out g2);

            message.TryGet(204, out h);
            message.TryGet(205, out h2);

            message.TryGet(207, out i);
            message.TryGet(208, out i2);

            message.TryGet(210, out j);
            message.TryGet(211, out j2);

            message.TryGet(213, out k);
            message.TryGet(214, out k2);

            message.TryGet(216, out l);
            message.TryGet(217, out l2);

            message.TryGet(219, out m);
            message.TryGet(220, out m2);

            message.TryGet(222, out n);
            message.TryGet(223, out n2);

            message.TryGet(225, out o);
            message.TryGet(226, out o2);

            message.TryGet(228, out p);
            message.TryGet(229, out p2);

           /* message.TryGet(57, out q);
            message.TryGet(58, out q2);

            message.TryGet(60, out r);
            message.TryGet(61, out r2);

            message.TryGet(63, out s);
            message.TryGet(64, out s2);

            message.TryGet(66, out t);
            message.TryGet(67, out t2); */

            mat.SetFloat("_a", a);
            mat.SetFloat("_a2", a2);
            mat.SetFloat("_b", b);
            mat.SetFloat("_b2", b2);
            mat.SetFloat("_c", c);
            mat.SetFloat("_c2", c2);
            mat.SetFloat("_d", d);
            mat.SetFloat("_d2", d2);
            mat.SetFloat("_e", e);
            mat.SetFloat("_e2", e2);
           /* mat.SetFloat("_rs", rs);
            mat.SetFloat("_rs2", rs2);  */
            mat.SetFloat("_f", f);
            mat.SetFloat("_f2", f2);
            mat.SetFloat("_ff", ff);
            mat.SetFloat("_ff2", ff2);
            mat.SetFloat("_fff", fff);
            mat.SetFloat("_fff2", fff2);
            mat.SetFloat("_g", g);
            mat.SetFloat("_g2", g2);
            mat.SetFloat("_h", h);
            mat.SetFloat("_h2", h2);
            mat.SetFloat("_i", i);
            mat.SetFloat("_i2", i2);
            mat.SetFloat("_j", j);
            mat.SetFloat("_j2", j2);
            mat.SetFloat("_k", k);
            mat.SetFloat("_k2", k2);
            mat.SetFloat("_l", l);
            mat.SetFloat("_l2", l2);
            mat.SetFloat("_m", m);
            mat.SetFloat("_m2", m2);
            mat.SetFloat("_n", n);
            mat.SetFloat("_n2", n2);
            mat.SetFloat("_o", o);
            mat.SetFloat("_o2", o2);
            mat.SetFloat("_p", p);
            mat.SetFloat("_p2", p2);

           // l,m,n,g,h,i,f,ff,fff,c,d,e
            
          /*  script.l = new Vector2(l, l2);
            script.m = new Vector2(m, m2);
            script.n = new Vector2(n, n2);
            script.g = new Vector2(g, g2);
            script.h = new Vector2(h, h2);
            script.i = new Vector2(i, i2);
            script.f = new Vector2(f, f2);
            script.ff = new Vector2(ff, ff2);
            script.fff = new Vector2(fff, fff2);
            script.c = new Vector2(c, c2);
            script.d = new Vector2(d, d2);
            script.e = new Vector2(e, e2);
            script.a = new Vector2(a, a2);
            script.b = new Vector2(b, b2);    */
            /* mat.SetFloat("_q", q);
             mat.SetFloat("_q2", q2);
             mat.SetFloat("_r", r);
             mat.SetFloat("_r2", r2);
             mat.SetFloat("_s", s);
             mat.SetFloat("_s2", s2);
             mat.SetFloat("_t", t);
             mat.SetFloat("_t2", t2); */
            // message.TryGet(19, out t);

            /*{
                Debug.Log("Received test2\n" + a + " " + b + " " + c +d+ e + f+ g+"\n");
            } */
        }
            void Event1( float value )
		{
            
            Debug.Log( "Received f7/f1" + value + "\n" );
		}


	
	}
}