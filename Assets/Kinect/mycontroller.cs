/*Developed by YangHu*/
using UnityEngine;
using System.Collections;

using UnityEngine;
using System;

using System.IO.Ports;//串口
using System.Threading;


public class mycontroller : MonoBehaviour {

    public SkeletonWrapper sw;//导入骨骼
    public GameObject Hip_Center;
    public GameObject Spine;
    public GameObject Shoulder_Center;
    public GameObject Head;
    public GameObject Collar_Left;
    public GameObject Shoulder_Left;
    public GameObject Elbow_Left;
    public GameObject Wrist_Left;
    public GameObject Hand_Left;
    public GameObject Fingers_Left; //unused
    public GameObject Collar_Right;
    public GameObject Shoulder_Right;
    public GameObject Elbow_Right;
    public GameObject Wrist_Right;
    public GameObject Hand_Right;
    public GameObject Fingers_Right; //unused
    public GameObject Hip_Override;
    public GameObject Hip_Left;
    public GameObject Knee_Left;
    public GameObject Ankle_Left;
    public GameObject Foot_Left;
    public GameObject Hip_Right;
    public GameObject Knee_Right;
    public GameObject Ankle_Right;
    public GameObject Foot_Right;
    public GameObject AAA;
    public GameObject BBB;
    public GameObject CCC;
	public GameObject DDD;
    public Boolean Sig1;
    public Boolean Sig2;
    public Boolean Sig3;
	public Boolean Sig4;
	public Boolean SigN1;
	public Boolean SigN2;
	public Boolean SigN3;
	public Boolean SigN4;

    public String ActionIndicate;

    public SerialPort _Port;//串口

    public Pose[] poseLibrary;//标准姿势库
	public Texture img1;
	public Texture img2;
	public Texture img3;
	public Texture img4;
	public Texture img;
	private AudioSource sound;
	public AudioClip start;
	public Transform playCompleteColone;
	public Transform play1Colone;
	public Transform play2Colone;
	public Transform play3Colone;
	public Transform play4Colone;


	// Use this for initialization

    

	void Start () {
        //Debug.Log("Start");
        InitPort();
        InitSig();
		setCom1();
		SigN1 = true;
		SigN2 = false;
		SigN3 = false;
		SigN4 = false;
        ActionIndicate = "开始练拳吧！";
		sound = gameObject.GetComponent<AudioSource> ();
		sound.clip = start;
		sound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
        doActions();
	}
    /*void OnGUI() {//加平移人物按键
        if (
            GUILayout.Button("MOVE0", GUILayout.Height(50), GUILayout.Width(100))) {
               // Debug.Log("yo");
                this.gameObject.transform.Translate(new Vector3(0, 5 * 1, 0));
               // Debug.Log("move");
        }
    }*/
    

    void OnGUI()//动作指示文本框
    {
        GUI.skin.label.normal.textColor = Color.red;//(0, 255.0 / 255, 0, 1.0);
        // 后面的color为 RGBA的格式，支持alpha，取值范围为浮点数： 0 - 1.0.
        GUI.skin.label.fontSize = 50;
        //GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(500, 600, 450, 120), ActionIndicate);//Rect —— 用来密码字段在屏幕上的矩形位置（起点x坐标，起点y坐标，控件宽度，控件高度）
		GUI.Label(new Rect(1200,100,200,200),img); 
		if(GUI.Button(new Rect(1200,600,100,50),"跳关"))  
		{  
			ActionIndicate	 = "跳关";  
			Sig1 = true;
			Sig2 = true;
			Sig3 = true;
			Sig4 = true;
		}
    }   

   public void setHeroState(int newState)//平移人物
    {
        this.gameObject.transform.Translate(new Vector3(0, 5 * 1, 0));
        //Debug.Log("ye");
    }


   public void SetCubeState1(GameObject temp)//移动物体。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
   {
       temp.transform.position = new Vector3(3, 0, 0);
       //GUILayout.Label("立方体的当前位置" + texiao.transform.position);

   }

	public void SetCubeState2(GameObject temp)//移动物体。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
	{
		temp.transform.position = new Vector3(1, 0, 0);
		//GUILayout.Label("立方体的当前位置" + texiao.transform.position);
		
	}

	public void SetCubeState3(GameObject temp)//移动物体。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
	{
		temp.transform.position = new Vector3(2, 2, -1);
		//GUILayout.Label("立方体的当前位置" + texiao.transform.position);
		
	}
	public void SetCubeState4(GameObject temp)//移动物体。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
	{
		temp.transform.position = new Vector3(2, 4, -2);
		//GUILayout.Label("立方体的当前位置" + texiao.transform.position);
		
	}
    
