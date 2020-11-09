using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using KP.Online.API.SaleOrder_WebService;
using Microsoft.Ajax.Utilities;
using ServiceStack;

namespace KP.Online.API.Models
{

    [DataContract]
    public class StatusUpdate
    {
        [DataMember]
        public string statuscode { get; set; }
    }

    [DataContract]
    public class SaleAmountByPassport
    {
        [DataMember]
        public decimal? SaleAmt { get; set; }

        [DataMember]
        public decimal? Alcohol { get; set; }

        [DataMember]
        public decimal? Tobacco { get; set; }

        public SaleAmountByPassport() { }

        public SaleAmountByPassport(SaleOrder_WebService.SaleAmountByPassport data) {

            this.SaleAmt = data.SaleAmt;
            this.Alcohol = data.Alcohol;
            this.Tobacco = data.Tobacco;
        }
    }

    [DataContract]
    public class SaleOnlineByPassport
    {
        [DataMember]
        public decimal SumSKU { get; set; }

        [DataMember]
        public decimal SumTotal { get; set; }

        [DataMember]
        public List<SaleOnlineByPassportDetail> Details { get; set; }
    }

    [DataContract]
    public class SaleOnlineByPassportDetail
    {
        [DataMember]
        public string SKU { get; set; }

        [DataMember]
        public string QTY { get; set; }

        [DataMember]
        public string Total { get; set; }
    }

    [DataContract]
    public class OrderHeader
    {
        [DataMember]
        public Order NewOrder { get; set; }

        [DataMember]
        public Flight Flight { get; set; }

        [DataMember]
        public Billing Billing { get; set; }

        [DataMember]
        public List<ItemSKU> Items { get; set; }

        [DataMember]
        public List<Payment> Payments { get; set; }

        public SaleOrder_WebService.OrderHeader ConvInput(OrderHeader order)
        {
            var inputOrder = new SaleOrder_WebService.OrderHeader();
            inputOrder.Billing = order.Billing.ConvertTo<SaleOrder_WebService.Billing>();
            inputOrder.Flight = order.Flight.ConvertTo<SaleOrder_WebService.Flight>();
            inputOrder.Items = order.Items.ConvertTo<SaleOrder_WebService.ItemSKU[]>();
            inputOrder.NewOrder = order.NewOrder.ConvertTo<SaleOrder_WebService.Order>();
            inputOrder.Payments = order.Payments.ConvertTo<SaleOrder_WebService.Payment[]>();

            return inputOrder;
        }
    }

    [DataContract]
    public class OrderSession
    {
        [DataMember]
        public Guid SessionGuid { get; set; }

        [DataMember]
        public long SessionId { get; set; }

        [DataMember]
        public string SaleOrderNo { get; set; }

        [DataMember]
        public string SaleOrderStatus { get; set; }

        [DataMember]
        public string POSSessionKey { get; set; }

        [DataMember]
        public string POSInvoiceNo { get; set; }

        [DataMember]
        public string POSOrderNo { get; set; }

        [DataMember]
        public string POSStatus { get; set; }

        public OrderSession() { }

        public OrderSession(SaleOrder_WebService.OrderSession session)
        {
            this.SessionGuid = new Guid(session.SessionGuid);
            this.SessionId = (long)session.SessionId;
            this.SaleOrderNo = session.SaleOrderNo;
            this.SaleOrderStatus = session.SaleOrderStatus;
            this.POSSessionKey = session.POSSessionKey;
            this.POSInvoiceNo = session.POSInvoiceNo;
            this.POSOrderNo = session.POSOrderNo;
            this.POSStatus = session.POSStatus;
        }

    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public string OrderNo { get; set; }

