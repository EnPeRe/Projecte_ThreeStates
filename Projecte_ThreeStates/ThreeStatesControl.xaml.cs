using Projecte_ThreeStates.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projecte_ThreeStates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThreeStatesControl : ContentView
    {
        #region .: Properties :.

        private List<Image> StateImages { get; set; }

        public string NeutralStateImageSource
        {
            get { return (string)GetValue(NeutralStateImageSourceProperty); }
            set { SetValue(NeutralStateImageSourceProperty, value); }
        }

        public string SetStateImageSource
        {
            get { return (string)GetValue(SetStateImageSourceProperty); }
            set { SetValue(SetStateImageSourceProperty, value); }
        }

        public string UnsetStateImageSource
        {
            get { return (string)GetValue(UnsetStateImageSourceProperty); }
            set { SetValue(UnsetStateImageSourceProperty, value); }
        }

        public States State
        {
            get { return (States)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region .: Bindable Properties :.

        public static readonly BindableProperty NeutralStateImageSourceProperty =
            BindableProperty.Create(nameof(NeutralStateImageSource), typeof(string), typeof(ThreeStatesControl), string.Empty);

        public static readonly BindableProperty SetStateImageSourceProperty =
            BindableProperty.Create(nameof(SetStateImageSource), typeof(string), typeof(ThreeStatesControl), string.Empty);

        public static readonly BindableProperty UnsetStateImageSourceProperty =
            BindableProperty.Create(nameof(UnsetStateImageSource), typeof(string), typeof(ThreeStatesControl), string.Empty);

        public static readonly BindableProperty StateProperty =
            BindableProperty.Create(nameof(State), typeof(States), typeof(ThreeStatesControl), States.Neutral,
                propertyChanged: OnStatePropertyChanged);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ThreeStatesControl), string.Empty,
                propertyChanged: OnTextPropertyChanged);

        #endregion

        #region .: Constructor :.

        public ThreeStatesControl()
        {
            InitializeComponent();

            StateImages = new List<Image>();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            StateImages.Clear();
            StateImages.Add(new Image() { Source = NeutralStateImageSource });
            StateImages.Add(new Image() { Source = SetStateImageSource });
            StateImages.Add(new Image() { Source = UnsetStateImageSource });

            ImageState.Source = StateImages.FirstOrDefault().Source;
        }

        #endregion

        #region .: Property Changed's :.

        private static async void OnStatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ThreeStatesControl)bindable;
            if (newValue is States state)
            {
                await control.ImageState.ScaleTo(0, 150);
                control.ImageState.Source = control.StateImages.ElementAt((int)state).Source;
                await control.ImageState.ScaleTo(1, 150);
            }
        }

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ThreeStatesControl)bindable;
            if (newValue is string text)
            {
                control.LabelText.Text = text;
            }
        }

        #endregion

        #region .: Events :.

        private void Element_Tapped(object sender, EventArgs e)
        {
            State = State.Next();
        }

        #endregion
    }

    public enum States
    {
        Neutral,
        Set,
        Unset
    }
}