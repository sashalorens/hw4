namespace hw4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var FE = new FEHandler();
            var BE = new BEHandler();
            var QA = new QAHandler();

            FE.SetNext(BE).SetNext(QA);
            Client.Init(FE);
        }
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler hanler);
        object? Handle(object operation);
    }

    public abstract class Handler : IHandler
    {
        private IHandler? next;

        public virtual object? Handle(object operation)
        {
            if (next == null) return null;
            return next.Handle(operation);
        }

        public IHandler SetNext(IHandler handler)
        {
            next = handler;
            return handler;
        }
    }

    public class FEHandler: Handler
    {
        public override object? Handle(object operation)
        {
            if ((operation as string) == "Use the new API to render data")
            {
                return $"FE: Fine, what payload I should send?";
            }
            return base.Handle(operation);
        }
    }

    public class BEHandler: Handler
    {
        public override object? Handle(object operation)
        {
            if ((operation as string) == "Implement a new endpoint")
            {
                return $"BE: Okay, what response model do you need?";
            }
            return base.Handle(operation);
        }
    }

    public class QAHandler: Handler
    {
        public override object? Handle(object operation)
        {
            if ((operation as string) == "Start regression testing")
            {
                return $"QA: Yup, we'll start tomorrow";
            }
                return base.Handle(operation);
        }
    }

    public class Client
    {
        public static void Init(Handler handler)
        {
            foreach (string task in new List<string> { "Make a design of the new screen", "Implement a new endpoint", "Use the new API to render data", "Start regression testing" })
            {
                var result = handler.Handle(task);
                if (result != null) Console.WriteLine($"{result}");
                else Console.WriteLine($"Oops, we need to hire someone who can {task}");
            }
        }
    }
}
