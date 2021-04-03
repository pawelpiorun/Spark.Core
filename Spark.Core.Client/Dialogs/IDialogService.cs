using System.Threading.Tasks;

namespace Spark.Core.Client.Dialogs
{
    public interface IDialogService
    {
        ValueTask ShowError(string message);
        ValueTask ShowInfo(string message);
        ValueTask ShowSuccess(string message);
    }
}
