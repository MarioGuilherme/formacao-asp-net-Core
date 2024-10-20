namespace DevFreela.Core.MessageBus;

public interface IMessageBusService {
    void Publish(string queue, byte[] message);
}