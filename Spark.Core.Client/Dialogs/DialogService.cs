using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Client.Dialogs
{
    public class DialogService : IDialogService
    {
        private readonly IJSRuntime js;

        public DialogService(IJSRuntime js)
        {
            this.js = js;
        }

        public async ValueTask ShowSuccess(string message)
        {
            await TextDialog("Success", message, MessageType.Success);
        }

        public async ValueTask ShowError(string message)
        {
            await TextDialog("Error", message, MessageType.Error);
        }

        public async ValueTask ShowInfo(string message)
        {
            await TextDialog("Info", message, MessageType.Info);
        }

        private async ValueTask TextDialog(string title,
            string message, MessageType type)
        {
            await js.InvokeVoidAsync("Swal.fire", title, message, type.ToSwalTypeString());
        }
    }

    internal enum MessageType
    {
        Info,
        Warning,
        Error,
        Success,
        Question
    }

    internal static class MessageTypeExtensions
    {
        internal static string ToSwalTypeString(this MessageType messageType)
        {
            return messageType.ToString().ToLower();
        }
    }
}
