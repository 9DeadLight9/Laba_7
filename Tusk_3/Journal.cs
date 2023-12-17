using System.Collections.Concurrent;

public class Operation
{
    public DateTime Timestamp { get; set; }
    public string ThreadId { get; set; }
    public string Resource { get; set; }
}

public class ConflictJournal
{
    private ConcurrentDictionary<string, Operation> operations = new ConcurrentDictionary<string, Operation>();
    private object lockObject = new object();

    public void LogOperation(string resource)
    {
        var operation = new Operation
        {
            Timestamp = DateTime.Now,
            ThreadId = Thread.CurrentThread.ManagedThreadId.ToString(),
            Resource = resource
        };

        lock (lockObject)
        {
            if (operations.ContainsKey(resource))
            {
                var lastOperation = operations[resource];
                if ((operation.Timestamp - lastOperation.Timestamp).TotalMilliseconds < 1000)
                {
                    Console.WriteLine($"Conflict detected on resource {resource} between operations from thread {lastOperation.ThreadId} and thread {operation.ThreadId}");
                    
                }
            }

            operations[resource] = operation;
        }
    }
}