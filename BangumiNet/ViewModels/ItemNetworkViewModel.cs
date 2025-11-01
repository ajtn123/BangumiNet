using BangumiNet.Converters;
using BangumiNet.Models.ItemNetwork;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.VisualElements;
using SkiaSharp;

namespace BangumiNet.ViewModels;

public partial class ItemNetworkViewModel : ViewModelBase
{
    public ItemNetworkViewModel(ItemViewModelBase item)
    {
        Origin = new Node { Item = item, X = 0, Y = 0 };
        SubjectSeries = new ScatterSeries<Node>
        {
            Values = [],
            Mapping = (node, point) => new(node.X, node.Y),
            Stroke = new SolidColorPaint(SKColors.Blue) { ZIndex = 99 },
            GeometrySize = 10,
            DataLabelsSize = 10,
            DataLabelsPaint = new SolidColorPaint(SKColors.DarkBlue) { ZIndex = 89 },
            DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Right,
            DataLabelsFormatter = point => NameCnCvt.Convert(point.Model?.Item) ?? "",
            XToolTipLabelFormatter = point => NameCnCvt.Convert(point.Model?.Item) ?? "",
            YToolTipLabelFormatter = point => point.Model?.Item.ItemTypeEnum.ToString() ?? "",
        };
        CharacterSeries = new ScatterSeries<Node>
        {
            Values = [],
            Mapping = (node, point) => new(node.X, node.Y),
            Stroke = new SolidColorPaint(SKColors.Red) { ZIndex = 98 },
            GeometrySize = 10,
            DataLabelsSize = 10,
            DataLabelsPaint = new SolidColorPaint(SKColors.DarkRed) { ZIndex = 88 },
            DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Right,
            DataLabelsFormatter = point => NameCnCvt.Convert(point.Model?.Item) ?? "",
            XToolTipLabelFormatter = point => NameCnCvt.Convert(point.Model?.Item) ?? "",
            YToolTipLabelFormatter = point => point.Model?.Item.ItemTypeEnum.ToString() ?? "",
        };
        PersonSeries = new ScatterSeries<Node>
        {
            Values = [],
            Mapping = (node, point) => new(node.X, node.Y),
            Stroke = new SolidColorPaint(SKColors.Green) { ZIndex = 97 },
            GeometrySize = 10,
            DataLabelsSize = 10,
            DataLabelsPaint = new SolidColorPaint(SKColors.DarkGreen) { ZIndex = 87 },
            DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Right,
            DataLabelsFormatter = point => NameCnCvt.Convert(point.Model?.Item) ?? "",
            XToolTipLabelFormatter = point => NameCnCvt.Convert(point.Model?.Item) ?? "",
            YToolTipLabelFormatter = point => point.Model?.Item.ItemTypeEnum.ToString() ?? "",
        };
        SubjectSeries.ChartPointPointerDown += async (s, e) => await Load(e.Model!);
        CharacterSeries.ChartPointPointerDown += async (s, e) => await Load(e.Model!);
        PersonSeries.ChartPointPointerDown += async (s, e) => await Load(e.Model!);
        Series = [SubjectSeries, CharacterSeries, PersonSeries];
        if (item.ItemTypeEnum == ItemType.Subject)
            SubjectSeries.Values = [Origin];
        if (item.ItemTypeEnum == ItemType.Character)
            CharacterSeries.Values = [Origin];
        if (item.ItemTypeEnum == ItemType.Person)
            PersonSeries.Values = [Origin];
        Relationships = [];
        _ = Load(Origin);
    }
    public async Task Load(Node node)
    {
        if (node.Item.ItemTypeEnum == ItemType.Subject)
        {
            await Load(node, ItemType.Subject);
            await Load(node, ItemType.Person);
            await Load(node, ItemType.Character);
        }
        else if (node.Item.ItemTypeEnum == ItemType.Character)
        {
            await Load(node, ItemType.Subject);
            await Load(node, ItemType.Person);
        }
        else if (node.Item.ItemTypeEnum == ItemType.Person)
        {
            await Load(node, ItemType.Subject);
            await Load(node, ItemType.Character);
        }
    }
    public async Task Load(Node node, ItemType itemType)
    {
        if (itemType == ItemType.Subject)
        {
            var a = new SubjectBadgeListViewModel(node.Item.ItemTypeEnum, node.Item.Id);
            await a.LoadSubjects();
            var vms = a.SubjectViewModels!;
            var radius = Math.Sqrt(vms.Count) * 10;
            var nodes = vms.Select(vm => new Node { Item = vm, X = Common.RandomDouble(-radius, radius) + node.X, Y = Common.RandomDouble(-radius, radius) + node.Y }).ToArray();
            SubjectSeries.Values = [.. SubjectSeries.Values!.UnionBy(nodes, x => x.Item.Id)];
            Relationships = [.. Relationships.Union(SubjectSeries.Values.IntersectBy(vms.Select(vm => vm.Id), n => n.Item.Id).Select(n => new RelationshipVisual(node, n)))];
        }
        else if (itemType == ItemType.Character)
        {
            var a = new CharacterBadgeListViewModel(node.Item.ItemTypeEnum, node.Item.Id);
            await a.LoadCharacters();
            var vms = a.CharacterViewModels!;
            var radius = Math.Sqrt(vms.Count) * 10;
            var nodes = vms.Select(vm => new Node { Item = vm, X = Common.RandomDouble(-radius, radius) + node.X, Y = Common.RandomDouble(-radius, radius) + node.Y }).ToArray();
            CharacterSeries.Values = [.. CharacterSeries.Values!.UnionBy(nodes, x => x.Item.Id)];
            Relationships = [.. Relationships.Union(CharacterSeries.Values.IntersectBy(vms.Select(vm => vm.Id), n => n.Item.Id).Select(n => new RelationshipVisual(node, n)))];
        }
        else if (itemType == ItemType.Person)
        {
            var a = new PersonBadgeListViewModel(node.Item.ItemTypeEnum, node.Item.Id);
            await a.LoadPersons();
            var vms = a.PersonViewModels!;
            var radius = Math.Sqrt(vms.Count) * 10;
            var nodes = vms.Select(vm => new Node { Item = vm, X = Common.RandomDouble(-radius, radius) + node.X, Y = Common.RandomDouble(-radius, radius) + node.Y }).ToArray();
            PersonSeries.Values = [.. PersonSeries.Values!.UnionBy(nodes, x => x.Item.Id)];
            Relationships = [.. Relationships.Union(PersonSeries.Values.IntersectBy(vms.Select(vm => vm.Id), n => n.Item.Id).Select(n => new RelationshipVisual(node, n)))];
        }

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
    [Reactive] public partial ScatterSeries<Node> SubjectSeries { get; set; }
    [Reactive] public partial ScatterSeries<Node> CharacterSeries { get; set; }
    [Reactive] public partial ScatterSeries<Node> PersonSeries { get; set; }
    [Reactive] public partial IChartElement[] Relationships { get; set; }
    [Reactive] public partial Node Origin { get; set; }
    public static TimeSpan AnimationSpeed { get; } = TimeSpan.FromSeconds(1);
    public static Dictionary<ItemType, SolidColorPaint> RelationshipColors { get; set; } = new()
    {
        [ItemType.Subject] = new(SKColors.LightBlue.WithAlpha(95)) { ZIndex = 9 },
        [ItemType.Character] = new(SKColors.Pink.WithAlpha(95)) { ZIndex = 8 },
        [ItemType.Person] = new(SKColors.LightGreen.WithAlpha(95)) { ZIndex = 7 },
    };

    public class RelationshipVisual : Visual
    {
        public RelationshipVisual(Node startingNode, Node endingNode)
        {
            Easing = EasingFunctions.ExponentialOut;
            AnimationSpeed = TimeSpan.FromSeconds(1);
            DrawnElement = new LineGeometry { Fill = RelationshipColors[endingNode.Item.ItemTypeEnum] };
            StartingNode = startingNode;
            EndingNode = endingNode;
            startingPoint = new(startingNode.X, startingNode.Y);
            endingPoint = new(endingNode.X, endingNode.Y);
        }
        public Node StartingNode { get; }
        public Node EndingNode { get; }
        public LvcPointD startingPoint;
        public LvcPointD endingPoint;
        protected override LineGeometry DrawnElement { get; }
        protected override void Measure(Chart chart)
        {
            var cartesianChart = (ICartesianChartView)chart.View;

            var startingLocation = cartesianChart.ScaleDataToPixels(startingPoint);
            var endingLocation = cartesianChart.ScaleDataToPixels(endingPoint);

            DrawnElement.X = (float)startingLocation.X;
            DrawnElement.Y = (float)startingLocation.Y;
            DrawnElement.X1 = (float)endingLocation.X;
            DrawnElement.Y1 = (float)endingLocation.Y;
        }
    }
}
