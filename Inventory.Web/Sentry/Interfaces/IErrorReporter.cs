using System;
using System.Threading.Tasks;

namespace Inventory.Web.Sentry.Interfaces
{
    public interface IErrorReporter
    {
        Task CaptureAsync(Exception exception);
        Task CaptureAsync(string message);
    } 
}
