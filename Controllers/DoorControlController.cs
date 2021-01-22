using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoorControl.Services;
using System;
using DoorControl.Entities;


namespace DoorControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoorControlController : ControllerBase
    {
        
        private readonly ILogger<DoorControlController> _logger;

        public DoorControlController(ILogger<DoorControlController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Msg DoorOpenPost(int unit, int doorId)
        {
            Console.WriteLine("----------" + DateTime.Now.ToString() + " : " + Request.HttpContext.Connection.RemoteIpAddress.ToString());
            return DoorOpen(unit, doorId);
        }

        private Msg DoorOpen(int unit, int doorId)
        {
            Msg msg=new Msg();
            
            Service service=new Service();
            switch(unit)
            {
                case 1 :
                service.connString = "protocol=TCP,ipaddress=172.18.0.201,port=4370,timeout=2000,passwd=";
                break;
                case 2:
                service.connString = "protocol=TCP,ipaddress=172.18.0.200,port=4370,timeout=2000,passwd=";
                break;
                default:
                service.connString = "protocol=TCP,ipaddress=172.18.0.201,port=4370,timeout=2000,passwd=";
                break;
            }
            service.Connect();
            if(service.h != IntPtr.Zero)
            {
                service.DoorOpen(doorId,Service.actionTimeS);
            }
            
            if(service.ret >= 0)
            {

                msg.code=200;
                msg.msg="成功";
                msg.data=service.retData;
            }
            else{
                msg.code=400;
                msg.msg="失败";
                msg.data=service.retData;
            }
            
            service.DisConnect();
            return msg;
        }
    }
}
