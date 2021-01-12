using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoorControl.Services;
using System;
using DoorControl.Entities;


namespace AcitonTimeControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcitonTimeController : ControllerBase
    {
    
        private readonly ILogger<AcitonTimeController> _logger;

        public AcitonTimeController(ILogger<AcitonTimeController> logger)
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
            msg.msg="成功";
            msg.data=retData;
            Console.WriteLine(retData);
            return msg;
        }

        [HttpPost]
        public Msg DoorActionTimeSet(int time)
        {
            Console.WriteLine("----------" + DateTime.Now.ToString() + " : "+ Request.HttpContext.Connection.RemoteIpAddress.ToString());
            Msg msg=new Msg();
            string retData="";
            if(time<3||time>60)
            {
                retData="DoorActionTime Set Failed : must be >= 3 s and <=60 s .";
                msg.code=400;
                msg.msg="失败";
                msg.data=retData;
            }
            else
            {
                Service.actionTimeS=time;
                retData="DoorActionTime Set Succeed : " + time + " s .";
                msg.code=200;
                msg.msg="成功";
                msg.data=retData;
            }
            
            Console.WriteLine(retData);
            return msg;
        }
    
    }
}