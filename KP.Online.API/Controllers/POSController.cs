using KP.Common.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KP.Online.API.Authen;
using System.Web.Http.Description;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;
using KP.Online.API.Order_WebService;
using KP.Online.API.Other_WebService;

namespace KP.Online.API.Controllers
{
    [RoutePrefix("api/online")]
    public class POSController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        [Route("complete-order")]
        [ResponseType(typeof(ReturnObject<Models.OrderSession>))]
        public async Task<IHttpActionResult> CompleteOrderOnlineAsync(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<Models.OrderSession> ret = new ReturnObject<Models.OrderSession>();

            try
            {
                var srv = new SaleOrderService();
                var data = srv.CompleteOrderOnline(order_no);
                ret.Data = new Models.OrderSession(data);
                if (ret.Data != null)
                {
                    // send update to endpoint COMPLETED 
                    var endpoint = ConfigurationManager.AppSettings["KP_Return_KPC"];
                    var service = ConfigurationManager.AppSettings["KP_Service_KPC"];
                    var client = new RestClient(endpoint);
                    var request = new RestRequest(String.Format("api/Orders/{0}/Status", order_no), Method.POST);
                    //var request = new RestRequest(String.Format("{0}/api/Orders/{1}/Status", service, order_no), Method.POST);
                    var accessToken = ConfigurationManager.AppSettings["KP_Return_KPC_Token"];
                    request.AddHeader("AccessToken", accessToken);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddJsonBody(new { statuscode = "receivecomplete" });
                    #pragma warning disable CS0618 // Type or member is obsolete
                    var restResponse = await client.ExecutePostTaskAsync(request);
                    #pragma warning restore CS0618 // Type or member is obsolete
                    if (restResponse.ErrorException != null)
                    {
                        throw restResponse.ErrorException.InnerException;
                    }
                    else
                    {
                        if (restResponse.StatusCode != HttpStatusCode.OK)
                        {
                            var data2 = srv.UpdateStatusOrderOnline(order_no, restResponse.StatusCode.ToString());
                            ret.Data = new Models.OrderSession(data2);
                            ret.totalCount = 1;
                            ret.isCompleted = true;
                        }
                        else
                        {
                            var data3 = srv.UpdateStatusOrderOnline(order_no, "receivecomplete");
                            ret.Data = new Models.OrderSession(data3);
                            ret.totalCount = 1;
                            ret.isCompleted = true;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("message", "connection error");
                }
            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }


            return Ok(ret);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        [Route("void-order")]
        [ResponseType(typeof(ReturnObject<Models.OrderSession>))]
        public async Task<IHttpActionResult> VoidOrderOnlineAsync(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<Models.OrderSession> ret = new ReturnObject<Models.OrderSession>();

            try
            {
                var srv = new SaleOrderService();
                var data = srv.VoidOrderOnline(order_no);
                ret.Data = new Models.OrderSession(data);
                if (ret.Data != null)
                {
                    
                    // send update to endpoint CANCELED 
                    var endpoint = ConfigurationManager.AppSettings["KP_Return_KPC"];
                    var service = ConfigurationManager.AppSettings["KP_Service_KPC"];
                    var client = new RestClient(endpoint);
                    var request = new RestRequest(String.Format("api/Orders/{0}/Status", order_no), Method.POST);
                    //var request = new RestRequest(String.Format("{0}/api/Orders/{1}/Status", service, order_no), Method.POST);
                    var accessToken = ConfigurationManager.AppSettings["KP_Return_KPC_Token"];
                    request.AddHeader("AccessToken", accessToken);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddJsonBody(new { statuscode = "refund" });
                    #pragma warning disable CS0618 // Type or member is obsolete
                    var restResponse = await client.ExecutePostTaskAsync(request);
                    #pragma warning restore CS0618 // Type or member is obsolete
                    if (restResponse.ErrorException != null)
                    {
                        throw restResponse.ErrorException.InnerException;
                    }
                    else
                    {
                        if (restResponse.StatusCode != HttpStatusCode.OK)
                        {
                            var data2 = srv.UpdateStatusOrderOnline(order_no, restResponse.StatusCode.ToString());
                            ret.Data = new Models.OrderSession(data2);
                            ret.totalCount = 0;
                            ret.isCompleted = false;
                        }
                        else
                        {
                            var data3 = srv.UpdateStatusOrderOnline(order_no, "refund");
                            ret.Data = new Models.OrderSession(data3);
                            ret.totalCount = 1;
                            ret.isCompleted = true;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("message", "connection error");
                }
            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }

            return Ok(ret);
        }
    }
}
