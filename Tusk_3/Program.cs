using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var journal = new ConflictJournal();

        var task1 = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                journal.LogOperation("Resource1");
                Thread.Sleep(100);
            }
        });

        var task2 = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                journal.LogOperation("Resource1");
                Thread.Sleep(150);
            }
        });

        Task.WaitAll(task1, task2);
    }
}