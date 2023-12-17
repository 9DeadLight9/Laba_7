using System.Collections.Concurrent;
public class DistributedSystemNode
{
    public enum NodeStatus { Active, Inactive }

    private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
    private NodeStatus status;

    public string NodeId { get; private set; }

    public DistributedSystemNode(string nodeId)
    {
        NodeId = nodeId;
        status = NodeStatus.Inactive;
    }

    public void SendMessage(DistributedSystemNode node, string message)
    {
        node.ReceiveMessage(message);
    }

    public void ReceiveMessage(string message)
    {
        messageQueue.Enqueue(message);
        Task.Run(() => ProcessMessages());
    }

    private async Task ProcessMessages()
    {
        status = NodeStatus.Active;
        NotifyStatus();

        while (messageQueue.TryDequeue(out string message))
        {
            await Task.Delay(1000); 
            Console.WriteLine($"Node {NodeId} processed message: {message}");
        }

        status = NodeStatus.Inactive;
        NotifyStatus();
    }

    private void NotifyStatus()
    {
        Console.WriteLine($"Node {NodeId} is now {status}");
    }
}