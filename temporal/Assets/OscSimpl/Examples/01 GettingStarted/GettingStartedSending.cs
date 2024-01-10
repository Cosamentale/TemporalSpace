using UnityEngine;

namespace OscSimpl.Examples
{
	public class GettingStartedSending : MonoBehaviour
	{
		[SerializeField] OscOut _oscOut;

		OscMessage _message2; // Cached message.
       
        private int Nbr_portOut;
        public string address1 = "/HandRightX";
        public string address2 = "/HandRightY";
        public string address3 = "/HandRightZ";
        public string address4 = "/HandLeftX";
		public string address5 = "/HandLeftY";
        public string address6 = "/HandLeftZ";
        public string address7 = "/HeadX";
        public string address8 = "/HeadY";
        public string address9 = "/HeadZ";
        public string address10 = "/HipRightX";
        public string address11 = "/HipRightY";
        public string address12 = "/HipRightZ";
        public string address13 = "/HipLeftX";
        public string address14 = "/HipLeftY";
        public string address15 = "/HipLeftZ";
        public string address16 = "/ShoulderRightX";
        public string address17 = "/ShoulderRightY";
        public string address18 = "/ShoulderRightZ";
        public string address19 = "/ShoulderLeftX";
        public string address20 = "/ShoulderLeftY";
        public string address21 = "/ShoulderLeftZ";
        public string address22 = "/ElbowRightX";
        public string address23 = "/ElbowRightY";
        public string address24 = "/ElbowRightZ";
        public string address25 = "/ElbowLeftX";
        public string address26 = "/ElbowLeftY";
        public string address27 = "/ElbowLeftZ";
        public string address28 = "/KneeRightX";
        public string address29 = "/KneeRightY";
        public string address30 = "/KneeRightZ";
        public string address31 = "/KneeLeftX";
        public string address32 = "/KneeLeftY";
        public string address33 = "/KneeLeftZ";
        public string address34 = "/AnkleRightX";
        public string address35 = "/AnkleRightY";
        public string address36 = "/AnkleRightZ";
        public string address37 = "/AnkleLeftX";
        public string address38 = "/AnkleLeftY";
        public string address39 = "/AnkleLeftZ";
       /* public float float1;
        public float float2;
        public float float3;
        public float float4;
        public float float5;
        public float float6;
        public float[] values;    */
        private string LocalIPTarget;  
        //public float floatValue;
        //public float floatValue2;
        public modelData script;
        void Start()
		{
            LocalIPTarget = _oscOut.remoteIpAddress;
            Nbr_portOut = _oscOut.port;
           // LocalIPTarget = "192.168.1.25";
            // Ensure that we have a OscOut component.
            if ( !_oscOut ) _oscOut = gameObject.AddComponent<OscOut>();

			// Prepare for sending messages locally on this device on port 7000.
			_oscOut.Open(Nbr_portOut, LocalIPTarget);

            // ... or, alternatively target remote devices with a IP Address.
            //oscOut.Open( 7000, "192.168.1.101" );

            // If you want to send a single value then you can use this one-liner.
            //_oscOut.Send( address1, 0.5f );

            // If you want to send a message with multiple values, then you
            // need to create a message, add your values and send it.
            // Always cache the messages you create, so that you can reuse them.
            //_message2 = new OscMessage( address2 );
            //_message2.Add( Time.frameCount ).Add( Time.time ).Add( Random.value );
            //_oscOut.Send( _message2 );
            // _oscOut.Send(address2, 0.6f);
        }


		void Update()
		{
            _oscOut.Send(address1, script.handRight.x * 10);
            _oscOut.Send(address2, script.handRight.y * 10);
            _oscOut.Send(address3, script.handRight.z * 10);
            _oscOut.Send(address4, script.handLeft.x*10);
             _oscOut.Send(address5, script.handLeft.y * 10);
             _oscOut.Send(address6, script.handLeft.z * 10);
             _oscOut.Send(address7, script.headc.x * 10);
             _oscOut.Send(address8, script.headc.y * 10);
             _oscOut.Send(address9, script.headc.z * 10);
            _oscOut.Send(address10, script.hipRight.x * 10);
            _oscOut.Send(address11, script.hipRight.y * 10);
            _oscOut.Send(address12, script.hipRight.z * 10);
            _oscOut.Send(address13, script.hipLeft.x * 10);
            _oscOut.Send(address14, script.hipLeft.y * 10);
            _oscOut.Send(address15, script.hipLeft.z * 10);
            _oscOut.Send(address16, script.shoulderRight.x * 10);
            _oscOut.Send(address17, script.shoulderRight.y * 10);
            _oscOut.Send(address18, script.shoulderRight.z * 10);
            _oscOut.Send(address19, script.shoulderLeft.x * 10);
            _oscOut.Send(address20, script.shoulderLeft.y * 10);
            _oscOut.Send(address21, script.shoulderLeft.z * 10);
            _oscOut.Send(address22, script.elbowRight.x * 10);
            _oscOut.Send(address23, script.elbowRight.y * 10);
            _oscOut.Send(address24, script.elbowRight.z * 10);
            _oscOut.Send(address25, script.elbowLeft.x * 10);
            _oscOut.Send(address26, script.elbowLeft.y * 10);
            _oscOut.Send(address27, script.elbowLeft.z * 10);
            _oscOut.Send(address28, script.kneeRight.x * 10);
            _oscOut.Send(address29, script.kneeRight.y * 10);
            _oscOut.Send(address30, script.kneeRight.z * 10);
            _oscOut.Send(address31, script.kneeLeft.x * 10);
            _oscOut.Send(address32, script.kneeLeft.y * 10);
            _oscOut.Send(address33, script.kneeLeft.z * 10);
            _oscOut.Send(address34, script.ankleRight.x * 10);
            _oscOut.Send(address35, script.ankleRight.y * 10);
            _oscOut.Send(address36, script.ankleRight.z * 10);
            _oscOut.Send(address37, script.ankleLeft.x * 10);
            _oscOut.Send(address38, script.ankleLeft.y * 10);
            _oscOut.Send(address39, script.ankleLeft.z * 10);

        }
	}
}