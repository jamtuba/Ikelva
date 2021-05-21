namespace Ikelva.ClassLibrary.Helpers
{
    public static class EndPoints
    {
        public static string ExchangeName { get; set; } = "Ikelva_exchange";
        public static string WebsiteQueueName { get; set; } = "Website_queue";
        public static string WebsiteRoutingKey{ get; set; } = "From_website";
        public static string ToBankQueueName { get; set; } = "To_bank_queue";
        public static string ToBankRoutingKey { get; set; } = "To_bank";
        public static string FromBankQueueName { get; set; } = "From_bank_queue";
        public static string FromBankRoutingKey{ get; set; } = "From_bank";
        public static string BankResponseQueueName { get; set; } = "Bank_response_queue";
        public static string BankResponseRoutingKey { get; set; } = "Bank_response";
        public static string InvalidQueueName { get; set; } = "Invalid_queue";
        public static string InvalidRoutingKey { get; set; } = "Invalid";

    }
}
