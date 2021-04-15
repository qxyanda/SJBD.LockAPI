using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoorControl.Services;
using System;
using DoorControl.Entities;


namespace ActionTimeControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionTimeController : ControllerBase
    {
    
        private readonly ILogger<ActionTimeController> _logger;

        public ActionTimeController(ILogger<ActionTimeController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public Msg DoorActionTimeGet()
        {
            Console.WriteLine("----------" + DateTime.Now.ToString() + " : " + Request.HttpContext.Connection.RemoteIpAddress.ToString());
            Msg msg =new Msg();
            string retData="DoorActionTime Get Succeed : " + Service.actionTimeS + " s .";
            msg.code=200;
            msg.message="成功";
            msg.data=retData;
            Console.WriteLine(retData);
            return msg;
        }

        [HttpPost]
        public Msg DoorActionTimeSet(ReqParamActionTime reqParamActionTime)
        {
            int time = int.Parse(reqParamActionTime.time);
            Console.WriteLine("----------" + DateTime.Now.ToString() + " : "+ Request.HttpContext.Connection.RemoteIpAddress.ToString());
            Msg msg=new Msg();
            string retData="";
            if(time<3||time>60)
            {
                retData="DoorActionTime Set Failed : must be >= 3 s and <=60 s .";
                msg.code=400;
                msg.message="失败";
                msg.data=retData;
            }
            else
            {
                Service.actionTimeS=time;
                retData="DoorActionTime Set Succeed : " + time + " s .";
                msg.code=200;
                msg.message="成功";
                msg.data=retData;
            }
            
            Console.WriteLine(retData);
            return msg;
        }
    
    }
}