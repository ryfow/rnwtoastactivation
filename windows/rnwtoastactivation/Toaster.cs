using Microsoft.ReactNative.Managed;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace rnwtoastactivation
{
    [ReactModule("Toaster")]
    class Toaster
    {
        [ReactMethod("makeToast")]
        public void MakeToast(ReactPromise<bool> promise)
        {
            Debug.WriteLine("Making Toast");
            ShowToast("Id", "Some toast", "mmm, toasty!");
            promise.Resolve(true);
        }
        private static void ShowToast(string id, string subject, string body)
        {
            ToastContent toastContent = MakeContent(id, subject, body);

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml())
            {
                Tag = "foo"
            };
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
        static ToastContent MakeContent(string id, string subject, string body)
        {
            string title = subject;

            // Construct the visuals of the toast
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },
                        new AdaptiveText()
                        {
                            Text = body
                        },
                    },
                    AppLogoOverride = new ToastGenericAppLogo()
                    {
                        Source = "ms-appdata:///local/Andrew.jpg",
                        HintCrop = ToastGenericAppLogoCrop.Circle
                    }
                }
            };
            var qs = System.Web.HttpUtility.ParseQueryString(string.Empty);
            qs.Add("subject", subject);
            qs.Add("body", body);
            qs.Add("id", id);
            // Now we can construct the final toast content
            ToastContent toastContent = new ToastContent()
            {
                Launch = qs.ToString(),
                Visual = visual,
                Scenario = ToastScenario.Alarm,
                Actions = new ToastActionsCustom() { Buttons = { new ToastButton("Click Here!", qs.ToString()) } }


                // Arguments when the user taps body of toast
            };
            return toastContent;
        }
    }
}
