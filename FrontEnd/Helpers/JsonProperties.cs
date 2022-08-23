namespace FrontEnd.Helpers
{
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public partial class JsonProperties
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string? TokenType { get; set; }
        [JsonProperty("app_id")]
        public string? AppId { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

    }
    public partial class PaypalOrderDetails
    {
        [JsonProperty("intent")]
        public string? Intent { get; set; }

        [JsonProperty("purchase_units")]
        public PurchaseUnit[]? PurchaseUnits { get; set; }

        [JsonProperty("application_context")]
        public ApplicationContext? ApplicationContext { get; set; }

    }
    public partial class ApplicationContext
    {
        [JsonProperty("return_url")]
        public string ReturnUrl { get; set; }

        [JsonProperty("cancel_url")]
        public string CancelUrl { get; set; }
    }

    public partial class PurchaseUnit
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }
    }
    public partial class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("unit_amount")]
        public UnitAmount UnitAmount { get; set; }
    }

    public partial class Amount
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }
    }

    public partial class Breakdown
    {
        [JsonProperty("item_total")]
        public ItemTotal ItemTotal { get; set; }
        [JsonProperty("discount")]
        public ItemTotal Discount { get; set; }
        [JsonProperty("shipping")]
        public ItemTotal Shipping { get; set; }
    }

    public partial class ItemTotal
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public partial class UnitAmount
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public partial class PaypalResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("create_time")]
        public DateTimeOffset CreateTime { get; set; }
        [JsonProperty("links")]
        public List<Link> Links { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public partial class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }
    public partial class CurrencyExchange
    {
        [JsonProperty("compra")]
        public string? Compra { get; set; }
        [JsonProperty("venta")]
        public string? Venta { get; set; }
        [JsonProperty("fecha")]
        public string? Fecha { get; set; }

    }
}


