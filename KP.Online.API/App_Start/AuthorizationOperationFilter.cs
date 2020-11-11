using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace KP.Online.API.App_Start
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "authorization",
                @in = "header",
                description = "Bearer access token",
                @default = "Bearer AZPouPv8KDEuy658mlrOdY3Qe73WMkeQNchxUxSed48jR83OvUinFdn2FUWWJvc7",
                required = true,
                type = "string"
            });

            var airport_code = operation.parameters.FirstOrDefault(x => x.name == "airport_code");
            if (airport_code != null)
            {
                airport_code.description = "รหัสสนามบิน";
            }

            var flight_code = operation.parameters.FirstOrDefault(x => x.name == "flight_code");
            if (flight_code != null)
            {
                flight_code.description = "รหัสเที่ยวบิน";
            }

            var flight_date = operation.parameters.FirstOrDefault(x => x.name == "flight_date");
            if (flight_date != null)
            {
                flight_date.description = "เที่ยวบิน (วันที่)";
            }

            var passport = operation.parameters.FirstOrDefault(x => x.name == "passport");
            if (passport != null)
            {
                passport.description = "หมายเลขหนังสือเดินทาง";
            }

            var order_no = operation.parameters.FirstOrDefault(x => x.name == "order_no");
            if (order_no != null)
            {
                order_no.description = "รหัสการสั่งซื้อสินค้า";
            }

            var skip = operation.parameters.FirstOrDefault(x => x.name == "skip");
            if (skip != null)
            {
                skip.description = "เริ่มต้นข้อมูลที่";
                skip.@default = "0";                                                                                                                                                                                                                     
            }

            var take = operation.parameters.FirstOrDefault(x => x.name == "take");
            if (take != null)
            {
                take.description = "จำนวนข้อมูลที่ต้องการให้แสดง";
                take.@default = "20";
            }

            var order = operation.parameters.FirstOrDefault(x => x.name == "order");
            if (order != null)
            {
                order.description = "object ข้อมูลในการบันทึก";
            }

            //throw new NotImplementedException();
        }
    }
}