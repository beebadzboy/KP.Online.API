using KP.Common.Return;
using KP.Online.API.Authen;
using KP.Online.API.Other_WebService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KP.Online.API.Controllers
{
    [RoutePrefix("api/queue")]
    public class SaleQueueController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        [Route("sale-by-sku")]
        [ResponseType(typeof(ReturnObject<List<SaleQueue>>))]
        public IHttpActionResult SaleQueueOnline(string airport_code, char terminal)
        {

            ReturnObject<List<SaleQueue>> ret = new ReturnObject<List<SaleQueue>>();
            ret.Data = new List<SaleQueue>();

            try
            {
                if (string.IsNullOrWhiteSpace(airport_code))
                {
                    throw new ArgumentException("message", nameof(airport_code));
                }

                if (char.IsWhiteSpace(terminal))
                {
                    throw new ArgumentException("message", nameof(terminal));
                }

                var omSrv = new OtherService();
                ret.Data = omSrv.SaleQueueOnline(airport_code, terminal, true).ToList();
                ret.totalCount = ret.Data.Count();
                ret.isCompleted = true;
            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }

            return Ok(ret.Data);
        }
    }
}
