using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTests.Helpers
{
    internal class Response<TValue>
    {
        public Response(ActionResult<TValue> result)
        {
            SetData(result);
        }

        public TValue Value { get; set; }

        public int? StatusCode { get; set; }

        private void SetData(ActionResult<TValue> result)
        {
            Value = result.Value;
            StatusCode = (result.Result as ObjectResult).StatusCode;
        }
    }
}
