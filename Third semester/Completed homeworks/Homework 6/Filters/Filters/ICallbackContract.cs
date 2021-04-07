using System.ServiceModel;

namespace Filters
{
    public interface ICallbackContract
    {
        [OperationContract(IsOneWay = true)]
        void ImageCallback(byte[] bytes);

        [OperationContract(IsOneWay = true)]
        void ProgressCallback(int progress);
    }
}