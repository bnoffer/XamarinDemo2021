// -----------------------------------------------------------------
//    Class:		StarRatingView.xaml.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 10.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinDemo2021.Views
{
    public partial class StarRatingView : ContentView
    {
        private double _starRating;
        public double StarRating
        {
            get { return _starRating; }
            set
            {
                _starRating = value;

                StarRatingList.Clear();

                if (_starRating > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < _starRating)
                            StarRatingList.Add(true);
                        else
                            StarRatingList.Add(false);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                        StarRatingList.Add(false);
                }

                StarsCollection.ItemsSource = StarRatingList;
            }
        }

        public List<bool> StarRatingList { get; set; }

        public static readonly BindableProperty StarRatingProperty = BindableProperty.Create(
                                                                     propertyName: "StarRating",
                                                                     returnType: typeof(double),
                                                                     declaringType: typeof(StarRatingView),
                                                                     defaultValue: 0.0,
                                                                     defaultBindingMode: BindingMode.TwoWay,
                                                                     propertyChanged: StarRatingPropertyChanged);



        public StarRatingView()
        {
            InitializeComponent();

            StarRatingList = new List<bool>();
        }

        private static void StarRatingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (StarRatingView)bindable;
            control.StarRating = (double)newValue;
        }
    }
}
