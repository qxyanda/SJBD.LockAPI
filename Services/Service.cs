using System;
using System.Runtime.InteropServices;


namespace DoorControl.Services
{
    public class Service
    {
        public IntPtr h = IntPtr.Zero;
        public int ret = 0;
        public string retData = "";

        static public int actionTimeS = 5;
        //4.1  call connect function
        [DllImport("plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();

        public string connString = "protocol=TCP,ipaddress=172.18.0.200,port=4370,timeout=2000,passwd=";

        public void Connect()
        {
            string str = connString;
            if (IntPtr.Zero == h)
            {
                h = Connect(str);
                if (h != IntPtr.Zero)
                {
                    retData="Connect Device Succeed !";
                    ret=0;
                }
                else
                {
                    ret = PullLastError();
                    retData="Connect Device Failed ! The Error Id Is: " + ret;
                }

            }
            Console.WriteLine(retData);
        }

        //4.2 call Disconnect function
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);
        public void DisConnect()
        {
            if (IntPtr.Zero != h)
            {
                Disconnect(h);
                h = IntPtr.Zero;
            }
            if (h == IntPtr.Zero)
            {
                retData="DisConnect Device Succeed!";
                ret=0;
            }
            else
            {
                ret = PullLastError();
                retData="Connect Device Failed! The Error Id Is: " + ret;
            }
            Console.WriteLine(retData);
        }

        [DllImport("plcommpro.dll", EntryPoint = "ControlDevice")]
        public static extern int ControlDevice(IntPtr h, int operationid, int param1, int param2, int param3, int param4, string options);

        public void DoorOpen(int doorId,int doorAction)
        {
            //protection
            if(doorId<1||doorId>58)return;
            // if(doorAction<3||doorAction>60)return;

            int operID = 1;
            int outputAddrType = 1;

            if (IntPtr.Zero != h)
            {
                retData=operID.ToString() + "," + doorId.ToString() + "," + outputAddrType.ToString() + "," + doorAction.ToString();
                ret = ControlDevice(h, operID, doorId, outputAddrType, doorAction, 0, "");     //call ControlDevice funtion from PullSDK
            }
            else
            {
                retData="Connect Device Failed ! The Error Id Is " + PullLastError() + " .";
                return;
            }
            Console.WriteLine(retData);
            if (ret >= 0)
            {
                retData="The Operation Succeed !";
                return;
            }
            Console.WriteLine(retData);
        }
    }
}