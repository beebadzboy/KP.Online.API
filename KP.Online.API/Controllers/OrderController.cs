using KP.Common.Return;
using KP.Online.API.Authen;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

using System.Linq;
using System.Globalization;
using System.Web.ModelBinding;
using System.Xml.Serialization;
using Newtonsoft;
using KP.Online.API.Models;

using ServiceStack;
using Newtonsoft.Json;
using KP.Online.API.SaleOrder_WebService;

namespace KP.Online.API.Controllers
{
    [RoutePrefix("api/online")]
    public class OrderController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("check-purchase-rights")]
        [ResponseType(typeof(ReturnObject<Models.SaleAmountByPassport>))]
        public IHttpActionResult CheckAllowSaleOnline(string airport_code, string flight_code, string flight_date, string passport)
        {
            ReturnObject<Models.SaleAmountByPassport> ret = new ReturnObject<Models.SaleAmountByPassport>();
            try
            {

                if (string.IsNullOrEmpty(airport_code))
                {
                    throw new ArgumentException("airport code is missing.", airport_code);
                }

                if (string.IsNullOrEmpty(flight_code))
                {
                    throw new ArgumentException("flight code is missing.", nameof(flight_code));
                }

                if (string.IsNullOrEmpty(passport))
                {
                    throw new ArgumentException("passport is missing.", nameof(passport));
                }

                if (string.IsNullOrEmpty(flight_date))
                {
                    throw new ArgumentException("flight date is missing.", nameof(flight_date));
                }

                var omSrv = new SaleOrderService();
                var data = omSrv.ValidateAllowSaleOnline(airport_code, passport, flight_date, flight_code);
                ret.Data = new Models.SaleAmountByPassport(data);
                ret.totalCount = ret.Data != null ? 1 : 0;
                ret.isCompleted = true;
            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }
            return Ok(ret);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpPost]
        [System.Web.Http.Route("save-order")]
        [ResponseType(typeof(ReturnObject<Models.OrderSession>))]
        public IHttpActionResult SaveOrderOnline(Models.OrderHeader order)
        {
            // validate data
            ReturnObject<Models.OrderSession> ret = new ReturnObject<Models.OrderSession>();
            try
            {
                var omSrv = new SaleOrderService();
                var input = order.ConvertTo<SaleOrder_WebService.OrderHeader>().ToJson();
                var data = omSrv.SaveOrderOnlineJson(input);
                ret.Data = data.ConvertTo<Models.OrderSession>();
                if (ret.Data == null)
                {
                    throw new ArgumentException("message", "connection error");
                }
   
                ret.totalCount = ret.Data != null ? 1 : 0;
                ret.isCompleted = true;

            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }

            return Ok(ret);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("hold-order")]
        [ResponseType(typeof(ReturnObject<Models.OrderSession>))]
        public IHttpActionResult HoldOrderOnline(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<Models.OrderSession> ret = new ReturnObject<Models.OrderSession>();
            try
            {
                var omSrv = new SaleOrderService();
                var data = omSrv.HoleOrderOnline(order_no);
                ret.Data = new Models.OrderSession(data);
                ret.totalCount = ret.Data != null ? 1 : 0;
                ret.isCompleted = true;
            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }


            return Ok(ret.Data);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("cancel-order")]
        [ResponseType(typeof(ReturnObject<Models.OrderSession>))]
        public IHttpActionResult CancelOrderOnline(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<Models.OrderSession> ret = new ReturnObject<Models.OrderSession>();

            try
            {
                var omSrv = new SaleOrderService();
                var data = omSrv.CancelOrderOnline(order_no);
                ret.Data = new Models.OrderSession(data);
                if (ret.Data == null)
                {
                    throw new ArgumentException("message", "connection error");
                }
                ret.totalCount = ret.Data != null ? 1 : 0;
                ret.isCompleted = true;

            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }


            return Ok(ret);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("get-order")]
        [ResponseType(typeof(ReturnObject<Models.OrderSession>))]
        public IHttpActionResult GetOrderOnline(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<Models.OrderSession> ret = new ReturnObject<Models.OrderSession>();

            try
            {
                var omSrv = new SaleOrderService();
                var data = omSrv.GetOrderOnline(order_no);
                ret.Data = new Models.OrderSession(data);
                if (ret.Data == null)
                {
                    throw new ArgumentException("message", "connection error");
                }
                ret.totalCount = ret.Data != null? 1 : 0;
                ret.isCompleted = true;
            }
            catch (Exception e)
            {
                ret.SetMessage(e);
                ret.Tracking = new ReturnTracking();
            }


            return Ok(ret);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("get-order-list")]
        [ResponseType(typeof(ReturnObject<List<Models.OrderSession>>))]
        public IHttpActionResult GetOrderOnlineList(string airport_code, int? skip, int? take)
        {
            if (string.IsNullOrWhiteSpace(airport_code))
            {
                throw new ArgumentException("message", nameof(airport_code));
            }

            ReturnObject<List<Models.OrderSession>> ret = new ReturnObject<List<Models.OrderSession>>();

            try
            {
                var srv = new SaleOrderService();
                var data = srv.GetOrderOnlineList(airport_code, (int)skip, false, (int)take, false).ToList();
                var newList = new List<Models.OrderSession>();
                foreach (var item in data)
                {
                    newList.Add(new Models.OrderSession(item));
                }
                ret.Data = newList;
                ret.totalCount = ret.Data.Count();
                ret.isCompleted = true;
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
