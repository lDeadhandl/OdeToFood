using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // Attribute routing
    // Used for specific controller routing 
    [Route("[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "519-641-7146";
        }

        public string Address()
        {
            return "beavey";
        }
    }
}
