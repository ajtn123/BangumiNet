using BangumiNet.Converters;
using BangumiNet.Models.ItemNetwork;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace BangumiNet.ViewModels;

public partial class ItemNetworkViewModel : ViewModelBase
{
    public ItemNetworkViewModel(ItemViewModelBase item)
    {
        Origin = new Node { Item = item, X = 0, Y = 0 };
        NodeSeries = new ScatterSeries<Node>
        {
            Values = [Origin],
            Mapping = (node, point) => new(node.X, node.Y),
            GeometrySize = 15,
            XToolTipLabelFormatter = cp => NameCnCvt.Convert(cp.Model?.Item) ?? "",
            YToolTipLabelFormatter = cp => cp.Model?.Item.ItemTypeEnum.ToString() ?? "",
        };
        NodeSeries.ChartPointPointerDown += async (s, e) => await Load(e.Model!);
        Series = [NodeSeries];
        _ = Load(Origin);

    }
    public async Task Load(Node node)
    {
        var a = new SubjectBadgeListViewModel(node.Item.ItemTypeEnum, node.Item.Id);
        await a.LoadSubjects();
        var vms = a.SubjectViewModels!;
        var radius = Math.Sqrt(vms.Count) * 10;
        var nodes = vms.Select(x => new Node { Item = x, X = Common.RandomDouble(-radius, radius) + node.X, Y = Common.RandomDouble(-radius, radius) + node.Y }).ToArray();
        NodeSeries.Values = [.. NodeSeries.Values!.UnionBy(nodes, x => x.Item, new ItemViewModelEqualityComparer())];
        //var relations = nodes.Select(n => new Relationship { From = node, To = n, Relation = (n.Item as PersonViewModel)?.Relation ?? "zz" });
        //RelationSeries = relations.Select(relation => new LineSeries<Node, VoidGeometry>
        //{
        //    Values = [relation.From, relation.To],
        //    Mapping = (node, point) => new(node.X, node.Y),
        //    XToolTipLabelFormatter = cp => relation.Relation,
        //    Stroke = Paint.Parse("3f7f7f"),
        //    Fill = null,
        //    GeometrySize = 0
        //});
    }

    [Reactive] public partial ISeries[]? Series { get; set; }
    [Reactive] public partial ScatterSeries<Node> NodeSeries { get; set; }
    [Reactive] public partial Node Origin { get; set; }
}
