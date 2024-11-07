using Newtonsoft.Json;
using SharedKernel.Utilities.Encryption.SymmetricEncryption;
using SharedKernel.Utilities.Logging.Services;

namespace FormBuilderService.EventSourcing.Events.Abstraction
{
    public class DomainEventMessage
    {
        public Guid EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public IEventType Event { get; set; }
        public string EncryptedData { get; }// Event data but in an encrypted form

        public string Source { get; set; } = default!; // Source system or service
        public string EventType { get; set; } = default!; // Type of event

        string encryptData(object data)
        {
            #region Default Keys Used for FormBuilderService.EventSourcing Encryption
            string EventSourcingKey = "ecb5b8c2731476856dfb2a4b4a4d6b2e09458e47fb78ac2956420c8a418a3ca3";
            string EventSourcingIV = "a8a2c7eb84f4969f9445eb0a08daed18";
            #endregion

            AESEncryption.SetKeyAndIV(EventSourcingKey, EventSourcingIV);

            string encryptedData = string.Empty;
            try
            {
                var jData = JsonConvert.SerializeObject(data);
                encryptedData = AESEncryption.Encrypt(jData);
            }
            catch (Exception ex)
            {
                LogService.LogError(ex);
            }
            return encryptedData;
        }

        public DomainEventMessage(IEventType @event, object data)
        {
            EventId = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Source = GetType().FullName?.Split('.')[0]!;
            EventType = @event.EventType;
            Event = @event;
            EncryptedData = encryptData(data);
        }
    }
}