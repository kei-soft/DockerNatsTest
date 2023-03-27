using System.Text;

using Microsoft.AspNetCore.Mvc;

using NATS.Client;

namespace WebAppNatsSerer.Controllers
{
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            Thread.Sleep(5000);

            ConnectionFactory cf = new ConnectionFactory();
            Options opts = ConnectionFactory.GetDefaultOptions();

            opts.Url = "nats://localhost:4222";

            IConnection c = cf.CreateConnection(opts);

            c.Publish("worker", Encoding.UTF8.GetBytes($"hello, world: {value}"));
            c.Close();

            return Ok();
        }
    }
}
