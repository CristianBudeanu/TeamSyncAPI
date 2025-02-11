using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSync.Helpers.HttpContextHelper
{
    public interface IHttpContextService
    {
        public string GetUsernameFromToken();
    }
}
