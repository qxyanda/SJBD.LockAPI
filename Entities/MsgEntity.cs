namespace DoorControl.Entities
{
    public class Msg
    {
        public int code { get; set; }

        public string message { get; set; }

        public string data { get; set; }

        public Msg Set(int i)
        {
            this.code = i;
            return this;
        }

        public Msg Set(string str)
        {            
            this.message = str;
            return this;
        }

        public Msg Set(int i, string str)
        {
            this.code = i;
            this.message = str;
            return this;
        }

        public Msg Set(int i, string str, string str1)
        {
            this.code = i;
            this.message = str;
            this.data = str1;
            return this;
        }


    }

    public class ReqParam
    {
        public string unit{ get; set; }
        public string doorId{ get; set; }

    }

    public class ReqParamActionTime
    {
        public string time{get;set;}
    }
}