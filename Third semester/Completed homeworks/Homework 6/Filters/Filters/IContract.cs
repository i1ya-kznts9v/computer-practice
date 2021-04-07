using System.ServiceModel;

namespace Filters
{
    [ServiceContract(CallbackContract = typeof(ICallbackContract))]
    public interface IContract
    {
        [OperationContract(IsOneWay = true)]
        void ApplyFilter(string filterType, byte[] bytes);

        [OperationContract]
        string[] GetAvailableFilters();
    }
}