namespace DoorControl.Entities
{
    public class Msg
    {
        public int code { get; set; }

        public string msg { get; set; }

        public string data { get; set; }

        public Msg Set(int i)
        {
            this.code = i;
            return this;
        }

        public Msg Set(string str)
        {            
            this.msg = str;
            return this;
        }

        public Msg Set(int i, string str)
        {
            this.code = i;
            this.msg = str;
            return this;
        }

        public Msg Set(int i, string str, string str1)
        {
            this.code = i;
            this.msg = str;
            this.data = str1;
            return this;
        }


    }
}