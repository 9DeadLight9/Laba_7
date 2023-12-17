using System;
using System.Collections.Generic;
using System.Threading;

public class Resource
{
    public string Name { get; set; }
    public SemaphoreSlim Semaphore { get; set; }
}

public class Program
{
    static void Main()
    {
        var resources = new List<Resource>
        {
            new Resource { Name = "CPU", Semaphore = new SemaphoreSlim(1, 1) },
            new Resource { Name = "RAM", Semaphore = new SemaphoreSlim(1, 1) },
            new Resource { Name = "Disk", Semaphore = new SemaphoreSlim(1, 1) }
        };

        var threads = new List<Thread>();
        for (int i = 0; i < 10; i++)
        {
            int threadId = i;
            var thread = new Thread(() =>
            {
                foreach (var resource in resources)
                {
                    Console.WriteLine($"Thread {threadId} waiting for {resource.Name}");
                    resource.Semaphore.Wait();
                    Console.WriteLine($"Thread {threadId} using {resource.Name}");
                    Thread.Sleep(1000); // Simulate work
                    Console.WriteLine($"Thread {threadId} done with {resource.Name}");
                    resource.Semaphore.Release();
                }
            });
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}