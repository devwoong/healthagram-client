using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using healthagram.CustomView;
[assembly: ExportRenderer(typeof(PlaceHolderEditor), typeof(PlaceholderEditorRenderer))]
namespace healthagram.CustomView
{
    public class PlaceholderEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            var element = (PlaceHolderEditor)Element;

            Control.Hint = element.Placeholder;
            Control.SetHintTextColor(element.PlaceholderColor.ToAndroid());
        }
    }
}