    public void InitPort()//串口初始化
    {
        _Port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
		_Port.WriteTimeout = 300;

		if(!_Port.IsOpen)
            _Port.Open();
         /*   //_Port .DataReceived +=
        }
        catch
        {// (Exception ex){ 
            // MessageBox.
        }
		*/
    }

    public void InitSig(){
        Sig1 = false;
        Sig2 = false;
        Sig3 = false;
		Sig4 = false;
    }
   
    public void setCom1()//写串口函数
   {
		if (_Port.IsOpen) {
			Byte[] buf = new Byte[4];
			buf[0] = 0xAF;
			buf[1] = 0xFD;
			buf[2] = 0X01;
			buf[3] = 0xDF;
			_Port.Write (buf,0,4);
			Debug.Log (buf);
		} 
		else {
		Debug .Log ("no");
		}
   }
    
	public void setCom2()//写串口函数
	{
		if (_Port.IsOpen) {
			Byte[] buf = new Byte[4];
			buf[0] = 0xAF;
			buf[1] = 0xFD;
			buf[2] = 0X02;
			buf[3] = 0xDF;
			_Port.Write (buf,0,4);
			Debug.Log (buf);
		} 
		else {
			Debug .Log ("no");
		}
	}
    private void doActions()//识别姿势
    {
        
       /* Vector3 hip = Hip_Center.transform.position;
        Vector3 handRight = Hand_Left.transform.position;
        Vector3 handLeft = Hand_Right.transform.position;*/


        PopulatePoseLibrary();//别忘了初始化！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //int instructionSeq;
        //Debug.Log(this.poseLibrary[0]);
        if (/*IsPose(this.poseLibrary[0])==true && SigN1 == true*/true) { //动作一
            //setHeroState(0);
            //setCom();     
            SetCubeState1(AAA);
            Sig1 = true;
            ActionIndicate = "动作1完成";
			img = img2;
			SigN2 = true;
			SigN1 = false;
			if(GameObject.FindWithTag("Play1") == null){
				create1();
			}
			//Debug.Log("hallo");
        }
        if (IsPose(this.poseLibrary[1]) == true && SigN2== true)
        { //动作二
            //setHeroState(0);
           // setCom();
            SetCubeState2(BBB);
            Sig2 = true;
            ActionIndicate = "动作2完成";
			img = img3;
			SigN2 = false;
			SigN3 = true;
			if(GameObject.FindWithTag("Play2") == null){
				create2();
			}
        }
        if (IsPose(this.poseLibrary[2]) == true && SigN3 == true)
        { //动作三
           //setHeroState(0);
            SetCubeState3(CCC);
            Sig3 = true;

            //setCom();
            ActionIndicate = "动作3完成";
			img = img4;
			SigN4 = true;
			SigN3 =false;
			if(GameObject.FindWithTag("Play3") == null){
				create3();
			}
        }
		if (IsPose(this.poseLibrary[3]) == true && SigN4 == true)
		{ //动作三
			//setHeroState(0);
			//SetCubeState(DDD);
			Sig4 = true;
			//setCom();
			SetCubeState4(DDD);
			ActionIndicate = "动作4完成";
			if(GameObject.FindWithTag("Play4") == null){
				create4();
			}
		}

        if (Sig1 && Sig2 && Sig3 && Sig4 == true) {
			ActionIndicate = "全部动作已完成";
			if(GameObject.FindWithTag("PlayComplete") == null){
				createPlayComplete();
			}
            setHeroState(0);       
            setCom2();

        }
        /*if (IsPose(this.poseLibrary[3]) == true)
        { //动作四
            setHeroState(0);
            setCom();
        }
        /*if (IsPose(this.poseLibrary[4]) == true)
        { //动作五
            setHeroState(0);
            setCom();
        }*/
        
        /*if ((handLeft.y - handRight.y)>10)
        {
            //print("left............................"); 
            //hs=Time.deltaTime*LSpeed; 
            //test.KeyVertical = 4; 
            setHeroState(0);
            setCom();
            //写串口
            //Debug.Log("left............................");
        }
        */
    }



