using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeter;
using ProtoBuf.Grpc.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebClientGrpc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            AboutViewModel model = new AboutViewModel();

            var channel = new Channel("localhost", 10042, ChannelCredentials.Insecure);
            try
            {
                var calculator = channel.CreateGrpcService<ICalculator>();
                var response = await calculator.MultiplyAsync(new MultiplyRequest() { X = 2, Y = 4 });
                if (response.Result != 8)
                {
                    throw new InvalidOperationException();
                }
                model.Result = response.Result;

                //model.Message = greeterResponse.Message;
            }
            finally
            {
                await channel.ShutdownAsync();
            }
            return View(model);
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";
            AboutViewModel model = new AboutViewModel();

            var channel = new Channel("localhost", 10042, ChannelCredentials.Insecure);
            try
            {
                var calculator = channel.CreateGrpcService<SharedStandard.ICalculator>();
                var response = await calculator.MultiplyAsync(new SharedStandard.MultiplyRequest() { X = 2, Y = 4 });
                if (response.Result != 8)
                {
                    throw new InvalidOperationException();
                }
                model.Result = response.Result;
            }
            finally
            {
                await channel.ShutdownAsync();
            }

            return View(model);
        }
    }
}