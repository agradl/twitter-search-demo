using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace TwitterSearchDemo.Framework.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumRadioAttribute : ValidationAttribute, IMetadataAware
    {
        public EnumRadioAttribute(Type enumType)
        {
            EnumType = enumType;
            ErrorMessage = "The submitted value for {0} was not in the list of expected options";
        }

        public Type EnumType { get; set; }

        #region IMetadataAware Members

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.TemplateHint = "EnumRadio";
            IEnumerable<SelectListItem> selectItems = GetValues();
            metadata.AdditionalValues["EnumValues"] = selectItems;
        }

        #endregion

        private IEnumerable<SelectListItem> GetValues()
        {
            return Enum
                .GetValues(EnumType)
                .Cast<object>()
                .Select(x => new SelectListItem { Text = GetDisplayName(x), Value = x.ToString() });
        }

        private string GetDisplayName(object obj)
        {
            var member = EnumType.GetMember(obj.ToString())[0];
            var desc = member.GetCustomAttributes(typeof (DescriptionAttribute), false).FirstOrDefault();
            return desc != null ? ((DescriptionAttribute) desc).Description : obj.ToString();
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
                return true;
            return GetValues().Any(x => x.Value == value as string);
        }
    }
}