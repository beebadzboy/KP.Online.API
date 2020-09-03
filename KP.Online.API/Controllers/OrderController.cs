using KP.Common.Return;
using KP.Online.API.Authen;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KP.Online.API.Other_WebService;
using System.Linq;
using KP.Online.API.Order_WebService;
using System.Globalization;

namespace KP.Online.API.Controllers
{
    [RoutePrefix("api/online")]
    public class OrderController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [Route("check-purchase-rights")]
        [ResponseType(typeof(ReturnObject<SaleAmountByPassport>))]
        public IHttpActionResult CheckAllowSaleOnline(string airport_code, string flight_code, string flight_date, string passport)
        {
            ReturnObject<SaleAmountByPassport> ret = new ReturnObject<SaleAmountByPassport>();
            try
            {

                if (string.IsNullOrEmpty(airport_code))
                {
                    throw new ArgumentException("message", nameof(airport_code));
                }

                if (string.IsNullOrEmpty(flight_code))
                {
                    throw new ArgumentException("message", nameof(flight_code));
                }

                if (string.IsNullOrEmpty(passport))
                {
                    throw new ArgumentException("message", nameof(passport));
                }

                DateTime flight_datetime = Convert.ToDateTime(flight_date);
                if (string.IsNullOrEmpty(flight_date))
                {
                    throw new ArgumentException("message", nameof(flight_date));
                }
                else
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    flight_datetime = DateTime.ParseExact(flight_date, "yyyy-MM-dd", provider);
                }

                var omSrv = new SaleOrderService();
                ret.Data = omSrv.ValidateAllowSaleOnline(airport_code, passport, flight_datetime, false, flight_code);
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
        [Route("save-order")]
        [ResponseType(typeof(ReturnObject<OrderSession>))]
        public IHttpActionResult SaveOrderOnline(OrderHeader order)
        {
            // validate data
            ReturnObject<OrderSession> ret = new ReturnObject<OrderSession>();
            try
            {
                var omSrv = new SaleOrderService();
                ret.Data = omSrv.SaveOrderOnline(order);
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
        [Route("hold-order")]
        [ResponseType(typeof(ReturnObject<OrderSession>))]
        public IHttpActionResult HoldOrderOnline(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<OrderSession> ret = new ReturnObject<OrderSession>();
            try
            {
                var omSrv = new SaleOrderService();
                ret.Data = omSrv.HoleOrderOnline(order_no);
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
        [Route("cancel-order")]
        [ResponseType(typeof(ReturnObject<OrderSession>))]
        public IHttpActionResult CancelOrderOnline(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<OrderSession> ret = new ReturnObject<OrderSession>();

            try
            {
                var omSrv = new SaleOrderService();
                ret.Data = omSrv.CancelOrderOnline(order_no);
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
        [Route("get-order")]
        [ResponseType(typeof(ReturnObject<OrderSession>))]
        public IHttpActionResult GetOrderOnline(string order_no)
        {
            if (string.IsNullOrWhiteSpace(order_no))
            {
                throw new ArgumentException("message", nameof(order_no));
            }

            ReturnObject<OrderSession> ret = new ReturnObject<OrderSession>();

            try
            {
                var omSrv = new SaleOrderService();
                ret.Data = omSrv.GetOrderOnline(order_no);
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
        [ResponseType(typeof(ReturnObject<List<OrderSession>>))]
        public IHttpActionResult GetOrderOnlineList(string airport_code, int? skip, int? take)
        {
            if (string.IsNullOrWhiteSpace(airport_code))
            {
                throw new ArgumentException("message", nameof(airport_code));
            }

            ReturnObject<List<OrderSession>> ret = new ReturnObject<List<OrderSession>>();

            try
            {
                var srv = new SaleOrderService();
                ret.Data = srv.GetOrderOnlineList(airport_code, (int)skip,false, (int)take,false).ToList();
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
