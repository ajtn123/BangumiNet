using Avalonia;
using Avalonia.Animation;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media.Transformation;

namespace BangumiNet.Utils;

// https://github.com/DarthMazut/MoveTransition

public class MoveTransition : TransformOperationsTransition
{
    public MoveTransition() => Property = Visual.RenderTransformProperty;
}

public class MoveTransitions : AvaloniaObject
{
    static MoveTransitions() => TransitionProperty.Changed.AddClassHandler<Layoutable>(HandleTransitionChanged);

    public static readonly AttachedProperty<MoveTransition?> TransitionProperty
        = AvaloniaProperty.RegisterAttached<MoveTransitions, Layoutable, MoveTransition?>("Transition", default, false, BindingMode.OneTime);

    private static void HandleTransitionChanged(Layoutable layoutable, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.NewValue is not null)
            layoutable.PropertyChanged += HostLayoutablePropertyChanged;
        else
            layoutable.PropertyChanged -= HostLayoutablePropertyChanged;
    }

    private static async void HostLayoutablePropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != Visual.BoundsProperty) return;

        // Get host control
        Layoutable host = (sender as Layoutable)!;
        //Unsubscribe from position changed, otherwise we'll end up with recursive calls
        host.PropertyChanged -= HostLayoutablePropertyChanged;
        // Calculate move
        var move = ((Rect)e.NewValue!).Center - ((Rect)e.OldValue!).Center;
        // Move host to original position (before position changed)
        // host.RenderTransform = GetTransformOperations(-move.X, -move.Y);
        host.RenderTransform = GetTransformOperations(0, -move.Y);
        // Retrieve transition defined in attached property
        MoveTransition moveTransition = host.GetValue(TransitionProperty)!;
        // If transitions are null create new container
        host.Transitions ??= [];
        // Add MoveTransition defined in attached property
        host.Transitions.Add(moveTransition);
        // Move element from original position to current one but now with transition
        host.RenderTransform = GetTransformOperations(0, 0);
        // Wait until animation finishes
        await Task.Delay(moveTransition.Duration + moveTransition.Delay);
        // Wait some more, as sometime above sometime is not enough
        await Task.Yield();
        // Remove MoveTransition
        host.Transitions.Remove(moveTransition);
        // Subscribe for position changed
        host.PropertyChanged += HostLayoutablePropertyChanged;
    }

    private static TransformOperations GetTransformOperations(double x, double y)
    {
        var builder = new TransformOperations.Builder(1);
        builder.AppendTranslate(x, y);
        return builder.Build();
    }

    public static void SetTransition(AvaloniaObject element, MoveTransition? transition)
        => element.SetValue(TransitionProperty, transition);

    public static MoveTransition? GetCommand(AvaloniaObject element)
        => element.GetValue(TransitionProperty);
}