        [DataMember]
        public string DeliveryCost { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public string MemberID { get; set; }

        [DataMember]
        public string UserToken { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string PaymentNo { get; set; }

        [DataMember]
        public bool IsAcceptMedia { get; set; }

        [DataMember]
        public string WeChatID { get; set; }

        [DataMember]
        public string DutyTax { get; set; }

        [DataMember]
        public string AgentCode { get; set; }
    }

    [DataContract]
    public class Billing
    {
        [DataMember]
        public string BillAddress1 { get; set; }

        [DataMember]
        public string BillAddress2 { get; set; }

        [DataMember]
        public string BillAddress3 { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string District { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Province { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string PassportNo { get; set; }

        [DataMember]
        public string Birthday { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public string Telephone { get; set; }

        [DataMember]
        public string MobilePhone { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string PersonalTitle { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PhoneCountryCode { get; set; }

    }

    [DataContract]
    public class Ship
    {

        [DataMember]
        public string ShipAddress1 { get; set; }

        [DataMember]
        public string ShipAddress2 { get; set; }

        [DataMember]
        public string ShipAddress3 { get; set; }

        [DataMember]
        public string ShipStreet { get; set; }

        [DataMember]
        public string ShipDistrict { get; set; }

        [DataMember]
        public string ShipCity { get; set; }

        [DataMember]
        public string ShipProvince { get; set; }

        [DataMember]
        public string ShipPostalCode { get; set; }

        [DataMember]
        public string ShipCountryCode { get; set; }

        [DataMember]
        public string ShipCountryName { get; set; }

        [DataMember]
        public string ShipTelephone { get; set; }

        [DataMember]
        public string ShipMobile { get; set; }

        [DataMember]
        public string ShipEmail { get; set; }

        [DataMember]
        public string ShipFax { get; set; }

        [DataMember]
        public string LogisticID { get; set; }

        [DataMember]
        public string ShipPersonalTitle { get; set; }

        [DataMember]
        public string ShipFirstName { get; set; }

        [DataMember]
        public string ShipLastName { get; set; }

        [DataMember]
        public string ShipPhoneCountryCode { get; set; }

        [DataMember]
        public string ShipLogisticName { get; set; }

        [DataMember]
        public string NationalID { get; set; }
    }

    [DataContract]
    public class ItemSKU
    {
        [DataMember]
        public string MaterialCode { get; set; }

        [DataMember]
        public double Quantity { get; set; }

        [DataMember]
        public decimal SellingPrice { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal DiscountRate { get; set; }

        [DataMember]
        public decimal DiscountPer { get; set; }

        [DataMember]
        public decimal Discount { get; set; }

        [DataMember]
        public string PromoCode { get; set; }

        [DataMember]
        public decimal Net { get; set; }

        [DataMember]
        public decimal SPDiscountRate { get; set; }

        [DataMember]
        public decimal SPDiscountPer { get; set; }

        [DataMember]
        public decimal SPDiscount { get; set; }

        [DataMember]
        public string SPPromoCode { get; set; }

        [DataMember]
        public decimal TotalDiscount { get; set; } // (DiscountPer + Discount) + (SPDiscountRate + SPDiscount) = TotalDiscount

        [DataMember]
        public decimal TotalNet { get; set; } // Net + SPDiscount = TotalNet

        [DataMember]
        public string SellerAdjustFee { get; set; }

        [DataMember]
        public string ord_no { get; set; }

        [DataMember]
        public string SubOrderType { get; set; }

        [DataMember]
        public string RecNo { get; set; }
    }

    [DataContract]
    public class Payment
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }

    [DataContract]
    public class SaleQueue
    {
        [DataMember]
        public string sku { get; set; }

        [DataMember]
        public decimal qty { get; set; }

        [DataMember]
        public List<DetailSaleQueue> details { get; set; }

        public SaleQueue(Other_WebService.SaleQueue data)
        {
            this.sku = data.sku;
            this.qty = data.qty;
        }
    }

    [DataContract]
    public class DetailSaleQueue
    {
        [DataMember]
        public DateTime Date { get; set; }


        [DataMember]
        public string MacNo { get; set; }

        [DataMember]
        public string DocNo { get; set; }

    }

    [DataContract]
    public class OrderKey
    {
        [DataMember]
        public string MacNo { get; set; }

        [DataMember]
        public string DocNO { get; set; }

        [DataMember]
        public string Date { get; set; }

    }

    [DataContract]
    public class FlightsAll
    {
        [DataMember]
        public List<Flight> Arrival { get; set; }

        [DataMember]
        public List<Flight> Departure { get; set; }

        [DataMember]
        public List<Flight> Transfer { get; set; }

    }

    [DataContract]
    public class Flight
    {
        [DataMember]
        public string FlightCode { get; set; }

        [DataMember]
        public string FlightDesc { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public string AirportCode { get; set; }

        [DataMember]
        public string AirlineCode { get; set; }

        [DataMember]
        public string AirlineName { get; set; }

        [DataMember]
        public string Terminal { get; set; }

        [DataMember]
        public PickUp PickUp { get; set; }

        [DataMember]
        public string DepartureAirport { get; set; }

        [DataMember]
        public string ArrivalAirport { get; set; }

        [DataMember]
        public FlightTime Time { get; set; }

        [DataMember]
        public List<FlightWeekDays> WeekDays { get; set; }

    }

    [DataContract]
    public class FlightCarrier
    {
        [DataMember]
        public string Fs { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FlightNumber { get; set; }
    }

    [DataContract]
    public class FlightTime
    {
        [DataMember]
        public string TimeAMPM { get; set; }

        [DataMember]
        public string Time24 { get; set; }
    }

    [DataContract]
    public class FlightWeekDays
    {
        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string FullName { get; set; }
    }

    [DataContract]
    public class PickUp
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Detail { get; set; }

        [DataMember]
        public string MapImg { get; set; }

    }
}