using Microsoft.AspNetCore.Mvc;

namespace SHP.AuthorizationServer.Tests.Helpers
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
