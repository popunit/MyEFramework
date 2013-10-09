
namespace eCommerce.Core
{
    public interface ITask : IOrderable
    {
        void Execute();
    }

    /// <summary>
    /// the tasks have nothing to do with database
    /// </summary>
    public interface IWarmupTask : ITask
    { 
    }
}
