﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Behavior
{
    static class TextBlockBehavior
    {
        public static string GetUpperText(DependencyObject obj) { return (string)obj.GetValue(UpperTextProperty); }
        public static void SetUpperText(DependencyObject obj, string value) { obj.SetValue(UpperTextProperty, value); }

        public static readonly DependencyProperty UpperTextProperty = DependencyProperty.RegisterAttached("UpperText", typeof(string), typeof(TextBlockBehavior), new UIPropertyMetadata(string.Empty, OnUpperTextChanged));

        private static void OnUpperTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TextBlock element = obj as TextBlock;

            if (element == null)
                throw new ArgumentException();

            element.Text = e.NewValue.ToString().ToUpper();
        }
    }
}
