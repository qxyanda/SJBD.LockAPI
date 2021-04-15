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
        public Msg DoorOpenPost(ReqParam reqParam)
        {
            Console.WriteLine("----------" + DateTime.Now.ToString() + " : " + Request.HttpContext.Connection.RemoteIpAddress.ToString());
            return DoorOpen(int.Parse(reqParam.unit), int.Parse(reqParam.doorId));
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

            if(doorId<=58 && doorId>=1 && unit>=1 && unit <=2)
            {
                service.Connect();
                if(service.h != IntPtr.Zero)
                {
                    service.DoorOpen(doorId,Service.actionTimeS);
                }
            }
            
            if(service.ret >= 0)
            {
                msg.code=200;
                msg.message="成功";
                msg.data=service.retData;
                Console.WriteLine(msg.data);
            }
            else{
                msg.code=400;
                msg.message="失败";
                msg.data=service.retData;
                Console.WriteLine(msg.data);
            }

            if(doorId>58 || doorId<1)
            {
                msg.code=400;
                msg.message="失败";
                msg.data="door id do not exist";
                Console.WriteLine(msg.data);
            }

            if(unit>2 || unit<1)
            {
                msg.code=400;
                msg.message="失败";
                msg.data="unit id do not exist";
                Console.WriteLine(msg.data);
            }
            
            service.DisConnect();
            return msg;
        }
    }
}