    private double GetJointAngle(Vector3 centerJoint, Vector3 angleJoint)//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        /*Point primaryPoint = GetJointPoint(this.KinectDevice, centerJoint, this.LayoutRoot.RenderSize, new Point());
        Point anglePoint = GetJointPoint(this.KinectDevice, angleJoint, this.LayoutRoot.RenderSize, new Point());
        Point x = new Point(primaryPoint.X + anglePoint.X, primaryPoint.Y);*/
       /* Vector3 hip = Hip_Center.transform.position;
        Vector3 handRight = Hand_Left.transform.position;
        Vector3 handLeft = Hand_Right.transform.position;*/
        Vector3 primaryPoint = centerJoint;
        Vector3 anglePoint = angleJoint;
        Vector3 X = new Vector3(primaryPoint.x + anglePoint.x, primaryPoint.y, primaryPoint.z);

        //if ((handLeft.y - handRight.y)>10)
        double a;
        double b;
        double c;

        a = Math.Sqrt(Math.Pow(primaryPoint.x - anglePoint.x, 2) + Math.Pow(primaryPoint.y - anglePoint.y, 2));
        b = anglePoint.x;
        c = Math.Sqrt(Math.Pow(anglePoint.x - X.x, 2) + Math.Pow(anglePoint.y - X.y, 2));

        double angleRad = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        double angleDeg = angleRad * 180 / Math.PI;

