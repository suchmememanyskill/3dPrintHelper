using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using _3dPrintHelper.Service;
using Avalonia.Threading;

namespace _3dPrintHelper.ViewsExt
{
    public abstract class UserControlExt<T> : UserControl
    {
        public void SetControls()
        {
            Dictionary<string, Button> foundButtons = new();
            
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                foreach (var customAttribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (customAttribute is NamedControlAttribute attribute)
                    {
                        string? name = attribute.Name;
                        if (name == null)
                            name = propertyInfo.Name;

                        object value = this.FindNameScope().Find(name);
                        if (value is Button button)
                            foundButtons.Add(name, button);
                        
                        propertyInfo.SetValue(this, value);
                        break;
                    }
                }
            }

            foreach (var methodInfo in typeof(T).GetMethods())
            {
                foreach (var customAttribute in methodInfo.GetCustomAttributes(true))
                {
                    if (customAttribute is CommandAttribute attribute)
                    {
                        Button button;
                        if (foundButtons.ContainsKey(attribute.ButtonName))
                            button = foundButtons[attribute.ButtonName];
                        else
                        {
                            object value = this.FindNameScope().Find(attribute.ButtonName);
                            if (value is Button foundButton)
                                button = foundButton;
                            else
                                throw new InvalidDataException("Name is not a button");
                        }

                        if (methodInfo.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null)
                            button.Command = new LambdaCommand(x =>
                                Dispatcher.UIThread.Post(() => methodInfo.Invoke(this, Array.Empty<object?>())));
                        else
                            button.Command = new LambdaCommand(x => methodInfo.Invoke(this, Array.Empty<object?>()));
                    }
                }
            }
        }
    }
}