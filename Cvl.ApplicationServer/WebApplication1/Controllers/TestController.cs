using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{  

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {        
        public TestController()
        {            
        }

        private static AsyncLocal<string> Secret = new AsyncLocal<string>();

        [Route("Test1")]
        public async Task<string> Test1(string request)
        {

            Secret.Value = DateTime.Now.Ticks.ToString();
            log(Secret.Value);

            log("1");
            await Task.Delay(500);
            log("2");
            await Task.Delay(500);
            log("3");

            await Test2();

            log(Secret.Value);
            return request;
        }

        private async Task Test2()
        {
            log("4");
            log(Secret.Value);
            Secret.Value = "Tee";
            await Task.Delay(500);
            log("4");
            log(Secret.Value);
        }

        private void log(string message)
        {
            Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId},{this.HttpContext.TraceIdentifier} - {message}");
        }
    }
}
