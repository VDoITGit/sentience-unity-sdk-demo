using Sentience.WaaS;

namespace Sentience.Relayer
{
    public interface IQueueableTransaction
    {
        public Transaction BuildTransaction();
        public string ToString();
    }
}