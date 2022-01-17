using Application.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Utils
{
    public static class DropDownHelper<T> where T : IDropDownEntity
    {
        public static List<DropDownListItem> ConvertToDropDownSource(List<T> values, List<string> texts, string defaultValue, string defaultText)
        {
            if (values.Count != texts.Count)
                throw new NotSameSizeException("Provided collections have different sizes");

            var list = values.Select((t, i)
                => new DropDownListItem { Value = t.Id.ToString(), Text = texts[i] }).ToList();

            if (!string.IsNullOrEmpty(defaultValue) && !string.IsNullOrEmpty(defaultText))
                list.Insert(0, new DropDownListItem {Text = defaultText, Value = defaultValue});

            return list;
        }
    }
}