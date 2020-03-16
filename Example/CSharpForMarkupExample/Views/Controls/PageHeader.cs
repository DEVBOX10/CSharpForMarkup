﻿using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using static Xamarin.Forms.Markup.GridRowsColumns;

namespace CSharpForMarkupExample.Views.Controls
{
    public static class PageHeader
    {
        static double rowHeight = 25;

        public static double ButtonDistanceFromTopOfPage => rowHeight * 2;

        public static double ButtonHeight => rowHeight;

        enum Row { StatusBar, Title, Subtitle }
        enum Col { First, BackButton = First, Title, Last = Title }

        public static Grid Create(
            double pageMarginSize, 
            string titlePropertyName = null, 
            string subTitlePropertyName = null, 
            string returnToPreviousViewCommandPropertyName = null, 
            string allowBackNavigationPropertyName = null, 
            Colors backgroundColor = Colors.ColorValuePrimary,
            bool centerTitle = false)
        {
            var grid = new Grid
            {
                BackgroundColor = backgroundColor.ToColor(),

                ColumnSpacing = 0,
                ColumnDefinitions = Columns.Define (
                    (Col.BackButton, 60 ),
                    (Col.Title     , GridLength.Star )
                ),

                RowDefinitions = Rows.Define (
                    ( Row.StatusBar, Device.RuntimePlatform == Device.iOS ? rowHeight : 0 ),
                    ( Row.Title    , rowHeight ),
                    ( Row.Subtitle , rowHeight )
                ),

                Children = {
                    new ContentView { Content = (returnToPreviousViewCommandPropertyName != null) ?
                        new Button { Text = "<" } .Font (24, bold: true) .TextColor (Colors.White) .BackgroundColor (backgroundColor)
                        .Left() .CenterVertical()
                        .Bind(Button.CommandProperty, returnToPreviousViewCommandPropertyName)
                        : null
                    } .Row (Row.Title, Row.Subtitle) .Column (Col.BackButton) .Padding (pageMarginSize, 0)
                      .Invoke(b => { if (allowBackNavigationPropertyName != null) b.Bind(ContentView.IsVisibleProperty, allowBackNavigationPropertyName); }),

                    new Label { 
                        LineBreakMode = LineBreakMode.TailTruncation, 
                        HorizontalOptions = centerTitle ? LayoutOptions.Center : LayoutOptions.Start,
                        VerticalOptions = subTitlePropertyName != null ? LayoutOptions.End : LayoutOptions.Center 
                    } .Bold () .TextColor (Colors.White)
                      .Row (Row.Title, subTitlePropertyName != null ? Row.Title : Row.Subtitle) .Column (centerTitle ? Col.First : Col.Title, centerTitle ? Col.Last : Col.Title)
                      .Invoke(l => { if (titlePropertyName != null) l.Bind(titlePropertyName); })
                }
            };

            if (subTitlePropertyName != null) grid.Children.Add(
                new Label { 
                    LineBreakMode = LineBreakMode.TailTruncation, 
                    HorizontalOptions = centerTitle ? LayoutOptions.Center : LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start 
                }.Bold () .TextColor (Colors.White)
                 .Row (Row.Subtitle) .Column (centerTitle ? Col.First : Col.Title, centerTitle ? Col.Last : Col.Title)
                 .Bind(subTitlePropertyName)
            );
            return grid;
        }
    }
}
