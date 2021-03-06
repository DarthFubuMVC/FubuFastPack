using FubuLocalization;
using FubuMVC.Core.Ajax;
using FubuValidation;
using System.Collections.Generic;

namespace FubuFastPack
{
    public static class AjaxContinuationExtensions
    {
        public const string ShowPage = "showPage";
        public const string NavigatePage = "navigatePage";
        public const string ShowDialog = "showDialog";
        public const string NewValueToDisplay = "NewValueToDisplay";

        public static AjaxContinuation WithSubmission(this AjaxContinuation ajaxContinuation, Notification notification)
        {
            ajaxContinuation.Success = notification.IsValid();
            ajaxContinuation.AddErrors(notification.ToValidationErrors());            
            return ajaxContinuation;
        }

        public static AjaxContinuation ForDialog(string url)
        {
            var returnValue = new AjaxContinuation();
            returnValue[ShowDialog] = url;
            return returnValue;
        }

        public static AjaxContinuation ForMessage(StringToken key)
        {
            var returnValue = AjaxContinuation.ForMessage(key);
            return returnValue;
        }

        public static AjaxContinuation ForPage(string url)
        {
            var returnValue = new AjaxContinuation();
            returnValue[ShowPage] = url;
            return returnValue;
        }

        public static AjaxContinuation ForRefresh()
        {
            var returnValue = ForSuccess();
            returnValue.ShouldRefresh = true;
            return returnValue;
        }

        public static AjaxContinuation ForSuccess()
        {
            return new AjaxContinuation {Success = true};
        }

        public static AjaxContinuation AddErrors(this AjaxContinuation ajaxContinuation, ValidationError[] validationErrors)
        {
            if (validationErrors == null) return ajaxContinuation;
            validationErrors.Each(x => ajaxContinuation.AddError(x));
            return ajaxContinuation;
        }

        public static AjaxContinuation AddError(this AjaxContinuation ajaxContinuation, ValidationError validationError)
        {
            if (validationError == null) return ajaxContinuation;
            ajaxContinuation.AddError(new AjaxError{field = validationError.field, message = validationError.message});
            return ajaxContinuation;
        }

        public static AjaxContinuation AddError(this AjaxContinuation ajaxContinuation, AjaxError ajaxError)
        {
            if (ajaxError == null) return ajaxContinuation;
            ajaxContinuation.Errors.Add(ajaxError);
            return ajaxContinuation;
        }

        public static AjaxContinuation ForError(Notification notification)
        {
            var ajaxContinuation = new AjaxContinuation
            {
                Success = notification.IsValid()
            };
            ajaxContinuation.AddErrors(notification.ToValidationErrors());            
            return ajaxContinuation;
        }

        public static AjaxContinuation ForNavigateWholePage(string url)
        {
            var forNavigateWholePage = new AjaxContinuation
            {
                Success = true
            };
            forNavigateWholePage[NavigatePage] = url;
            return forNavigateWholePage;
        }

        public static object TryGetData(this AjaxContinuation ajaxContinuation, string key)
        {           
            object returnValue = null;
            if(ajaxContinuation.HasData(key))
            {
                returnValue = ajaxContinuation[key];
            }
            
            return returnValue;
        }
    }
}