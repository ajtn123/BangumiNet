using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace BangumiNet.Utils;

public static class DraggableHelper
{
    public static readonly AttachedProperty<bool> IsDraggableProperty
        = AvaloniaProperty.RegisterAttached<Control, bool>("IsDraggable", typeof(DraggableHelper));

    public static bool GetIsDraggable(Control control)
        => control.GetValue(IsDraggableProperty);
    public static void SetIsDraggable(Control control, bool value = true)
    {
        control.SetValue(IsDraggableProperty, value);
        if (value)
        {
            control.PointerMoved += Control_PointerMoved;
            control.PointerPressed += Control_PointerPressed;
            control.PointerReleased += Control_PointerReleased;
            control.PointerCaptureLost += Control_PointerCaptureLost;
            if (control.FindAncestorOfType<Window>(true) is Window window)
                window.LostFocus += Control_LostFocus;
        }
        else
        {
            control.PointerMoved -= Control_PointerMoved;
            control.PointerPressed -= Control_PointerPressed;
            control.PointerReleased -= Control_PointerReleased;
            control.PointerCaptureLost -= Control_PointerCaptureLost;
            if (control.FindAncestorOfType<Window>(true) is Window window)
                window.LostFocus -= Control_LostFocus;
        }
    }

    private static bool IsMouseDown;
    private static PointerPoint OriginalPoint;

    //https://github.com/AvaloniaUI/Avalonia/discussions/8441#discussioncomment-3081536
    private static void Control_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (sender is not Control control || !IsMouseDown) return;
        Window? window = control as Window ?? control.FindAncestorOfType<Window>();
        if (window != null)
        {
            PointerPoint currentPoint = e.GetCurrentPoint(window);
            PointerPoint originalPoint = OriginalPoint;
            window.Position = new PixelPoint(
                window.Position.X + (int)(currentPoint.Position.X - originalPoint.Position.X),
                window.Position.Y + (int)(currentPoint.Position.Y - originalPoint.Position.Y)
            );
        }
    }
    private static void Control_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Control control) return;
        Window? window = control as Window ?? control.FindAncestorOfType<Window>();
        if (window != null && window.WindowState == WindowState.Normal && e.GetCurrentPoint(window).Properties.IsLeftButtonPressed)
        {
            IsMouseDown = true;
            OriginalPoint = e.GetCurrentPoint(window);
        }
    }

    private static void Control_PointerReleased(object? sender, PointerReleasedEventArgs e)
        => IsMouseDown = false;
    private static void Control_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
        => IsMouseDown = false;
    private static void Control_LostFocus(object? sender, RoutedEventArgs e)
        => IsMouseDown = false;
}
