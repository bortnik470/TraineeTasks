using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace ServiceTest
{
    internal class MessageInterception : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine(reply.ToString());
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Console.WriteLine(request.ToString());

            return null;
        }
    }
}
