
using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using healthagram.CustomView;

[assembly: ExportRenderer(typeof(PlaceHolderEditor), typeof(PlaceholderEditorRenderer))]
namespace healthagram.CustomView
{
    public class PlaceholderEditorRenderer : EditorRenderer
    {
        private UILabel _placeholderLabel;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            //     CreatePlaceholderLabel((PlaceHolderEditor)Element, Control);

            //     Control.Ended += OnEnded;
            //     Control.Changed += OnChanged;
            SetPlaceholder((PlaceHolderEditor)Element, Control);

            Control.ScrollEnabled = false;
            Control.Started += (object sender, EventArgs args) => {
                if( Control.Text == ((PlaceHolderEditor)Element).Placeholder)
                {
                    Control.Text = "";
                    Control.TextColor = UIColor.Black;
                }
            };
            Control.Ended += (object sender, EventArgs args) => {
                PlaceHolderEditor placeHolder = (PlaceHolderEditor)Element;
                if(Control.Text == ((PlaceHolderEditor)Element).Placeholder | Control.Text == "")
                {
                    Control.Text = placeHolder.Placeholder;
                    Color c = placeHolder.PlaceholderColor;
                    Control.TextColor = new UIColor(new nfloat(c.R), new nfloat(c.G), new nfloat(c.B), new nfloat(c.A));
                }
            };
            Control.Changed += (object sender, EventArgs args) => {
                //Control.SizeToFit();
            };

        }

        private void CreatePlaceholderLabel(PlaceHolderEditor element, UITextView parent)
        {
            _placeholderLabel = new UILabel
            {
                Text = element.Placeholder,
                TextColor = element.PlaceholderColor.ToUIColor(),
                BackgroundColor = UIColor.Clear,
                Font = UIFont.FromName(element.FontFamily, (nfloat)element.FontSize)
            };
            _placeholderLabel.SizeToFit();

            parent.AddSubview(_placeholderLabel);

            //parent.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            //parent.AddConstraints(
            //    _placeholderLabel.AtLeftOf(parent, 7),
            //    _placeholderLabel.WithSameCenterY(parent)
            //);
            parent.LayoutIfNeeded();

            _placeholderLabel.Hidden = parent.HasText;
        }
        private void SetPlaceholder(PlaceHolderEditor element, UITextView parent)
        {
            parent.Text = element.Placeholder;
            Color c = element.PlaceholderColor;
            parent.TextColor = new UIColor(new nfloat(c.R), new nfloat(c.G), new nfloat(c.B), new nfloat(c.A));
        }

        private void OnEnded(object sender, EventArgs args)
        {
            if (!((UITextView)sender).HasText && _placeholderLabel != null)
                _placeholderLabel.Hidden = false;
        }

        private void OnChanged(object sender, EventArgs args)
        {
            if (_placeholderLabel != null)
                _placeholderLabel.Hidden = ((UITextView)sender).HasText;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.Ended -= OnEnded;
                Control.Changed -= OnChanged;

                _placeholderLabel?.Dispose();
                _placeholderLabel = null;
            }

            base.Dispose(disposing);
        }
    }
}
