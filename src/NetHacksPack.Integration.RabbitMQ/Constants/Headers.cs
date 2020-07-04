namespace NetHacksPack.Integration.RabbitMQ.Constants
{
    public static class Headers
    {
        public const string ATTEMPT = "x-message-attempt";
        public const string COMPRESSED = "x-message-compressed";
        public const string CURRENT_CHUNCK = "x-message-current-chunk";
        public const string ENCODING = "x-message-encoding";
        public const string FILEID = "x-message-fileid";
        public const string REQUEUE_COUNT = "x-requeue-count";
        public const string SENDER_TAG = "SenderTag";
        public const string TOTAL_CHUNCK = "x-message-total-chunk";
        public const string TTL = "x-message-ttl";
    }
}
