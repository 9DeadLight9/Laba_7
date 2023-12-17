class Program
{
    static void Main(string[] args)
    {
        var node1 = new DistributedSystemNode("Node1");
        var node2 = new DistributedSystemNode("Node2");
        var node3 = new DistributedSystemNode("Node3");

        node1.SendMessage(node2, "Hello from Node1!");
        node2.SendMessage(node3, "Hello from Node2!");
        node3.SendMessage(node1, "Hello from Node3!");

        Console.ReadLine();
    }
}