using Avalonia.Controls;
using Avalonia.Controls.Documents;
using BangumiNet.Api.Interfaces;
using FluentAvalonia.Core;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SubjectCollectionCountView : ReactiveUserControl<ICollection>
{
    public SubjectCollectionCountView()
    {
        InitializeComponent();
        this.WhenAnyValue(x => x.ViewModel).Subscribe(vm =>
        {
            if (vm is not { } col || col.GetTotal() == 0)
            {
                LayoutGird.ColumnDefinitions = [GetCD(1), GetCD(1), GetCD(1), GetCD(1), GetCD(1)];
                WishRun.Text = DoingRun.Text = DoneRun.Text = OnHoldRun.Text = DroppedRun.Text = string.Empty;
                return;
            }

            WishRun.Text = col.Wish.ToString();
            DoingRun.Text = col.Doing.ToString();
            DoneRun.Text = col.Collect.ToString();
            OnHoldRun.Text = col.OnHold.ToString();
            DroppedRun.Text = col.Dropped.ToString();

            LayoutGird.ColumnDefinitions = [
                GetCD(col.Wish, WishRun),
                GetCD(col.Doing, DoingRun),
                GetCD(col.Collect, DoneRun),
                GetCD(col.OnHold, OnHoldRun),
                GetCD(col.Dropped, DroppedRun),
            ];

            ColumnDefinition GetCD(int? width, Run? run = null)
            {
                ColumnDefinition cd;
                if (width is int w)
                    cd = new(w, GridUnitType.Star);
                else
                    cd = new(0, GridUnitType.Auto);
                if (run != null)
                    cd.Bind(ColumnDefinition.MinWidthProperty, this.WhenAnyValue(x => x.IsPointerOver).Select(isPointerOver => isPointerOver ? LayoutUtils.CalculateTextSize("占位 " + run.Text, 14, run.FontFamily).Width + 20 : 5d));
                return cd;
            }
        });
    }
}