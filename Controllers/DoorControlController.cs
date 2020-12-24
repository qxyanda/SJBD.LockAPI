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
        public Msg DoorOpenPost(int doorId)
        {
            
            return DoorOpen(doorId);
        }

        private Msg DoorOpen(int doorId)
        {
            Msg msg=new Msg();
            
            Service service=new Service();
            service.Connect();
            if(service.h != IntPtr.Zero)
            {
                service.DoorOpen(doorId,Service.actionTimeS);
            }
            
            if(service.ret >= 0)
            {
                service.DisConnect();
                service.DoorOpen(doorId,Service.actionTimeS);
                msg.code=200;
                msg.msg="成功";
                msg.data=service.retData;
            }
            else{
                msg.code=400;
                msg.msg="失败";
                msg.data=service.retData;
            }
            
            return msg;
        }
    }
}
