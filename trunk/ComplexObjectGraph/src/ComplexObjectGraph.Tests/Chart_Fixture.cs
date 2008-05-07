using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace ComplexObjectGraph.Tests
{
    [TestFixture]
    public class Chart_Fixture
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;
        private ISession _session;
        private Chart _chart;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof(Chart).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetupContext()
        {
            _session = _sessionFactory.OpenSession();
            new SchemaExport(_configuration).Execute(false, true, false, false, _session.Connection, null);
            CreateInitialData(_session);
            _session.Clear();
        }

        [TearDown]
        public void TearDownContext()
        {
            _session.Close();
            _session.Dispose();
        }

        [Test]
        public void Default_Load()
        {
            var chart = _session.Get<Chart>(1);
            DumpChart(chart);
        }

        [Test]
        public void Eager_fetch()
        {
            var chartId = _chart.Id;
            var chart = _session.CreateQuery("from Chart chart" +
                                             " inner join fetch chart.MainCurve mc" +
                                             " left join fetch mc.Segments s" +
                                             " left join fetch s.SegmentData sd" +
                                             " left join fetch s.PlotDetails spd" +
                                             " left join fetch s.Transition t" +
                                             " left join fetch t.PlotDetails tpd" +
                                             " where chart.Id=:id")
                .SetInt32("id", chartId)
                .UniqueResult<Chart>();

            var chart2 = _session.CreateQuery("from Chart chart" +
                                              " inner join fetch chart.MainCurve mc" +
                                              " left join fetch mc.Segments s" +
                                              " left join fetch s.Points sp" +
                                              " left join fetch sp.DataPoint dp" +
                                              " where chart.Id=:id")
                .SetInt32("id", chartId)
                .UniqueResult<Chart>();

            var chart3 = _session.CreateQuery("from Chart chart" +
                                              " inner join fetch chart.MainCurve mc" +
                                              " left join fetch mc.Segments s" +
                                              " left join fetch s.Transition t" +
                                              " left join fetch t.Points tp" +
                                              " left join fetch tp.DataPoint dp" +
                                              " where chart.Id=:id")
                .SetInt32("id", chartId)
                .UniqueResult<Chart>();

            Assert.AreSame(chart, chart2);
            Assert.AreSame(chart, chart3);

            DumpChart(chart);
        }

        private void CreateInitialData(ISession session)
        {
            IList<DataPoint> points = new List<DataPoint>();
            Console.WriteLine("--------------- create initial data ---------------------");
            var random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 20; i++)
            {
                var point = new DataPoint { X = random.NextDouble(), Y = random.NextDouble() };
                points.Add(point);
                session.Save(point);
            }

            _chart = new Chart { MainCurve = new Curve { Name = "Sample Curve 1" } };

            var transition1 = new Transition
            {
                Name = "Transition 1-->2",
                PlotDetails = new PlotDetails { LineColor = Color.Black }
            };
            var transition2 = new Transition
            {
                Name = "Transition 2-->3",
                PlotDetails = new PlotDetails { LineColor = Color.Black }
            };

            var segment1 = new Segment
            {
                Name = "Segment 1",
                SegmentData = new SegmentData { Data = "Some data..." },
                PlotDetails = new PlotDetails { LineColor = Color.Red },
                Transition = transition1
            };
            AddDataPointsToSegment(segment1, 0, 5, points);
            AddDataPointsToTransition(transition1, 6, 8, points);
            var segment2 = new Segment
            {
                Name = "Segment 2",
                SegmentData = new SegmentData { Data = "Some other data..." },
                PlotDetails = new PlotDetails { LineColor = Color.Blue },
                Transition = transition2
            };
            AddDataPointsToSegment(segment2, 9, 12, points);
            AddDataPointsToTransition(transition2, 13, 15, points);
            var segment3 = new Segment
            {
                Name = "Segment 3",
                PlotDetails = new PlotDetails { LineColor = Color.Green }
            };
            AddDataPointsToSegment(segment3, 16, 19, points);

            _chart.MainCurve.Segments.Add(segment1);
            _chart.MainCurve.Segments.Add(segment2);
            _chart.MainCurve.Segments.Add(segment3);

            foreach (var segment in _chart.MainCurve.Segments)
            {
                session.Save(segment);
                if (segment.Transition != null)
                    session.Save(segment.Transition);
            }
            session.Save(_chart.MainCurve);
            session.Save(_chart);

            session.Flush();
            Console.WriteLine("--------------- initial data created ---------------------");
        }

        private void AddDataPointsToSegment(Segment segment, int minIndex, int maxIndex, IList<DataPoint> points)
        {
            for (int i = minIndex; i <= maxIndex; i++)
                segment.Points.Add(new SegmentPoint { DataPoint = points[i] });
        }

        private void AddDataPointsToTransition(Transition transition, int minIndex, int maxIndex, IList<DataPoint> points)
        {
            for (int i = minIndex; i <= maxIndex; i++)
                transition.Points.Add(new TransitionPoint { DataPoint = points[i] });
        }

        private void DumpChart(Chart chart)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Chart with Id={0}\r\n", chart.Id);
            sb.AppendFormat("  MainCurve: Name={0}\r\n", chart.MainCurve.Name);
            foreach (var segment in chart.MainCurve.Segments)
            {
                sb.AppendFormat("    Segment: Name={0}\r\n", segment.Name);
                foreach (var point in segment.Points)
                    sb.AppendFormat("      Point: id={0}, x={1}, y={2}\r\n",
                                    point.DataPoint.Id, point.DataPoint.X, point.DataPoint.Y);
                sb.AppendFormat("      PlotDetails: LineColor={0}\r\n", segment.PlotDetails.LineColor);
                if (segment.SegmentData != null)
                    sb.AppendFormat("      SegmentData: Data={0}\r\n", segment.SegmentData.Data);
                if (segment.Transition != null)
                {
                    sb.AppendFormat("      Transition: Name={0}\r\n", segment.Transition.Name);
                    foreach (var point in segment.Transition.Points)
                        sb.AppendFormat("        Point: id={0}, x={1}, y={2}\r\n",
                                        point.DataPoint.Id, point.DataPoint.X, point.DataPoint.Y);
                }
            }
                
            Console.WriteLine(sb);
        }
    }
}
