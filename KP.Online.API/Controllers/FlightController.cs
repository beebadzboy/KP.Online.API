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
using ServiceStack;
using System.Globalization;

namespace KP.Online.API.Controllers
{
    //[Authorize(Roles = "SuperAdmin, Admin, User")]
    [RoutePrefix("api/flight")]
    public class FlightController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("validate-code")]
        [ResponseType(typeof(ReturnObject<Models.Flight>))]
        public IHttpActionResult ValidateFlights(string flight_code)
        {
            ReturnObject<Models.Flight> ret = new ReturnObject<Models.Flight>();
            ret.Data = new Models.Flight();

            try
            {
                CultureInfo ci = CultureInfo.InvariantCulture;
                CultureInfo _cultureEnInfo = new CultureInfo("en-US");
                var date = DateTime.Now.Date.ToString("yyyy/MM/dd", ci);
                var date2 = DateTime.Now.Date.ToString("yyyy/MM/dd", _cultureEnInfo);


                //if (string.IsNullOrEmpty(flight_code))
                //{
                //    throw new ArgumentException("message", nameof(flight_code));
                //}

                //var srv = new OtherService();
                //var data = srv.CheckFlights(flight_code);
                //ret.Data = data.ConvertTo<Models.Flight>();
                //ret.totalCount = 1;
                //ret.isCompleted = true;
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
        [System.Web.Http.Route("check-flight")]
        [ResponseType(typeof(ReturnObject<Models.Flight>))]
        public IHttpActionResult CheckFlights(string flight_code, string flight_date)
        {
            ReturnObject<Models.Flight> ret = new ReturnObject<Models.Flight>();
            ret.Data = new Models.Flight();

            try
            {
                if (string.IsNullOrEmpty(flight_code))
                {
                    throw new ArgumentException("flight code", nameof(flight_code));
                }

                if (string.IsNullOrEmpty(flight_date))
                {
                    throw new ArgumentException("flight date", nameof(flight_date));
                }


                var srv = new OtherService();
                var data = srv.CheckFlightsBy(flight_code, flight_date);
                ret.Data = data.ConvertTo<Models.Flight>();
                ret.totalCount = 1;
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
        [System.Web.Http.Route("all")]
        [ResponseType(typeof(ReturnObject<Models.FlightsAll>))]
        public IHttpActionResult GetAll()
        {
            ReturnObject<Models.FlightsAll> ret = new ReturnObject<Models.FlightsAll>();
            ret.Data = new Models.FlightsAll();

            try
            {
                var srv = new OtherService();
                var data = srv.GetDataAll(); 
                ret.Data = data.ConvertTo<Models.FlightsAll>();

                int d = ret.Data.Departure.Count();
                int a = ret.Data.Arrival.Count();
                int z = ret.Data.Transfer.Count();

                ret.totalCount = d + a + z;
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
        [System.Web.Http.Route("arrival")]
        [ResponseType(typeof(ReturnObject<List<Models.Flight>>))]
        public IHttpActionResult GetArrival()
        {
            ReturnObject<List<Models.Flight>> ret = new ReturnObject<List<Models.Flight>>();
            ret.Data = new List<Models.Flight>();

            try
            {
                var srv = new OtherService();
                var data = srv.GetDataArrival().ToList();
                ret.Data = data.ConvertTo<List<Models.Flight>>();
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

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("departure")]
        [ResponseType(typeof(ReturnObject<List<Models.Flight>>))]
        public IHttpActionResult GetDeparture()
        {
            ReturnObject<List<Models.Flight>> ret = new ReturnObject<List<Models.Flight>>();
            ret.Data = new List<Models.Flight>();

            try
            {
                var srv = new OtherService();
                var data = srv.GetDataDeparture().ToList();
                ret.Data = data.ConvertTo<List<Models.Flight>>();
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

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin, User, SuperAdmin")]
        [HttpGet]
        [System.Web.Http.Route("transfer")]
        [ResponseType(typeof(ReturnObject<List<Models.Flight>>))]
        public IHttpActionResult GetTransfer()
        {
            ReturnObject<List<Models.Flight>> ret = new ReturnObject<List<Models.Flight>>();
            ret.Data = new List<Models.Flight>();

            try
            {
                var srv = new OtherService();
                var data = srv.GetDataTransfer().ToList();
                ret.Data = data.ConvertTo<List<Models.Flight>>();
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
