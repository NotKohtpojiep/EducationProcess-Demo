using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EducationProcess.Desktop.Helpers
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            var itemType = item.GetType();

            var resourceDictonary = new ResourceDictionary
            {
                Source = new Uri(string.Format(
                    "pack://application:,,,/{0};component/Views/{1}.xaml",
                    itemType.Assembly.FullName,
                    itemType.Name.Replace("ViewModel", string.Empty)))
            };
            return resourceDictonary
                .Values
                .OfType<DataTemplate>()
                .SingleOrDefault(_ => ReferenceEquals(_.DataType, itemType));
        }
    }
}
