namespace EventBus.Messages.Common
{
    public static class EventBusConstants
	{
		public const string OrderCheckoutQueue = "order-checkout-queue";
		public const string CatalogQueue = "catalog-queue";
		public const string BasketCheckoutEndPoint = "EventBus.Messages.Events:BasketCheckoutEvent";
		public const string OrderCheckoutEndPoint = "EventBus.Messages.Events:OrderCheckoutEvent";
		public const string BillingQueue = "EventBus.Messages.Events:BillingEvent";

		public const string CatalogEndPoint = "EventBus.Messages.Events:CatalogEndPoint";
		public const string BillingEndPoint = "EventBus.Messages.Events:BillingEndPoint";
	}
}