        if (primaryPoint.y < anglePoint.y)
        {
            angleDeg = 360 - angleDeg;
        }
        //Debug.Log("angleDeg" + angleDeg);
        /*Debug.Log("primaryPoint.X" + primaryPoint.x);
        Debug.Log("primaryPoint.Y" + primaryPoint.y);
        Debug.Log("anglePoint.X" + anglePoint.x);
        Debug.Log("anglePoint.Y" + anglePoint.y);
        Debug.Log("a" + a);
        Debug.Log("b" + b);
        Debug.Log("x.X" + X.x);
        Debug.Log("x.Y" + X.y);
        Debug.Log("c" + c);*/
        //Thread.Sleep(100);
        /*//调试
            Console.WriteLine("angleDeg" + angleDeg);
            //调试
            Console.WriteLine("primaryPoint.X" + primaryPoint.X);
            Console.WriteLine("primaryPoint.Y" + primaryPoint.Y);
            Console.WriteLine("anglePoint.X" + anglePoint.X);
            Console.WriteLine("anglePoint.Y" + anglePoint.Y);
            Console.WriteLine("a" + a);
            //调试
            Console.WriteLine("b" + b);
            //调试
            Console.WriteLine("x.X" + x.X);
            Console.WriteLine("x.Y" + x.Y);
            Console.WriteLine("c" + c);
            //调试
            Console.WriteLine("angleRad" + angleRad);*/
        return angleDeg;
    }

    public struct Pose
    {
        public string Title;
        public PoseAngle[] Angles;
    }

    public struct PoseAngle
    {
        public PoseAngle(Vector3 centerJoint, Vector3 angleJoint, double angle, double threshold)
        {
            CenterJoint = centerJoint;
            AngleJoint = angleJoint;
            Angle = angle;
            Threshold = threshold;
        }
        public Vector3 CenterJoint;
        public Vector3 AngleJoint;
        public double Angle;
        public double Threshold;
    }


    public void PopulatePoseLibrary()//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        Vector3 hip = Hip_Center.transform.position;
        Vector3 spine = Spine.transform.position; 
        Vector3 shoulder = Shoulder_Center.transform.position;
        Vector3 head = Head.transform.position;
        Vector3 collarLeft = Collar_Left.transform.position;
        Vector3 shoulderLeft = Shoulder_Left.transform.position;
        Vector3 elbowLeft = Elbow_Left.transform.position;
        Vector3 wristLeft = Wrist_Left.transform.position;
        Vector3 handRight = Hand_Left.transform.position;
        Vector3 fingers = Fingers_Left.transform.position;//unused
        Vector3 collarRight = Collar_Right.transform.position;
        Vector3 shoulderRight = Shoulder_Right.transform.position;
        Vector3 elbowRight = Elbow_Right.transform.position;
        Vector3 wristRight = Wrist_Right.transform.position;
        Vector3 handLeft = Hand_Right.transform.position;
        Vector3 fingersRight = Fingers_Right.transform.position;//unused
        Vector3 hipOverride = Hip_Override.transform.position;
        Vector3 hipLeft = Hip_Left.transform.position;
        Vector3 kneeLeft = Knee_Left.transform.position;
        Vector3 ankleLeft = Ankle_Left.transform.position;
        Vector3 footLeft = Foot_Left.transform.position;
        Vector3 hipRight = Hip_Right.transform.position;
        Vector3 kneeRight = Knee_Right.transform.position;
        Vector3 ankleRight = Ankle_Right.transform.position;
        Vector3 footRight = Foot_Right.transform.position;


        this.poseLibrary = new Pose[5];

        //Pose 1 -举起手来 Both Hands Up
        this.poseLibrary[0] = new Pose();
        this.poseLibrary[0].Title = "两手头顶握拳";
        this.poseLibrary[0].Angles = new PoseAngle[8];
        this.poseLibrary[0].Angles[0] = new PoseAngle(shoulderLeft, elbowLeft, 250, 20);//JointType.ShoulderLeft, JointType.ElbowLeft,
        this.poseLibrary[0].Angles[1] = new PoseAngle(elbowLeft, wristLeft, 300, 20);
        this.poseLibrary[0].Angles[2] = new PoseAngle(shoulderRight, elbowRight, 290, 20);
        this.poseLibrary[0].Angles[3] = new PoseAngle(elbowRight, wristRight, 240, 20);
        this.poseLibrary[0].Angles[4] = new PoseAngle(kneeLeft, hipLeft, 290, 15);
        this.poseLibrary[0].Angles[5] = new PoseAngle(ankleLeft, kneeLeft, 290, 15);
        this.poseLibrary[0].Angles[6] = new PoseAngle(kneeRight, hipRight, 250, 15);
        this.poseLibrary[0].Angles[7] = new PoseAngle(ankleRight, kneeRight, 250, 15); 


        //Pose 2 -举起手来 Both Hands Up
        this.poseLibrary[1] = new Pose();
        this.poseLibrary[1].Title = "左手上举动作";
        this.poseLibrary[1].Angles = new PoseAngle[8];
        this.poseLibrary[1].Angles[0] = new PoseAngle(shoulderLeft,elbowLeft, 195, 15);
        this.poseLibrary[1].Angles[1] = new PoseAngle(elbowLeft, wristLeft, 260, 15);
        this.poseLibrary[1].Angles[2] = new PoseAngle(shoulderRight, elbowRight, 45, 15);
        this.poseLibrary[1].Angles[3] = new PoseAngle(elbowRight, wristRight, 45, 15);
        this.poseLibrary[1].Angles[4] = new PoseAngle(kneeLeft, hipLeft, 290, 15);
        this.poseLibrary[1].Angles[5] = new PoseAngle(ankleLeft, kneeLeft, 290, 20);
        this.poseLibrary[1].Angles[6] = new PoseAngle(kneeRight, hipRight, 250, 15);
        this.poseLibrary[1].Angles[7] = new PoseAngle(ankleRight, kneeRight, 250, 15);


        //Pose 3 - 把手放下来 Both Hands Down
        this.poseLibrary[2] = new Pose();
        this.poseLibrary[2].Title = "左手出拳";
        this.poseLibrary[2].Angles = new PoseAngle[8];
        this.poseLibrary[2].Angles[0] = new PoseAngle(shoulderLeft, elbowLeft, 200, 20);
        this.poseLibrary[2].Angles[1] = new PoseAngle(elbowLeft, wristLeft, 200, 20);
        this.poseLibrary[2].Angles[2] = new PoseAngle(shoulderRight, elbowRight, 25, 20);
        this.poseLibrary[2].Angles[3] = new PoseAngle(elbowRight, wristRight, 205, 20);
        this.poseLibrary[2].Angles[4] = new PoseAngle(kneeLeft, hipLeft, 290, 20);
        this.poseLibrary[2].Angles[5] = new PoseAngle(ankleLeft, kneeLeft, 290, 20);
        this.poseLibrary[2].Angles[6] = new PoseAngle(kneeRight, hipRight, 250, 20);
        this.poseLibrary[2].Angles[7] = new PoseAngle(ankleRight, kneeRight, 250, 20);


        //Pose 4 - 举起左手 Left Up and Right Down
        this.poseLibrary[3] = new Pose();
        this.poseLibrary[3].Title = "动作4";
        this.poseLibrary[3].Angles = new PoseAngle[8];
        this.poseLibrary[3].Angles[0] = new PoseAngle(shoulderLeft, elbowLeft, 220, 15);
        this.poseLibrary[3].Angles[1] = new PoseAngle(elbowLeft, wristLeft, 230, 15);
        this.poseLibrary[3].Angles[2] = new PoseAngle(shoulderRight, elbowRight, 320, 15);
        this.poseLibrary[3].Angles[3] = new PoseAngle(elbowRight, wristRight, 310, 15);
        this.poseLibrary[3].Angles[4] = new PoseAngle(kneeLeft, hipLeft, 290, 15);
        this.poseLibrary[3].Angles[5] = new PoseAngle(ankleLeft, kneeLeft, 290, 15);
        this.poseLibrary[3].Angles[6] = new PoseAngle(kneeRight, hipRight, 250, 15);
        this.poseLibrary[3].Angles[7] = new PoseAngle(ankleRight, kneeRight, 250, 15);


        //Pose 5 - 举起右手 Right Up and Left Down
        this.poseLibrary[4] = new Pose();
        this.poseLibrary[4].Title = "左手白鹤亮翅";
        this.poseLibrary[4].Angles = new PoseAngle[8];
        this.poseLibrary[4].Angles[0] = new PoseAngle(shoulderLeft, elbowLeft, 175, 25);
        this.poseLibrary[4].Angles[1] = new PoseAngle(elbowLeft, wristLeft, 140, 25);
        this.poseLibrary[4].Angles[2] = new PoseAngle(shoulderRight, elbowRight, 290, 25);
        this.poseLibrary[4].Angles[3] = new PoseAngle(elbowRight, wristRight, 200, 25);
        this.poseLibrary[4].Angles[4] = new PoseAngle(kneeLeft, hipLeft, 100, 25);
        this.poseLibrary[4].Angles[5] = new PoseAngle(ankleLeft, kneeLeft, 95, 25);
        this.poseLibrary[4].Angles[6] = new PoseAngle(kneeRight, hipRight, 15, 25);
        this.poseLibrary[4].Angles[7] = new PoseAngle(ankleRight, kneeRight, 90, 25);


    }

    public bool IsPose(Pose pose)//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        bool isPose = true;
        double angle;
        double poseAngle;
        double poseThreshold;
        double loAngle;
        double hiAngle;

        for (int i = 0; i < pose.Angles.Length && isPose; i++)//i小于检测角度个数，且这个角度检测通过了
        {
            //调试
            //Console.WriteLine("i" + i);
            //Debug.Log("i" + i);
            poseAngle = pose.Angles[i].Angle;
            poseThreshold = pose.Angles[i].Threshold;
            
            angle = GetJointAngle(pose.Angles[i].CenterJoint, pose.Angles[i].AngleJoint);//改。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。

            hiAngle = poseAngle + poseThreshold;
            loAngle = poseAngle - poseThreshold;

            if (hiAngle >= 360 || loAngle < 0)
            {
                loAngle = (loAngle < 0) ? 360 + loAngle : loAngle;
                hiAngle = hiAngle % 360;

                isPose = !(loAngle > angle && angle > hiAngle);
            }
            else
            {
                isPose = (loAngle <= angle && hiAngle >= angle);
            }
        }

        return isPose;
    }
	private void create1(){
		Transform play1 = Instantiate (play1Colone, play1Colone.position, play1Colone.rotation) as Transform;
		play1.SetParent (transform);
		play1.position = new Vector3 (-3, 0, 3);
		play1.localScale = new Vector3 (1, 1, 1);
	}
	private void create2(){
		Transform play2 = Instantiate (play2Colone, play2Colone.position, play2Colone.rotation) as Transform;
		play2.SetParent (transform);
		play2.position = new Vector3 (-3, 0, 3);
		play2.localScale = new Vector3 (1, 1, 1);
	}
	private void create3(){
		Transform play3 = Instantiate (play3Colone, play3Colone.position, play3Colone.rotation) as Transform;
		play3.SetParent (transform);
		play3.position = new Vector3 (-3, 0, 3);
		play3.localScale = new Vector3 (1, 1, 1);
	}
	private void create4(){
		Transform play4 = Instantiate (play4Colone, play4Colone.position, play4Colone.rotation) as Transform;
		play4.SetParent (transform);
		play4.position = new Vector3 (-3, 0, 3);
		play4.localScale = new Vector3 (1, 1, 1);
	}
	private void createPlayComplete(){
	    Transform playComplete = Instantiate (playCompleteColone, playCompleteColone.position, playCompleteColone.rotation) as Transform;
		playComplete.SetParent (transform);
		playComplete.position = new Vector3 (-3, 0, 3);
		playComplete.localScale = new Vector3 (1, 1, 1);
	

	}


}
